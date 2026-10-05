using Markdowned.Abstractions.Markdown;
using Markdowned.Markdown;
using Markdowned.Page;
using String = Pure.Primitives.String.String;

namespace Markdowned.Tests.Page;

public sealed record HtmlDocumentTests
{
    [Fact]
    public void WrapsBodyInDocument()
    {
        IHtml html = new HtmlDocument(new MarkdownHtml(new String("text")));

        Assert.StartsWith("<!doctype html>", html.TextValue);
        Assert.Contains("<p>text</p>", html.TextValue);
        Assert.Equal(html.TextValue.Length, html.Count());
    }
}
