# Changelog

All notable changes to `markdowned` are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

### Added

* `markdowned <input.md>` renders Markdown to a PDF with headless Chromium: GitHub-shaped HTML,
  `github-markdown-css`, embedded Noto Sans / Liberation Mono / Noto Color Emoji, A4/Letter/Legal,
  `--landscape`, `--margin`, `n / N` footer, document outline and tagged PDF.
* `--browser <path>` to use any Chromium-based browser; otherwise a pinned
  `chrome-headless-shell` is downloaded once into the per-user cache and verified by SHA-256.
* `markdowned install-browser`, `--offline`, `--quiet`.
* Alerts, footnotes, YAML front matter tables and emoji shortcodes, rendered with GitHub's markup.
* Raw HTML is sanitised like GitHub does it (tag filter, allow-list, safe URLs, `dir="auto"`, image links); unsafe Markdown link and image URLs are dropped.
* Local and remote images, `--offline`, `--timeout`; broken images are reported as warnings.
* Syntax highlighting with starry-night, GitHub's highlighter.
* Math: `$x$`, `$`x`$`, `$$x$$` and ```` ```math ```` blocks typeset with MathJax.
* Mermaid diagrams render with the default theme.
* A weekly workflow pins the newest stable Chrome and its SHA-256 hashes.
* Exit codes: 0 success, 1 arguments or I/O, 2 browser, 3 render.
