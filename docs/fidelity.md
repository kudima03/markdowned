# Fidelity to github.com

markdowned aims at PDFs that look as close to github.com as possible. This file lists the
known deviations; each one is deliberate and measured.

## How fidelity is measured

Fixtures in `src/Tests/Markdowned.Tests/Fidelity/*.md` are rendered by GitHub itself and the
HTML is recorded next to them with `scripts/record-fixtures.sh`. Our HTML is compared with the
recording after normalisation (`NormalizedHtml`): attribute order and insignificant whitespace
are ignored, the page wrapper is removed, `pl-*` highlight spans are unwrapped, camo image
URLs are mapped back to their canonical URL and random id suffixes are dropped.

## Known deviations

None recorded yet.
