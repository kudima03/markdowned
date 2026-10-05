using System.Globalization;
using System.Text;
using Pure.Primitives.Abstractions.Char;
using Pure.Primitives.Abstractions.String;
using Char = Pure.Primitives.Char.Char;

namespace Markdowned.Markdown;

/// <summary>GitHub's heading id: lower case, letters, numbers, marks, '_' and '-' kept.</summary>
public sealed record HeadingSlug : IString
{
    private readonly IString _text;

    public HeadingSlug(IString text)
    {
        _text = text;
    }

    public string TextValue
    {
        get
        {
            StringBuilder slug = new StringBuilder();

            foreach (char symbol in _text.TextValue.ToLowerInvariant())
            {
                UnicodeCategory category = char.GetUnicodeCategory(symbol);

                if (symbol == ' ')
                {
                    _ = slug.Append('-');
                }
                else if (
                    symbol is '-' or '_'
                    || char.IsLetterOrDigit(symbol)
                    || category
                        is UnicodeCategory.NonSpacingMark
                            or UnicodeCategory.SpacingCombiningMark
                            or UnicodeCategory.EnclosingMark
                            or UnicodeCategory.ConnectorPunctuation
                )
                {
                    _ = slug.Append(symbol);
                }
            }

            return slug.ToString();
        }
    }

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
