using Markdowned.Abstractions.Markdown;
using Markdowned.Markdown;
using String = Pure.Primitives.String.String;

namespace Markdowned.Tests.Markdown;

public sealed record MarkdownHtmlTests
{
    private static string Render(string markdown)
    {
        return new MarkdownHtml(new String(markdown)).TextValue;
    }

    [Fact]
    public void RendersHeadingWithAnchor()
    {
        string html = Render("# Title");

        Assert.Contains("<div class=\"markdown-heading\" dir=\"auto\">", html);
        Assert.Contains("id=\"user-content-title\"", html);
        Assert.Contains("href=\"#title\"", html);
    }

    [Fact]
    public void NumbersDuplicateHeadings()
    {
        string html = Render("# A\n\n# A\n\n# A");

        Assert.Contains("id=\"user-content-a\"", html);
        Assert.Contains("id=\"user-content-a-1\"", html);
        Assert.Contains("id=\"user-content-a-2\"", html);
    }

    [Fact]
    public void RendersTableWithAlignment()
    {
        string html = Render("|a|b|c|\n|:-|:-:|-:|\n|1|2|3|");

        Assert.Contains("<markdown-accessiblity-table>", html);
        Assert.Contains("<th align=\"left\">a</th>", html);
        Assert.Contains("<td align=\"center\">2</td>", html);
        Assert.Contains("<td align=\"right\">3</td>", html);
    }

    [Fact]
    public void RendersTableWithoutBody()
    {
        string html = Render("|a|b|\n|-|-|");

        Assert.Contains("<thead>", html);
        Assert.DoesNotContain("<tbody>", html);
    }

    [Fact]
    public void StartsOrderedListAtItsNumber()
    {
        Assert.Contains("<ol dir=\"auto\" start=\"3\">", Render("3. a\n4. b"));
    }

    [Fact]
    public void KeepsTightListItemsWithoutParagraphs()
    {
        string html = Render("- a\n- b");

        Assert.Contains("<li>a</li>", html);
    }

    [Fact]
    public void WrapsLooseListItemsInParagraphs()
    {
        Assert.Contains(
            "<li>\n<p dir=\"auto\">a</p>",
            Render("- a\n\n- b").Replace("\r", "")
        );
    }

    [Fact]
    public void RendersTaskListItems()
    {
        string html = Render("- [x] done\n- [ ] todo");

        Assert.Contains("<ul class=\"contains-task-list\">", html);
        Assert.Contains("aria-label=\"Completed task\" checked=\"\"", html);
        Assert.Contains("aria-label=\"Incomplete task\">", html);
        Assert.Contains("<li class=\"task-list-item\">", html);
    }

    [Fact]
    public void MarksExternalLinksNofollow()
    {
        string html = Render("[a](https://x.y) [b](docs/b.md)");

        Assert.Contains("<a href=\"https://x.y\" rel=\"nofollow\">a</a>", html);
        Assert.Contains("<a href=\"docs/b.md\">b</a>", html);
    }

    [Fact]
    public void KeepsLinkTitle()
    {
        Assert.Contains("title=\"T\"", Render("[a](https://x.y \"T\")"));
    }

    [Fact]
    public void WrapsStandaloneRemoteImageInLink()
    {
        string html = Render("![alt](https://x.y/i.png \"T\")");

        Assert.Contains(
            "<a target=\"_blank\" rel=\"noopener noreferrer nofollow\" href=\"https://x.y/i.png\">",
            html
        );
        Assert.Contains("alt=\"alt\" title=\"T\" style=\"max-width: 100%;\">", html);
    }

    [Fact]
    public void DoesNotWrapImageInsideLink()
    {
        string html = Render("[![alt](https://x.y/i.png)](https://x.y)");

        Assert.DoesNotContain("target=\"_blank\"", html);
    }

    [Fact]
    public void DoesNotWrapLocalImage()
    {
        string html = Render("![alt](img/a.png)");

        Assert.DoesNotContain("<a ", html);
        Assert.Contains("<img src=\"img/a.png\"", html);
    }

    [Fact]
    public void LinksAngleBracketAutolinks()
    {
        Assert.Contains(
            "<a href=\"https://x.y/z\" rel=\"nofollow\">https://x.y/z</a>",
            Render("<https://x.y/z>")
        );
        Assert.Contains("<a href=\"mailto:a@b.c\">a@b.c</a>", Render("<a@b.c>"));
    }

    [Fact]
    public void LinksBareEmailAddresses()
    {
        Assert.Contains(
            "text <a href=\"mailto:first.last+tag@sub.example.com\">first.last+tag@sub.example.com</a>.",
            Render("text first.last+tag@sub.example.com.")
        );
    }

    [Theory]
    [InlineData("user@")]
    [InlineData("@user")]
    [InlineData("user@localhost")]
    [InlineData("user@a..b")]
    [InlineData("@@")]
    public void LeavesNonEmailsAlone(string text)
    {
        Assert.DoesNotContain("mailto:", Render(text));
    }

    [Fact]
    public void RendersCodeBlockWithClipboardSource()
    {
        string html = Render("```\na < b\n```");

        Assert.Contains("data-snippet-clipboard-copy-content=\"a &lt; b\"", html);
        Assert.Contains("<pre class=\"notranslate\"><code>a &lt; b\n</code></pre>", html);
    }

    [Fact]
    public void RendersEmptyCodeBlock()
    {
        Assert.Contains("<code></code>", Render("```\n```"));
    }

    [Fact]
    public void RendersStrikethrough()
    {
        Assert.Contains("<del>x</del>", Render("~~x~~"));
    }

    [Fact]
    public void RendersParagraphWithDirAuto()
    {
        IHtml html = new MarkdownHtml(new String("text"));

        Assert.Contains("<p dir=\"auto\">text</p>", html.TextValue);
        Assert.Equal(html.TextValue.Length, html.Count());
    }
}

public sealed record SlugTests
{
    [Theory]
    [InlineData("Heading One", "heading-one")]
    [InlineData("it's (really) a \"test\" & more!", "its-really-a-test--more")]
    [InlineData("Ünïcödé", "ünïcödé")]
    [InlineData("snake_case-kebab", "snake_case-kebab")]
    [InlineData("a.b", "ab")]
    [InlineData("é", "é")]
    public void FollowsGitHub(string text, string expected)
    {
        Assert.Equal(expected, new HeadingSlug(new String(text)).TextValue);
    }

    [Fact]
    public void EnumeratesCharacters()
    {
        Assert.Equal(3, new HeadingSlug(new String("a b")).Count());
    }
}

public sealed record InlineTextTests
{
    private static string Heading(string markdown)
    {
        string html = new MarkdownHtml(new String(markdown)).TextValue;
        const string marker = "aria-label=\"Permalink: ";
        int start = html.IndexOf(marker, StringComparison.Ordinal) + marker.Length;

        return html[start..html.IndexOf('"', start)];
    }

    [Fact]
    public void FlattensFormatting()
    {
        Assert.Equal("bold code link", Heading("# **bold** `code` [link](https://x.y)"));
    }

    [Fact]
    public void DecodesEntities()
    {
        Assert.Equal("a &amp; b", Heading("# a &amp; b"));
    }

    [Fact]
    public void SeparatesSoftBreaksWithSpace()
    {
        Assert.Matches("^a[ \n]b$", Heading("a  \nb\n---"));
    }

    [Fact]
    public void SkipsInlineHtml()
    {
        Assert.Equal("ab", Heading("# a<br>b"));
    }

    [Fact]
    public void HandlesMissingInline()
    {
        Assert.Empty(new InlineText(null).TextValue);
        Assert.Empty(new InlineText(null));
    }
}
