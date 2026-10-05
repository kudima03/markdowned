#!/usr/bin/env bash
# Pins the newest stable chrome-headless-shell: rewrites src/Markdowned/Browser/browser.json with
# the version from Chrome for Testing and the SHA-256 of each release platform's archive.
# Chrome for Testing publishes no checksums, so the archives are downloaded and hashed.
#
# Usage: scripts/bump-chrome.sh [--check]   (--check only prints the newest version)
set -euo pipefail

cd "$(dirname "$0")/.."

pin=src/Markdowned/Browser/browser.json
base=https://storage.googleapis.com/chrome-for-testing-public
latest=$(curl -fsS https://googlechromelabs.github.io/chrome-for-testing/last-known-good-versions.json \
  | jq -r '.channels.Stable.version')
current=$(jq -r '.version' "$pin")

if [ "${1:-}" = "--check" ]; then
  echo "pinned $current, newest $latest"
  exit 0
fi

if [ "$latest" = "$current" ]; then
  echo "already at $current"
  exit 0
fi

work=$(mktemp -d)
trap 'rm -rf "$work"' EXIT
hashes='{}'

for platform in $(jq -r '.sha256 | keys[]' "$pin"); do
  echo "hashing $platform $latest" >&2
  curl -fsS -o "$work/$platform.zip" "$base/$latest/$platform/chrome-headless-shell-$platform.zip"
  hash=$(sha256sum "$work/$platform.zip" | cut -d' ' -f1)
  hashes=$(jq --arg p "$platform" --arg h "$hash" '. + {($p): $h}' <<< "$hashes")
  rm "$work/$platform.zip"
done

jq --arg v "$latest" --argjson h "$hashes" '.version = $v | .sha256 = $h' "$pin" > "$work/browser.json"
mv "$work/browser.json" "$pin"
echo "pinned $latest"
