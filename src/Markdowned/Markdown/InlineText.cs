using System.Text;
using Markdig.Syntax.Inlines;
using Pure.Primitives.Abstractions.Char;
using Pure.Primitives.Abstractions.String;
using Char = Pure.Primitives.Char.Char;

namespace Markdowned.Markdown;

public sealed record InlineText : IString
{
    private readonly ContainerInline? _inline;

    public InlineText(ContainerInline? inline)
    {
        _inline = inline;
    }

    public string TextValue
    {
        get
        {
            StringBuilder text = new StringBuilder();

            foreach (Inline child in _inline ?? Enumerable.Empty<Inline>())
            {
                _ = child switch
                {
                    LiteralInline literal => text.Append(literal.Content.ToString()),
                    CodeInline code => text.Append(code.Content),
                    HtmlEntityInline entity => text.Append(entity.Transcoded.ToString()),
                    LineBreakInline => text.Append(' '),
                    ContainerInline container => text.Append(
                        new InlineText(container).TextValue
                    ),
                    _ => text,
                };
            }

            return text.ToString();
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
