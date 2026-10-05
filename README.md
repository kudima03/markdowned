# markdowned

Render a Markdown file to a PDF that looks the way github.com renders it.

[![.NET build & test](https://github.com/kudima03/markdowned/actions/workflows/build-and-test.yml/badge.svg?branch=main)](https://github.com/kudima03/markdowned/actions/workflows/build-and-test.yml)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Overview

`markdowned` is a Native AOT command-line tool for Windows, macOS and Linux. It turns Markdown
into HTML shaped like GitHub's, using GitHub's CSS, fonts and client-side renderers, then prints
that HTML to PDF with headless Chromium. The main goal is output as close to github.com as
possible.

## Installation

```shell
dotnet tool install -g Markdowned.Cli
```

Or use a native binary, which needs no .NET:

**linux-x64**

```shell
wget https://github.com/kudima03/markdowned/releases/download/0.1.0-preview.0.1.0/markdowned-0.1.0-preview.0.1.0-linux-x64.zip
unzip markdowned-0.1.0-preview.0.1.0-linux-x64.zip
sudo mv markdowned /usr/local/bin/
markdowned --version
```

**linux-arm64**

```shell
wget https://github.com/kudima03/markdowned/releases/download/0.1.0-preview.0.1.0/markdowned-0.1.0-preview.0.1.0-linux-arm64.zip
unzip markdowned-0.1.0-preview.0.1.0-linux-arm64.zip
sudo mv markdowned /usr/local/bin/
markdowned --version
```

**osx-arm64**

```shell
wget https://github.com/kudima03/markdowned/releases/download/0.1.0-preview.0.1.0/markdowned-0.1.0-preview.0.1.0-osx-arm64.zip
unzip markdowned-0.1.0-preview.0.1.0-osx-arm64.zip
sudo mv markdowned /usr/local/bin/
markdowned --version
```

**win-x64** (PowerShell)

```powershell
Invoke-WebRequest https://github.com/kudima03/markdowned/releases/download/0.1.0-preview.0.1.0/markdowned-0.1.0-preview.0.1.0-win-x64.zip -OutFile markdowned.zip
Expand-Archive markdowned.zip -DestinationPath .
.\markdowned.exe --version
```

Put `markdowned.exe` in a folder on your `PATH` to run it from anywhere.

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
| `--offline` | Never download anything: no browser download, no remote images |
| `--timeout <seconds>` | Give up rendering after this long (default 60, counted once the browser runs) |
| `--quiet` | No progress on stderr |
| `--help`, `--version` | |

## Images

Local images are resolved relative to the Markdown file (or the working directory for stdin) and
only image files below that directory are served. Remote images are fetched unless `--offline`
is set. Images that cannot be loaded render as broken images, like on GitHub, and a warning goes
to stderr. Nothing else is ever fetched: every other request from the page is blocked.

## Design

`markdowned` follows the [Pure](https://github.com/kudima03/Pure) ecosystem conventions; see
[CLAUDE.md](CLAUDE.md) for the code style.

## License

[MIT](LICENSE)
