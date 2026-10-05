#!/usr/bin/env bash
# Records the HTML that github.com renders for every fidelity fixture.
#
# Usage: scripts/record-fixtures.sh [git-ref] [fixture-name ...]
#
# The ref (default: HEAD) must be pushed to GitHub, because the rendering comes from the
# repository contents endpoint (Accept: application/vnd.github.html+json). Unlike POST
# /markdown it returns the markup github.com shows on a file page: heading wrappers and
# anchors, dir="auto", alert and footnote markup, server-side highlighting.
set -euo pipefail

cd "$(dirname "$0")/.."

fixtures=src/Tests/Markdowned.Tests/Fidelity
ref=$(git rev-parse "${1:-HEAD}")
shift || true
names=("$@")
repo=$(gh repo view --json nameWithOwner --jq .nameWithOwner)

if ! git branch -r --contains "$ref" | grep -q .; then
  echo "ref $ref is not pushed to GitHub; push it first." >&2
  exit 1
fi

for markdown in "$fixtures"/*.md; do
  name=$(basename "$markdown" .md)
  if [ ${#names[@]} -gt 0 ] && [[ ! " ${names[*]} " =~ " $name " ]]; then continue; fi
  echo "recording $name" >&2
  gh api -H "Accept: application/vnd.github.html+json" \
    "repos/$repo/contents/$markdown?ref=$ref" > "$fixtures/$name.html"
done

if [ ${#names[@]} -eq 0 ]; then
  printf 'repository: %s\nref: %s\nrecorded: %s\n' "$repo" "$ref" "$(date -u +%Y-%m-%dT%H:%M:%SZ)" \
    > "$fixtures/recorded.txt"
fi
