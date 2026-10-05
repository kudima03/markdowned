using System.Text;
using Markdowned.Abstractions.Page;
using Markdowned.Markdown;
using Markdowned.Page;
using String = Pure.Primitives.String.String;

namespace Markdowned.Tests.Page;

public sealed record PageTests
{
    [Fact]
    public void WrapsBodyInGitHubArticle()
    {
        string html = new PageHtml(new MarkdownHtml(new String("text"))).TextValue;

        Assert.StartsWith("<!doctype html>", html);
        Assert.Contains("<article class=\"markdown-body entry-content\">", html);
        Assert.Contains("<p dir=\"auto\">text</p>", html);
        Assert.Contains("/assets/github-markdown-light.css", html);
        Assert.Contains("window.markdownedReady", html);
        Assert.Equal(
            html.Length,
            new PageHtml(new MarkdownHtml(new String("text"))).Count()
        );
    }

    [Fact]
    public void EmbedsStylesheetsAndEveryFont()
    {
        string[] paths =
        [
            .. new EmbeddedResources().Select(resource => resource.Path.TextValue),
        ];

        Assert.Contains("assets/github-markdown-light.css", paths);
        Assert.Contains("assets/fonts.css", paths);
        Assert.Contains("assets/page.css", paths);
        Assert.Contains("assets/js/mathjax.js", paths);
        Assert.Contains("assets/js/mermaid.js", paths);
        Assert.Contains("assets/js/starry-night/index.js", paths);
        Assert.Contains("assets/js/starry-night/onig.wasm", paths);

        foreach (
            string font in new[]
            {
                "NotoSans-Regular",
                "NotoSans-Bold",
                "NotoSans-Italic",
                "NotoSans-BoldItalic",
                "LiberationMono-Regular",
                "LiberationMono-Bold",
                "LiberationMono-Italic",
                "LiberationMono-BoldItalic",
                "NotoColorEmoji",
            }
        )
        {
            Assert.Contains($"assets/fonts/{font}.ttf", paths);
        }
    }

    [Fact]
    public void DeclaresEveryEmbeddedFontInFontFaces()
    {
        string css = Encoding.UTF8.GetString(
            new EmbeddedResources()
                .Single(resource => resource.Path.TextValue == "assets/fonts.css")
                .Content
        );

        foreach (
            IPageResource font in new EmbeddedResources().Where(resource =>
                resource.Path.TextValue.EndsWith(".ttf", StringComparison.Ordinal)
            )
        )
        {
            Assert.Contains($"/{font.Path.TextValue}", css);
        }
    }

    [Theory]
    [InlineData("a.html", "text/html; charset=utf-8")]
    [InlineData("a.css", "text/css; charset=utf-8")]
    [InlineData("a.js", "text/javascript; charset=utf-8")]
    [InlineData("a.mjs", "text/javascript; charset=utf-8")]
    [InlineData("a.json", "application/json; charset=utf-8")]
    [InlineData("a.svg", "image/svg+xml")]
    [InlineData("a.PNG", "image/png")]
    [InlineData("a.jpg", "image/jpeg")]
    [InlineData("a.jpeg", "image/jpeg")]
    [InlineData("a.gif", "image/gif")]
    [InlineData("a.webp", "image/webp")]
    [InlineData("a.ttf", "font/ttf")]
    [InlineData("a.woff2", "font/woff2")]
    [InlineData("a.wasm", "application/wasm")]
    [InlineData("a.bin", "application/octet-stream")]
    public void KnowsContentTypes(string path, string expected)
    {
        Assert.Equal(expected, new ContentType(new String(path)).TextValue);
        Assert.Equal(expected.Length, new ContentType(new String(path)).Count());
    }

    [Fact]
    public void ServesHtmlAsIndex()
    {
        IPageResource resource = new HtmlResource(
            new PageHtml(new MarkdownHtml(new String("x")))
        );

        Assert.Equal("index.html", resource.Path.TextValue);
        Assert.Equal("text/html; charset=utf-8", resource.ContentType.TextValue);
        Assert.StartsWith("<!doctype html>", Encoding.UTF8.GetString(resource.Content));
    }

    [Fact]
    public void CombinesResources()
    {
        IPageResources combined = new CombinedResources(
            new EmbeddedResources(),
            new SingleResource(
                new HtmlResource(new PageHtml(new MarkdownHtml(new String("x"))))
            )
        );

        Assert.Contains(combined, resource => resource.Path.TextValue == "index.html");
        Assert.Contains(
            combined,
            resource => resource.Path.TextValue == "assets/page.css"
        );
    }

    [Fact]
    public void FailsOnUnknownEmbeddedResource()
    {
        _ = Assert.Throws<InvalidOperationException>(() =>
            new EmbeddedResource("assets/missing").Content
        );
    }
}
