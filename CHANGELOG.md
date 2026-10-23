# Changelog

All notable changes to `markdowned` are documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.1.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [0.1.0-preview.0.2.0] - 2026-10-23

### Changed

* Bundled mermaid updated from 11.17.2 to 12.2.0. The bundle now includes elkjs (EPL-2.0) and
  grows the tool package by about 2 MB.

## [0.1.0-preview.0.1.1] - 2026-10-22

### Changed

* Markdig updated from 1.4.0 to 1.4.2 and Pure.Primitives from 3.6.5 to 3.7.0.

## [0.1.0-preview.0.1.0] - 2026-10-05

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
* Exit codes: 0 success, 1 arguments or I/O, 2 browser, 3 render.
