using Markdowned.Abstractions.Browser;
using Pure.Primitives.Abstractions.Char;
using Pure.Primitives.Abstractions.String;
using Char = Pure.Primitives.Char.Char;

namespace Markdowned.Browser;

public sealed record CachedBrowser : IString
{
    private readonly IString _cache;

    private readonly IBrowserDownload _download;

    public CachedBrowser(IString cache, IBrowserDownload download)
    {
        _cache = cache;
        _download = download;
    }

    public string TextValue =>
        Path.Combine(
            _cache.TextValue,
            "chrome-headless-shell",
            _download.Version.TextValue,
            _download.Platform.TextValue,
            $"chrome-headless-shell-{_download.Platform.TextValue}",
            _download.Platform.TextValue == "win64"
                ? "chrome-headless-shell.exe"
                : "chrome-headless-shell"
        );

    public IEnumerator<IChar> GetEnumerator()
    {
        return TextValue.Select(symbol => new Char(symbol)).Cast<IChar>().GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
