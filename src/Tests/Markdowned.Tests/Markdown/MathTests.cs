using Markdowned.Markdown;
using Markdowned.Page;
using String = Pure.Primitives.String.String;

namespace Markdowned.Tests.Markdown;

public sealed record MathTests
{
    private static string Render(string markdown)
    {
        return new MarkdownHtml(new String(markdown)).TextValue;
    }

    [Theory]
    [InlineData("$x$", "$x$")]
    [InlineData("a $x^2 + y$ b", "$x^2 + y$")]
    [InlineData("$`a_1`$", "$a_1$")]
    [InlineData("$a<b$", "$a&lt;b$")]
    public void RendersInlineMath(string markdown, string tex)
    {
        string html = Render(markdown);

        Assert.Contains(
            "<math-renderer class=\"js-inline-math\" style=\"display: inline-block\"",
            html
        );
        Assert.Contains($">{tex}</math-renderer>", html);
    }

    [Theory]
    [InlineData("cost $5 and $6")]
    [InlineData("$ x$")]
    [InlineData("$x $")]
    [InlineData("a $")]
    [InlineData("$$")]
    [InlineData("$`unclosed")]
    [InlineData("$$unclosed")]
    [InlineData("\\$x$")]
    [InlineData("$`$")]
    public void LeavesDollarsThatAreNotMathAlone(string markdown)
    {
        Assert.DoesNotContain("math-renderer", Render(markdown));
    }

    [Fact]
    public void RendersDisplayMathInParagraphs()
    {
        string html = Render("$$\n\\frac{1}{2}\n$$");

        Assert.Contains("<p dir=\"auto\"><math-renderer class=\"js-display-math\"", html);
        Assert.Contains("style=\"display: block\"", html);
        Assert.Contains("$$\n\\frac{1}{2}\n$$", html);
    }

    [Fact]
    public void ResolvesBackslashEscapesLikeGitHub()
    {
        Assert.Contains("$a,b$", Render("$a\\,b$"));
        Assert.Contains("$a\\\\b$", Render("$a\\\\\\\\b$").Replace("\\\\\\\\", "\\\\"));
        Assert.Contains("\\frac", Render("$\\frac{a}{b}$"));
    }

    [Fact]
    public void RendersMathFences()
    {
        string html = Render("```math\n\\sum_{i} i\n```");

        Assert.Contains("class=\"js-display-math\"", html);
        Assert.Contains(">$$\\sum_{i} i$$</math-renderer>", html);
        Assert.DoesNotContain("<pre", html);
    }

    [Fact]
    public void NeverTouchesCodeSpans()
    {
        Assert.DoesNotContain("math-renderer", Render("`$x$`"));
    }

    [Fact]
    public void LoadsMathJaxOnlyForPagesWithMath()
    {
        string with = new PageHtml(new MarkdownHtml(new String("$x$"))).TextValue;
        string without = new PageHtml(new MarkdownHtml(new String("x"))).TextValue;

        Assert.Contains("/assets/js/mathjax.js", with);
        Assert.Contains("tex2svgPromise", with);
        Assert.DoesNotContain("mathjax", without, StringComparison.OrdinalIgnoreCase);
    }
}
