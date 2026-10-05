namespace Markdowned.Markdown;

/// <summary>Per-document heading ids: GitHub appends -1, -2, … to repeated slugs.</summary>
public sealed class HeadingSlugs
{
    private readonly Dictionary<string, int> _seen = new Dictionary<string, int>(
        StringComparer.Ordinal
    );

    public string Unique(string slug)
    {
        if (!_seen.TryGetValue(slug, out int count))
        {
            _seen[slug] = 0;

            return slug;
        }

        _seen[slug] = count + 1;

        return $"{slug}-{count + 1}";
    }
}
