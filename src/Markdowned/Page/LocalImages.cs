using Markdowned.Abstractions.Page;
using Pure.Primitives.Abstractions.String;

namespace Markdowned.Page;

/// <summary>
/// Image files below a directory, served at their relative path. Nothing outside the directory
/// and nothing that is not an image is ever served.
/// </summary>
public sealed record LocalImages : IResourceLookup
{
    private static readonly string[] Extensions =
    [
        ".png",
        ".jpg",
        ".jpeg",
        ".gif",
        ".webp",
        ".svg",
        ".bmp",
        ".ico",
        ".avif",
    ];

    private readonly IString _directory;

    public LocalImages(IString directory)
    {
        _directory = directory;
    }

    public IPageResource? this[IString path]
    {
        get
        {
            string root = Path.GetFullPath(_directory.TextValue);
            string relative = Uri.UnescapeDataString(path.TextValue).TrimStart('/');
            string file = Path.GetFullPath(Path.Combine(root, relative));
            string prefix =
                root.TrimEnd(Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;

            return
                relative.Length > 0
                && Extensions.Contains(
                    Path.GetExtension(file),
                    StringComparer.OrdinalIgnoreCase
                )
                && file.StartsWith(prefix, StringComparison.Ordinal)
                && File.Exists(file)
                && IsInside(file, prefix)
                ? new FileResource(path.TextValue, file)
                : null;
        }
    }

    private static bool IsInside(string file, string prefix)
    {
        string? target = new FileInfo(file).ResolveLinkTarget(true)?.FullName;

        return target is null
            || Path.GetFullPath(target).StartsWith(prefix, StringComparison.Ordinal);
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
