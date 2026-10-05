using Markdowned.Markdown;
using String = Pure.Primitives.String.String;

namespace Markdowned.Tests.Markdown;

public sealed record ExtensionTests
{
    private static string Render(string markdown)
    {
        return new MarkdownHtml(new String(markdown)).TextValue;
    }

    private static string FrontMatter(string yaml)
    {
        return new FrontMatterTable(new String(yaml)).TextValue;
    }

    [Theory]
    [InlineData("NOTE", "note", "Note")]
    [InlineData("tip", "tip", "Tip")]
    [InlineData("IMPORTANT", "important", "Important")]
    [InlineData("WARNING", "warning", "Warning")]
    [InlineData("CAUTION", "caution", "Caution")]
    public void RendersAlertWithIconAndTitle(string marker, string kind, string title)
    {
        string html = Render($"> [!{marker}]\n> text");

        Assert.Contains(
            $"<div class=\"markdown-alert markdown-alert-{kind}\" dir=\"auto\">",
            html
        );
        Assert.Contains($"octicon octicon-", html);
        Assert.Contains($"</svg>{title}</p>", html);
        Assert.Contains("<p dir=\"auto\">text</p>", html);
    }

    [Fact]
    public void KeepsUnknownAlertAsQuote()
    {
        string html = Render("> [!FOO]\n> text\n>\n> more");

        Assert.Contains("<blockquote>", html);
        Assert.Contains("[!FOO]", html);
        Assert.Contains("text</p>", html);
        Assert.Contains("<p dir=\"auto\">more</p>", html);
        Assert.DoesNotContain("markdown-alert", html);
    }

    [Fact]
    public void RejectsUnknownIconKind()
    {
        _ = Assert.Throws<ArgumentException>(() => new AlertIcon("foo").TextValue);
        Assert.NotEmpty(new AlertIcon("note").TextValue);
        Assert.Equal(new AlertIcon("tip").TextValue.Length, new AlertIcon("tip").Count());
    }

    [Fact]
    public void NumbersFootnotesByFirstReference()
    {
        string html = Render("b[^b] a[^a] b2[^b]\n\n[^a]: A\n[^b]: B");

        Assert.Contains("fn-b-", html);
        Assert.Matches("fnref-b-[0-9a-f]{32}\"[^>]*>1</a>", html);
        Assert.Matches("fnref-a-[0-9a-f]{32}\"[^>]*>2</a>", html);
        Assert.Matches("fnref-b-2-[0-9a-f]{32}\"[^>]*>1</a>", html);
        Assert.Contains("Back to reference 1-2", html);
        Assert.Contains("↩<sup>2</sup>", html);
    }

    [Fact]
    public void OmitsUnreferencedFootnotes()
    {
        string html = Render("text\n\n[^x]: never used");

        Assert.DoesNotContain("never used", html);
    }

    [Fact]
    public void PutsBackReferenceInOwnParagraphAfterNonParagraphBlocks()
    {
        string html = Render("t[^c]\n\n[^c]: intro\n\n    - item");

        Assert.Contains("<p dir=\"auto\"><a href=\"#user-content-fnref-c-", html);
    }

    [Fact]
    public void RendersFrontMatterTable()
    {
        string html = Render("---\ntitle: T\n---\n\n# H");

        Assert.Contains("<markdown-accessiblity-table>", html);
        Assert.Contains("<th>title</th>", html);
        Assert.Contains("<td>T</td>", html);
        Assert.Contains("markdown-heading", html);
    }

    [Fact]
    public void RendersBlockListsAndQuotedValues()
    {
        string html = FrontMatter(
            "# comment\nname: \"Quoted & <b>\"\nsingle: 'x'\nitems:\n  - one\n  - two\nempty:\nlist: [ a , 'b' ]"
        );

        Assert.Contains("<td>Quoted &amp; &lt;b&gt;</td>", html);
        Assert.Contains("<td>x</td>", html);
        Assert.Contains("<td><div dir=\"auto\">one</div></td>", html);
        Assert.Contains("<td><div dir=\"auto\">two</div></td>", html);
        Assert.Contains("<td></td>", html);
        Assert.Contains("<td><div dir=\"auto\">b</div></td>", html);
    }

    [Fact]
    public void RendersOneLevelOfMaps()
    {
        string html = FrontMatter("author:\n  name: Ada\n  role: dev");

        Assert.Contains("<thead>", html);
        Assert.Contains("<th>name</th>", html);
        Assert.Contains("<th>role</th>", html);
        Assert.Contains("<td><div dir=\"auto\">Ada</div></td>", html);
    }

    [Fact]
    public void IgnoresLinesThatAreNotKeys()
    {
        string html = FrontMatter("not a key\nurl: http://x.y\n");

        Assert.DoesNotContain("not a key", html);
        Assert.Contains("<td>http://x.y</td>", html);
        Assert.Equal(
            html.Length,
            new FrontMatterTable(new String("not a key\nurl: http://x.y\n")).Count()
        );
    }

    [Fact]
    public void ConvertsShortcodesToEmoji()
    {
        Assert.Contains("😄 👍 🚀", Render(":smile: :+1: :rocket:"));
    }

    [Fact]
    public void LeavesUnknownAndNonAlphanumericShortcodesAlone()
    {
        string html = Render(":notanemoji: :-1: `:smile:`");

        Assert.Contains(":notanemoji: :-1:", html);
        Assert.Contains("<code>:smile:</code>", html);
    }
}
