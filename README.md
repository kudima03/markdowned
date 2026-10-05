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

## Installation

```shell
dotnet tool install -g Markdowned.Cli
```

The first run without `--browser` downloads a pinned `chrome-headless-shell` (about 120 MB) into
the per-user cache; `markdowned install-browser` does it ahead of time. On a minimal Linux
machine Chromium needs shared libraries, e.g. on Debian/Ubuntu:
`sudo apt install libnss3 libgbm1 libasound2t64 libatk-bridge2.0-0 libcups2 libxkbcommon0`.

## Usage

```shell
markdowned README.md                           # → README.pdf next to the input
markdowned README.md -o out.pdf --paper Letter --landscape
markdowned README.md --browser "/usr/bin/google-chrome"
cat notes.md | markdowned - -o - > notes.pdf
markdowned install-browser
```

| Option | Description |
|---|---|
| `-o`, `--output <file.pdf \| ->` | Output file; `-` is stdout. Default: next to the input |
| `--browser <path>` | Use this Chromium-based browser instead of the pinned download |
| `--paper A4\|Letter\|Legal` | Paper size (default A4) |
| `--landscape` | Landscape orientation |
| `--margin <mm>` | Page margin in millimetres (default 15) |
| `--offline` | Never download anything |
| `--quiet` | No progress on stderr |
| `--help`, `--version` | |

## Design

`markdowned` follows the [Pure](https://github.com/kudima03/Pure) ecosystem conventions; see
[CLAUDE.md](CLAUDE.md) for the code style.

## License

[MIT](LICENSE)
