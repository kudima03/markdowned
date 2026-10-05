# markdowned

Render a Markdown file to a PDF that looks the way github.com renders it.

[![.NET build & test](https://github.com/kudima03/markdowned/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/kudima03/markdowned/actions/workflows/build-and-test.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Overview

`markdowned` is a Native AOT command-line tool for Windows, macOS and Linux. It turns Markdown
into HTML shaped like GitHub's, using GitHub's CSS, fonts and client-side renderers, then prints
that HTML to PDF with headless Chromium. The main goal is output as close to github.com as
possible.

> **Status:** under construction. See [PLAN.md](PLAN.md) for the design and delivery plan.

## Usage

```shell
markdowned README.md                           # → README.pdf next to the input
markdowned README.md -o out.pdf --paper Letter --landscape
markdowned README.md --browser "/usr/bin/google-chrome"
cat notes.md | markdowned - -o - > notes.pdf
```

## Design

`markdowned` follows the [Pure](https://github.com/kudima03/Pure) ecosystem conventions; see
[CLAUDE.md](CLAUDE.md) for the code style.

## License

[MIT](LICENSE)
