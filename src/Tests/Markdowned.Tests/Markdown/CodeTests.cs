using Markdowned.Markdown;
using Markdowned.Page;
using String = Pure.Primitives.String.String;

namespace Markdowned.Tests.Markdown;

public sealed record CodeTests
{
    private static string Render(string markdown)
    {
        return new MarkdownHtml(new String(markdown)).TextValue;
    }

    [Theory]
    [InlineData("js", "source.js")]
    [InlineData("JavaScript", "source.js")]
    [InlineData("csharp", "source.cs")]
    [InlineData("c++", "source.c++")]
    [InlineData("json", "source.json")]
    [InlineData("sh", "source.shell")]
    [InlineData("yml", "source.yaml")]
    [InlineData("js {1} extra", "source.js")]
    [InlineData("  js", "source.js")]
    [InlineData("main.go", "source.go")]
    public void FindsTheScopeOfALanguage(string info, string scope)
    {
        Assert.Equal(scope, new CodeLanguage(info).TextValue);
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("unknownlang")]
    public void KnowsNothingOfOtherLanguages(string info)
    {
        Assert.Empty(new CodeLanguage(info).TextValue);
        Assert.Empty(new CodeLanguage(info));
    }

    [Fact]
    public void MarksHighlightedBlocksForTheClient()
    {
        string html = Render("```js\nlet a < 1;\n```");

        Assert.Contains("<div class=\"highlight highlight-source-js notranslate", html);
        Assert.Contains("data-markdowned-scope=\"source.js\"", html);
        Assert.Contains("data-snippet-clipboard-copy-content=\"let a &lt; 1;\"", html);
        Assert.Contains("<pre>let a &lt; 1;</pre>", html);
    }

    [Fact]
    public void KeepsUnknownLanguagesAsPlainBlocks()
    {
        string html = Render("```nothing extra\nx\n```");

        Assert.Contains(
            "<pre lang=\"nothing\" class=\"notranslate\"><code>x\n</code></pre>",
            html
        );
        Assert.DoesNotContain("data-markdowned-scope", html);
    }

    [Fact]
    public void KeepsIndentedBlocksPlain()
    {
        Assert.Contains(
            "<pre class=\"notranslate\"><code>x\n</code></pre>",
            Render("    x")
        );
    }

    [Fact]
    public void LoadsStarryNightOnlyThroughThePage()
    {
        string html = new PageHtml(
            new MarkdownHtml(new String("```js\nx\n```"))
        ).TextValue;

        Assert.Contains("type=\"module\"", html);
        Assert.Contains("/assets/js/starry-night/index.js", html);
        Assert.Contains("window.markdownedTasks", html);
    }
}
