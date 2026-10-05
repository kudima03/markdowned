// Bundles the browser-side libraries that markdowned embeds into its page.
//
//   npm ci && npm run build
//
// The output goes to ../src/Markdowned/Assets and is committed, so building markdowned needs
// no Node.js. Dependabot keeps the npm packages current; rebuild and commit after a bump.
import { build } from 'esbuild'
import { all } from '@wooorm/starry-night'
import { copyFileSync, mkdirSync, readFileSync, rmSync, writeFileSync } from 'node:fs'
import { dirname, join } from 'node:path'
import { fileURLToPath } from 'node:url'

const here = dirname(fileURLToPath(import.meta.url))
const out = join(here, '..', 'src', 'Markdowned', 'Assets')
const js = join(out, 'js')
const licenses = join(out, 'licenses')

const packageRoot = (name) => join(here, 'node_modules', name)
const version = (name) => JSON.parse(readFileSync(join(packageRoot(name), 'package.json'), 'utf8')).version

rmSync(join(js), { recursive: true, force: true })
mkdirSync(join(js, 'starry-night'), { recursive: true })
mkdirSync(licenses, { recursive: true })

// github-markdown-css: GitHub's stylesheet for rendered Markdown, light theme.
const css = readFileSync(join(packageRoot('github-markdown-css'), 'github-markdown-light.css'), 'utf8')
writeFileSync(
  join(out, 'github-markdown-light.css'),
  `/* github-markdown-css ${version('github-markdown-css')} (MIT), light theme. Vendored by assets/build.mjs. */\n${css}`
)
copyFileSync(join(packageRoot('github-markdown-css'), 'license'), join(licenses, 'github-markdown-css.txt'))

// starry-night: GitHub's syntax highlighter. Every grammar is its own chunk, loaded on demand.
const flags = {}
const loaders = []
const dependencies = {}

for (const grammar of all) {
  loaders.push(`${JSON.stringify(grammar.scopeName)}: () => import(${JSON.stringify(`@wooorm/starry-night/${grammar.scopeName}`)})`)
  dependencies[grammar.scopeName] = grammar.dependencies ?? []

  for (const name of grammar.names) flags[name] = grammar.scopeName
}

const extensions = {}

for (const grammar of all) {
  for (const extension of grammar.extensions ?? []) extensions[extension] ??= grammar.scopeName
  for (const extension of grammar.extensionsWithDot ?? []) extensions[extension] ??= grammar.scopeName
}

const entry = `
import { createStarryNight } from '@wooorm/starry-night'

const flags = ${JSON.stringify(flags)}
const extensions = ${JSON.stringify(extensions)}
const dependencies = ${JSON.stringify(dependencies)}
const loaders = { ${loaders.join(',\n')} }

// Same lookup as starry-night's flagToScope: names first, then extensions.
export function scopeForFlag(flag) {
  const normal = flag.trim().toLowerCase().replace(/\\/+$/, '')
  if (flags[normal]) return flags[normal]
  const dot = normal.lastIndexOf('.')
  return dot === -1 ? extensions['.' + normal] : extensions[normal.slice(dot)]
}

async function grammarsFor(scope, found) {
  if (found.some((grammar) => grammar.scopeName === scope) || !loaders[scope]) return
  const grammar = (await loaders[scope]()).default
  found.push(grammar)
  for (const dependency of dependencies[scope]) await grammarsFor(dependency, found)
}

function escape(text) {
  return text.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;')
}

function toHtml(node) {
  if (node.type === 'text') return escape(node.value)
  const children = (node.children ?? []).map(toHtml).join('')
  if (node.type !== 'element') return children
  const classes = [].concat(node.properties?.className ?? []).join(' ')
  return '<span class="' + classes + '">' + children + '</span>'
}

// Highlights several snippets, [{ source, scope }]; resolves to the markup of each, in order.
// All grammars are loaded first because starry-night only reads its options when it is created.
export async function highlightAll(snippets) {
  const grammars = []
  for (const { scope } of snippets) await grammarsFor(scope, grammars)
  const starryNight = await createStarryNight(grammars, {
    getOnigurumaUrlFetch: () => new URL('/assets/js/starry-night/onig.wasm', location.href)
  })
  return snippets.map(({ source, scope }) => toHtml(starryNight.highlight(source, scope)))
}
`

writeFileSync(join(out, 'languages.json'), JSON.stringify({ flags, extensions }) + '\n')
writeFileSync(join(here, 'starry-night.entry.mjs'), entry)

await build({
  entryPoints: { index: join(here, 'starry-night.entry.mjs') },
  outdir: join(js, 'starry-night'),
  bundle: true,
  splitting: true,
  format: 'esm',
  minify: true,
  target: 'chrome120',
  legalComments: 'none',
  chunkNames: 'grammar-[hash]',
  logLevel: 'warning'
})

rmSync(join(here, 'starry-night.entry.mjs'))
copyFileSync(join(packageRoot('vscode-oniguruma'), 'release', 'onig.wasm'), join(js, 'starry-night', 'onig.wasm'))
copyFileSync(join(packageRoot('@wooorm/starry-night'), 'notice'), join(licenses, 'starry-night-grammars.txt'))
copyFileSync(join(packageRoot('@wooorm/starry-night'), 'license'), join(licenses, 'starry-night.txt'))
copyFileSync(join(packageRoot('vscode-oniguruma'), 'LICENSE.txt'), join(licenses, 'vscode-oniguruma.txt'))
copyFileSync(join(packageRoot('vscode-oniguruma'), 'NOTICES.txt'), join(licenses, 'vscode-oniguruma-notices.txt'))

// MathJax: the SVG output needs no web fonts.
copyFileSync(join(packageRoot('mathjax-full'), 'es5', 'tex-svg.js'), join(js, 'mathjax.js'))
copyFileSync(join(packageRoot('mathjax-full'), 'LICENSE'), join(licenses, 'mathjax.txt'))

// Mermaid, default theme.
copyFileSync(join(packageRoot('mermaid'), 'dist', 'mermaid.min.js'), join(js, 'mermaid.js'))
copyFileSync(join(packageRoot('mermaid'), 'LICENSE'), join(licenses, 'mermaid.txt'))

// Mermaid's bundle contains its dependencies; list them with their licences.
const seen = new Map()
const licenseOf = (name, manifest) => {
  if (typeof manifest.license === 'string') return manifest.license
  if (manifest.license) return JSON.stringify(manifest.license)
  // Some packages only ship a license file: use its title line.
  for (const file of ['LICENSE', 'license', 'LICENSE.md', 'LICENSE.txt']) {
    try {
      return readFileSync(join(packageRoot(name), file), 'utf8').split('\n')[0].trim()
    } catch {
      // try the next name
    }
  }
  return 'unknown'
}
const visit = (name) => {
  if (seen.has(name)) return
  let manifest
  try {
    manifest = JSON.parse(readFileSync(join(packageRoot(name), 'package.json'), 'utf8'))
  } catch {
    return
  }
  seen.set(name, manifest)
  for (const dependency of Object.keys(manifest.dependencies ?? {})) visit(dependency)
}
visit('mermaid')
writeFileSync(
  join(licenses, 'mermaid-dependencies.md'),
  '# Packages bundled in mermaid.js\n\n| Package | Version | License |\n|---|---|---|\n' +
    [...seen.entries()]
      .sort(([a], [b]) => a.localeCompare(b))
      .map(([name, manifest]) => `| ${name} | ${manifest.version} | ${licenseOf(name, manifest)} |`)
      .join('\n') +
    '\n'
)

writeFileSync(
  join(out, 'versions.json'),
  JSON.stringify(
    Object.fromEntries(
      ['github-markdown-css', '@wooorm/starry-night', 'vscode-oniguruma', 'mathjax-full', 'mermaid'].map((name) => [name, version(name)])
    ),
    null,
    2
  ) + '\n'
)
console.log('assets written to', out)
