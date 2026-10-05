# Fidelity to github.com

markdowned aims at PDFs that look as close to github.com as possible. This file lists the
known deviations; each one is deliberate and measured.

## How fidelity is measured

Fixtures in `src/Tests/Markdowned.Tests/Fidelity/*.md` are rendered by GitHub itself and the
HTML is recorded next to them with `scripts/record-fixtures.sh`. Our HTML is compared with the
recording after normalisation (`NormalizedHtml`): attribute order and insignificant whitespace
are ignored, the page wrapper is removed, `pl-*` highlight spans are unwrapped, camo image
URLs are mapped back to their canonical URL and random id suffixes are dropped.

## Known deviations

- **Front matter:** GitHub renders YAML front matter with a full YAML parser. We support the
  subset it renders as nested tables: scalars, flow lists (`[a, b]`), block lists and one level
  of maps. Deeper nesting, anchors and multi-line scalars are shown as plain text or omitted.
- **Footnote id suffix:** GitHub appends a random 32-hex suffix to footnote ids; we use a
  constant one (the comparison ignores it).
- **Emoji:** shortcodes come from Markdig's table, minus names that do not start with a letter
  or digit (GitHub leaves `:-1:` as text). Custom emoji such as `:octocat:` stay text.
- **Raw HTML:** GitHub parses raw HTML into a tree and sanitises the tree; we sanitise the
  token stream (GFM tag filter, GitHub's element and attribute allow-list, safe URL protocols,
  `user-content-` ids, `dir="auto"`, image links, pictures, tables, headings). The result is the
  same for well-formed HTML. Differences appear for malformed or exotic HTML, for example block
  elements that GitHub's parser hoists out of a paragraph before removing them, and for GitHub
  auto-linking URLs inside text that the tag filter escaped.
- **Syntax highlighting:** GitHub highlights on the server with its own grammar versions; we
  highlight in the page with starry-night, GitHub's open-source highlighter. The language is
  found by the same name/extension lookup, and the wrapper markup matches, but a few
  tokens can get different `pl-*` classes (for example `var` in C#). The comparison unwraps
  the highlight spans, so it checks the structure, not the colours.
- **Math:** the server markup (`math-renderer`) matches GitHub's, and MathJax typesets it in the
  page. GitHub's client loader is not public; we use MathJax 3 with SVG output (no web fonts), so
  glyph shapes and spacing may differ slightly. `\$x\$` is text here but math on GitHub.
- **Mermaid:** GitHub renders diagrams in an iframe from `viewscreen.githubusercontent.com`; we
  render them in the page with the embedded mermaid (default theme, `strict` security level,
  Noto Sans). The server markup differs (no loader or identities), so the fixture is recorded but
  not compared. A diagram that fails to render stays as its source.
