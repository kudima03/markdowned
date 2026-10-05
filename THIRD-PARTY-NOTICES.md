# Third-party notices

markdowned embeds the following software and fonts. Their licence texts are in
`src/Markdowned/Assets/licenses/` and `src/Markdowned/Assets/fonts/`, and are the notices
that must accompany redistributions. The embedded versions are in
`src/Markdowned/Assets/versions.json`; the JavaScript is bundled by `assets/build.mjs`.

| Component | Licence | Notice |
|---|---|---|
| [Markdig](https://github.com/xoofx/markdig) (NuGet dependency) | BSD-2-Clause | on NuGet |
| [github-markdown-css](https://github.com/sindresorhus/github-markdown-css) | MIT | `licenses/github-markdown-css.txt` |
| [starry-night](https://github.com/wooorm/starry-night) | MIT | `licenses/starry-night.txt` |
| TextMate grammars bundled with starry-night | various, see file | `licenses/starry-night-grammars.txt` |
| [vscode-oniguruma](https://github.com/microsoft/vscode-oniguruma) (`onig.wasm`) | MIT | `licenses/vscode-oniguruma.txt`, `licenses/vscode-oniguruma-notices.txt` |
| [MathJax](https://github.com/mathjax/MathJax-src) | Apache-2.0 | `licenses/mathjax.txt` |
| [Mermaid](https://github.com/mermaid-js/mermaid) | MIT | `licenses/mermaid.txt` |
| Packages bundled in Mermaid | MIT, ISC, BSD, Apache-2.0, … | `licenses/mermaid-dependencies.md` |
| Noto Sans, Liberation Mono, Noto Color Emoji | SIL Open Font License 1.1 | `fonts/OFL-1.1.txt`, `fonts/README.md` |

At run time markdowned downloads Chrome for Testing's `chrome-headless-shell` (BSD-style Chromium
licences, shipped inside its archive) when no `--browser` is given.
