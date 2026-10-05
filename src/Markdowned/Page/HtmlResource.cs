using System.Text;
using Markdowned.Abstractions.Markdown;
using Markdowned.Abstractions.Page;
using Pure.Primitives.Abstractions.String;
using String = Pure.Primitives.String.String;

namespace Markdowned.Page;

public sealed record HtmlResource : IPageResource
{
    private readonly IHtml _html;

    public HtmlResource(IHtml html)
    {
        _html = html;
    }

    public IString Path => new String("index.html");

    public IString ContentType => new ContentType(Path);

    public byte[] Content => Encoding.UTF8.GetBytes(_html.TextValue);

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
