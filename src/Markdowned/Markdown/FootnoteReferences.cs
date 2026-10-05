namespace Markdowned.Markdown;

/// <summary>Per-document footnote bookkeeping: GitHub's id suffix and reference counts.</summary>
public sealed class FootnoteReferences
{
    private readonly Dictionary<string, int> _counts = new Dictionary<string, int>(
        StringComparer.Ordinal
    );

    public string Suffix { get; } = "a54e445bb694be13f72d1195ac423c7d";

    private readonly Dictionary<string, int> _numbers = new Dictionary<string, int>(
        StringComparer.Ordinal
    );

    /// <summary>Footnote number: the order in which footnotes are first referenced.</summary>
    public int Number(string label)
    {
        if (!_numbers.TryGetValue(label, out int number))
        {
            number = _numbers.Count + 1;
            _numbers[label] = number;
        }

        return number;
    }

    public bool IsReferenced(string label)
    {
        return _numbers.ContainsKey(label);
    }

    /// <summary>Number of the next reference to the footnote, counting from 1.</summary>
    public int Next(string label)
    {
        _counts[label] = _counts.GetValueOrDefault(label) + 1;

        return _counts[label];
    }

    public string ReferenceId(string label, int occurrence)
    {
        return occurrence == 1
            ? $"user-content-fnref-{label}-{Suffix}"
            : $"user-content-fnref-{label}-{occurrence}-{Suffix}";
    }
}
