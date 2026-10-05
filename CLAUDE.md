# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

`markdowned` renders Markdown to a PDF that looks like github.com: Markdig → GitHub-shaped
HTML → headless Chromium `Page.printToPDF` over a small AOT-safe DevTools client. Fidelity to
GitHub is the main goal; every deviation is measured and listed in `docs/fidelity.md`.

## Commands

All `dotnet` commands must be run from the `./src` directory.

```bash
dotnet restore
dotnet build --no-restore -warnaserror
dotnet format --verify-no-changes             # check code style (CI enforces this)
dotnet format                                 # auto-fix code style
dotnet csharpier check .                      # second style gate enforced by CI
dotnet test --no-build --verbosity normal     # run tests
```

## Code Style

Pure ecosystem style, same as `kartchrono-cli`. Enforced via `src/.editorconfig`,
`dotnet format --verify-no-changes` and `csharpier check .`:

- One `public sealed record` per file, implementing exactly one interface.
- Behaviour lives in property getters. Do not add methods — the only ones permitted are those
  the language forces plus `GetHashCode`/`ToString`, which both `throw new NotSupportedException()`.
- No static helpers or utility classes.
- Values are lazy: recompute on each property access, never cache.
- Values are `IString`, `INumber<T>` and `IBool`, never raw primitives.
- No `var` — always explicit types.
- File-scoped namespaces. All braces on new lines. Max line length 90.
- Private fields `_camelCase`.

## Delivery

Stacked PRs, each linked to an issue with `Closes #N`; the owner merges them. Commit messages
are `<type>: <subject>` — no scope, no body, no trailer. Keep `CHANGELOG.md` current.

Do not mention Claude or AI assistance in commits, PRs, or code.
