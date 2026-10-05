using Markdowned.Abstractions.Markdown;
using Pure.Primitives.Abstractions.Char;
using Char = Pure.Primitives.Char.Char;

namespace Markdowned.Page;

public sealed record HtmlDocument : IHtml
{
    private readonly IHtml _body;

    public HtmlDocument(IHtml body)
    {
        _body = body;
    }

    public string TextValue =>
        "<!doctype html><html><head><meta charset=\"utf-8\"></head><body>"
        + _body.TextValue
        + "</body></html>";

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
