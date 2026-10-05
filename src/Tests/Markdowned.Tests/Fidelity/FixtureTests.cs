using Pure.Primitives.Abstractions.String;
using String = Pure.Primitives.String.String;

namespace Markdowned.Tests.Fidelity;

public sealed record FixtureTests
{
    private static string Read(string name, string extension)
    {
        return File.ReadAllText(
            Path.Combine(AppContext.BaseDirectory, "Fidelity", name + extension)
        );
    }

    private static IString Normalized(string html)
    {
        return new NormalizedHtml(new String(html));
    }

    [Fact]
    public void HasFixtures()
    {
        Assert.NotEmpty(new FidelityFixtures());
    }

    [Theory]
    [ClassData(typeof(FidelityFixtures))]
    public void RecordsGitHubHtmlOfEveryFixture(string name)
    {
        string recorded = Read(name, ".html");

        Assert.Contains("<article class=\"markdown-body", recorded);
        Assert.NotEmpty(Normalized(recorded).TextValue);
    }

    [Fact]
    public void RecordsProvenance()
    {
        string provenance = File.ReadAllText(
            Path.Combine(AppContext.BaseDirectory, "Fidelity", "recorded.txt")
        );

        Assert.Contains("ref: ", provenance);
    }

    [Fact]
    public void KeepsOnlyArticleContent()
    {
        string text = Normalized(Read("blocks", ".html")).TextValue;

        Assert.DoesNotContain("<article", text);
        Assert.StartsWith("<p dir=\"auto\">", text);
    }

    [Fact]
    public void IgnoresAttributeOrderAndWhitespace()
    {
        Assert.Equal(
            Normalized("<p class=\"a\" id=\"b\">x   y</p>").TextValue,
            Normalized("<p id=\"b\"   class=\"a\">\n  x y\n</p>").TextValue
        );
    }

    [Fact]
    public void UnwrapsHighlightSpans()
    {
        Assert.Equal(
            Normalized("<pre><code>const a</code></pre>").TextValue,
            Normalized(
                "<pre><code><span class=\"pl-k\">const</span> <span class=\"pl-s1\">a</span></code></pre>"
            ).TextValue
        );
    }

    [Fact]
    public void KeepsNonHighlightSpans()
    {
        Assert.Contains(
            "<span",
            Normalized("<p><span class=\"x\">a</span></p>").TextValue
        );
    }

    [Fact]
    public void RestoresCanonicalImageSource()
    {
        string text = Normalized(
            "<img src=\"https://camo.githubusercontent.com/x\" data-canonical-src=\"https://a.b/c.png\">"
        ).TextValue;

        Assert.Contains("src=\"https://a.b/c.png\"", text);
        Assert.DoesNotContain("camo", text);
    }

    [Fact]
    public void DropsRandomSuffixes()
    {
        Assert.Equal(
            Normalized("<a href=\"#fn-1\">1</a>").TextValue,
            Normalized(
                "<a href=\"#fn-1-0123456789abcdef0123456789abcdef\">1</a>"
            ).TextValue
        );
    }

    [Fact]
    public void KeepsSpacingInsidePreformattedText()
    {
        Assert.Contains(
            "\"a   b\\n\"",
            Normalized("<pre><code>a   b\n</code></pre>").TextValue
        );
    }

    [Fact]
    public void KeepsSpaceBetweenInlineElements()
    {
        Assert.Contains("\" \"", Normalized("<p><b>a</b> <i>b</i></p>").TextValue);
    }

    [Fact]
    public void DropsSpaceAroundBlocksAndBreaks()
    {
        string text = Normalized(
            "<ul>\n<li>a <br> b\n<ul><li>c</li></ul>\n</li>\n</ul>"
        ).TextValue;

        Assert.Contains("\"a\"", text);
        Assert.Contains("\"b\"", text);
    }

    [Fact]
    public void EnumeratesCharacters()
    {
        IString html = Normalized("<p>x</p>");

        Assert.Equal(html.TextValue.Length, html.Count());
    }
}
