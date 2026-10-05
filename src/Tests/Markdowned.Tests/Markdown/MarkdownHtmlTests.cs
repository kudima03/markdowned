using Markdowned.Abstractions.Markdown;
using Markdowned.Markdown;
using String = Pure.Primitives.String.String;

namespace Markdowned.Tests.Markdown;

public sealed record MarkdownHtmlTests
{
    [Fact]
    public void RendersHeading()
    {
        IHtml html = new MarkdownHtml(new String("# Title"));

        Assert.Contains("<h1", html.TextValue);
    }

    [Fact]
    public void RendersTable()
    {
        IHtml html = new MarkdownHtml(new String("|a|\n|-|\n|1|"));

        Assert.Contains("<table>", html.TextValue);
    }
}
