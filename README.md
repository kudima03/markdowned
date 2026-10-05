# Pure.Template

Project template for new **Pure** ecosystem NuGet libraries.

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)

## Overview

`Pure.Template` is the canonical starting point for new Pure ecosystem packages. It ships with a pre-configured project structure, `.editorconfig`, GitHub Actions workflows for build/test and NuGet publish, and standard `CODEOWNERS` and `CODE_OF_CONDUCT.md` files — so a new library can be bootstrapped consistently without manually copying configuration from another repo.

## What's included

| Item | Description |
|------|-------------|
| `.editorconfig` | Shared code style rules for the Pure ecosystem |
| `CODEOWNERS` | Default ownership configuration |
| `CODE_OF_CONDUCT.md` | Contributor code of conduct |
| `.github/workflows/build-and-test.yml` | CI workflow: restore, build, and test on every push |
| `.github/workflows/publish-nuget.yml` | Publish workflow: triggered on git tag push |
