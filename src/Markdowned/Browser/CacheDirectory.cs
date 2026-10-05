using Pure.Primitives.Abstractions.Char;
using Pure.Primitives.Abstractions.String;
using Char = Pure.Primitives.Char.Char;

namespace Markdowned.Browser;

public sealed record CacheDirectory : IString
{
    public string TextValue =>
        Path.Combine(
            OperatingSystem.IsWindows()
                    ? Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData
                    )
                : OperatingSystem.IsMacOS()
                    ? Path.Combine(
                        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                        "Library",
                        "Caches"
                    )
                : Environment.GetEnvironmentVariable("XDG_CACHE_HOME")
                    is { Length: > 0 } xdg
                    ? xdg
                : Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                    ".cache"
                ),
            "markdowned"
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
