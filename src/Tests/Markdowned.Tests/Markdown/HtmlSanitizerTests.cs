using Markdowned.Markdown;

namespace Markdowned.Tests.Markdown;

public sealed record HtmlSanitizerTests
{
    private static string Clean(string html)
    {
        return new HtmlSanitizer(new HeadingSlugs()).Sanitize(html);
    }

    [Theory]
    [InlineData("<script>alert(1)</script>", "&lt;script>alert(1)&lt;/script>")]
    [InlineData("<SCRIPT src=x>", "&lt;SCRIPT src=x>")]
    [InlineData("<style>p{}</style>", "&lt;style>p{}&lt;/style>")]
    [InlineData("<iframe src=x></iframe>", "&lt;iframe src=x>&lt;/iframe>")]
    [InlineData("<title>t</title>", "&lt;title>t&lt;/title>")]
    [InlineData("<textarea>x</textarea>", "&lt;textarea>x&lt;/textarea>")]
    [InlineData("<xmp>x</xmp>", "&lt;xmp>x&lt;/xmp>")]
    [InlineData("<plaintext>", "&lt;plaintext>")]
    public void EscapesFilteredTags(string html, string expected)
    {
        Assert.Equal(expected, Clean(html));
    }

    [Theory]
    [InlineData("<u>a</u>", "a")]
    [InlineData("<font color=red>a</font>", "a")]
    [InlineData("<button>a</button>", "a")]
    [InlineData("<form action=/x><input name=n></form>", "")]
    [InlineData("<object data=x></object>", "")]
    [InlineData("<unknown-tag>a</unknown-tag>", "a")]
    [InlineData("<svg onload=x></svg>", "")]
    public void DropsTagsOutsideTheAllowList(string html, string expected)
    {
        Assert.Equal(expected, Clean(html));
    }

    [Fact]
    public void RemovesCommentsAndDeclarations()
    {
        Assert.Equal("ab", Clean("a<!-- hidden -->b"));
        Assert.Equal("a", Clean("a<!-- unterminated"));
        Assert.Equal("ab", Clean("a<!DOCTYPE html>b"));
        Assert.Equal("ab", Clean("a<?php x ?>b"));
    }

    [Theory]
    [InlineData("<b onclick=\"x()\" onmouseover=y>a</b>", "<b>a</b>")]
    [InlineData("<b style=\"color:red\" class=\"c\">a</b>", "<b>a</b>")]
    [InlineData("<span data-x=1 aria-label=l role=r>a</span>", "<span>a</span>")]
    [InlineData("<span TITLE=\"t\">a</span>", "<span title=\"t\">a</span>")]
    [InlineData(
        "<span title='single \"quoted\"'>a</span>",
        "<span title=\"single &quot;quoted&quot;\">a</span>"
    )]
    [InlineData("<span title=bare>a</span>", "<span title=\"bare\">a</span>")]
    [InlineData("<details open>a</details>", "<details open=\"\">a</details>")]
    public void FiltersAttributes(string html, string expected)
    {
        Assert.Equal(expected, Clean(html));
    }

    [Theory]
    [InlineData("<a href=\"javascript:alert(1)\">x</a>")]
    [InlineData("<a href=\"JaVaScRiPt:alert(1)\">x</a>")]
    [InlineData("<a href=\"java\tscript:alert(1)\">x</a>")]
    [InlineData("<a href=\"&#106;avascript:alert(1)\">x</a>")]
    [InlineData("<a href=\" javascript:alert(1)\">x</a>")]
    [InlineData("<a href=javascript:alert(1)>x</a>")]
    [InlineData("<a href=\"data:text/html,x\">x</a>")]
    [InlineData("<a href=\"vbscript:x\">x</a>")]
    [InlineData("<a href=\"ftp://x.y\">x</a>")]
    public void DropsUnsafeLinks(string html)
    {
        Assert.Equal("x", Clean(html));
    }

    [Theory]
    [InlineData("https://x.y/z", "<a href=\"https://x.y/z\" rel=\"nofollow\">")]
    [InlineData("HTTP://x.y", "<a href=\"HTTP://x.y\" rel=\"nofollow\">")]
    [InlineData("mailto:a@b.c", "<a href=\"mailto:a@b.c\">")]
    [InlineData("#frag", "<a href=\"#frag\">")]
    [InlineData("rel/path:with:colon", "<a href=\"rel/path:with:colon\">")]
    [InlineData("?query:x", "<a href=\"?query:x\">")]
    public void KeepsSafeLinks(string url, string expected)
    {
        Assert.StartsWith(expected, Clean($"<a href=\"{url}\">x</a>"));
    }

    [Fact]
    public void PrefixesIdsAndNames()
    {
        Assert.Equal("<a name=\"user-content-n\">x</a>", Clean("<a name=\"n\">x</a>"));
        Assert.Equal("<a id=\"user-content-i\">x</a>", Clean("<a id=\"i\">x</a>"));
        Assert.Equal(
            "<a id=\"user-content-i\">x</a>",
            Clean("<a id=\"user-content-i\">x</a>")
        );
    }

    [Fact]
    public void DropsAnchorsWithoutDestination()
    {
        Assert.Equal("x y", Clean("<a>x</a> <a class=c>y</a>"));
    }

    [Fact]
    public void KeepsAnchorStateAcrossCalls()
    {
        HtmlSanitizer sanitizer = new HtmlSanitizer(new HeadingSlugs());

        Assert.Equal(string.Empty, sanitizer.Sanitize("<a href=\"javascript:x\">"));
        Assert.Equal("text", sanitizer.Sanitize("text"));
        Assert.Equal(string.Empty, sanitizer.Sanitize("</a>"));
        Assert.Equal(
            "<a href=\"https://x.y\" rel=\"nofollow\">",
            sanitizer.Sanitize("<a href=\"https://x.y\">")
        );
        Assert.Equal("</a>", sanitizer.Sanitize("</a>"));
    }

    [Fact]
    public void IgnoresClosingTagsThatWereNeverOpened()
    {
        Assert.Equal("a", Clean("a</div></a>"));
    }

    [Fact]
    public void AddsDirAutoToBlocks()
    {
        Assert.Equal("<div dir=\"auto\">a</div>", Clean("<div>a</div>"));
        Assert.Equal("<p dir=\"rtl\">a</p>", Clean("<p dir=\"rtl\">a</p>"));
        Assert.Equal("<ul dir=\"auto\"><li>a</li></ul>", Clean("<ul><li>a</li></ul>"));
        Assert.Equal("<span>a</span>", Clean("<span>a</span>"));
    }

    [Fact]
    public void WrapsOuterTablesOnly()
    {
        Assert.Equal(
            "<markdown-accessiblity-table><table><tr><td><table></table></td></tr></table></markdown-accessiblity-table>",
            Clean("<table><tr><td><table></table></td></tr></table>")
        );
    }

    [Fact]
    public void WrapsPictures()
    {
        Assert.Equal(
            "<themed-picture data-catalyst-inline=\"true\"><picture><source srcset=\"a.png\"><img src=\"b.png\"></picture></themed-picture>",
            Clean("<picture><source srcset=\"a.png\"><img src=\"b.png\"></picture>")
        );
    }

    [Fact]
    public void LinksStandaloneImages()
    {
        string html = Clean("<img src=\"https://x.y/a.png\" alt=\"a\" onerror=\"x()\">");

        Assert.StartsWith(
            "<a target=\"_blank\" rel=\"noopener noreferrer nofollow\" href=\"https://x.y/a.png\">",
            html
        );
        Assert.Contains("style=\"max-width: 100%;\"", html);
        Assert.DoesNotContain("onerror", html);
        Assert.EndsWith("</a>", html);
    }

    [Fact]
    public void SizesImagesWithWidthAndHeight()
    {
        string html = Clean("<img src=\"a.png\" width=\"100\" height=\"50\">");

        Assert.Contains("max-height: 50px;; aspect-ratio: 100 / 50;", html);
        Assert.Contains("class=\"js-gh-image-fallback\"", html);
        Assert.Contains("rel=\"noopener noreferrer\" href=\"a.png\"", html);
    }

    [Fact]
    public void DropsUnsafeImageSources()
    {
        string html = Clean("<img src=\"data:image/png;base64,AAAA\" alt=\"d\">");

        Assert.Contains("href=\"\"", html);
        Assert.DoesNotContain("data:", html);
        Assert.Contains("alt=\"d\"", html);
    }

    [Fact]
    public void DoesNotLinkImagesInsideLinksOrPictures()
    {
        Assert.Equal(
            "<a href=\"https://x.y\" rel=\"nofollow\"><img src=\"a.png\" style=\"max-width: 100%;\"></a>",
            Clean("<a href=\"https://x.y\"><img src=\"a.png\"></a>")
        );
        Assert.Contains(
            "<img src=\"a.png\">",
            Clean("<picture><img src=\"a.png\"></picture>")
        );
    }

    [Fact]
    public void WrapsRawHeadingsLikeMarkdownOnes()
    {
        string html = Clean("<h2 id=\"x\">Some <b>Title</b></h2>");

        Assert.StartsWith(
            "<div class=\"markdown-heading\" dir=\"auto\"><h2 id=\"user-content-x\" class=\"heading-element\" dir=\"auto\">Some <b>Title</b></h2>",
            html
        );
        Assert.Contains("id=\"user-content-some-title\"", html);
        Assert.Contains("aria-label=\"Permalink: Some Title\"", html);
    }

    [Fact]
    public void NumbersRepeatedRawHeadings()
    {
        HtmlSanitizer sanitizer = new HtmlSanitizer(new HeadingSlugs());
        _ = sanitizer.Sanitize("<h1>A</h1>");

        Assert.Contains("id=\"user-content-a-1\"", sanitizer.Sanitize("<h1>A</h1>"));
    }

    [Fact]
    public void KeepsHeadingsWithoutClosingTagAsPlainTags()
    {
        Assert.Equal("<h3 dir=\"auto\">open", Clean("<h3>open"));
    }

    [Theory]
    [InlineData("a < b", "a &lt; b")]
    [InlineData("<", "&lt;")]
    [InlineData("<3", "&lt;3")]
    [InlineData("<b", "&lt;b")]
    [InlineData("a <> b", "a &lt;> b")]
    public void EscapesStrayAngleBrackets(string html, string expected)
    {
        Assert.Equal(expected, Clean(html));
    }

    [Fact]
    public void SurvivesQuotesAndSlashesInsideTags()
    {
        Assert.Equal(
            "<span title=\"a&gt;b\">x</span>".Replace("&gt;", ">"),
            Clean("<span title=\"a>b\" / >x</span>")
        );
        Assert.Equal("<br>", Clean("<br/>"));
    }
}

public sealed record XssTests
{
    private static string Render(string markdown)
    {
        return new MarkdownHtml(new Pure.Primitives.String.String(markdown)).TextValue;
    }

    [Theory]
    [InlineData("[x](javascript:alert(1))")]
    [InlineData("[x](JavaScript:alert(1))")]
    [InlineData("[x](data:text/html,<script>alert(1)</script>)")]
    [InlineData("[x](vbscript:x)")]
    [InlineData("<a href=\"javascript:alert(1)\">x</a>")]
    [InlineData("[x][r]\n\n[r]: javascript:alert(1)")]
    public void NeverEmitsScriptUrls(string markdown)
    {
        string html = Render(markdown);

        Assert.DoesNotContain("javascript:", html, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("vbscript:", html, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("href=\"data:", html, StringComparison.OrdinalIgnoreCase);
        Assert.Contains("x", html);
    }

    [Theory]
    [InlineData("<script>alert(1)</script>")]
    [InlineData("text <script>alert(1)</script>")]
    [InlineData("<img src=x onerror=alert(1)>")]
    [InlineData("<div onclick=\"alert(1)\">x</div>")]
    [InlineData("<svg onload=alert(1)>")]
    [InlineData("<iframe src=\"https://evil.example\"></iframe>")]
    [InlineData("<style>@import 'https://evil.example/x.css'</style>")]
    [InlineData("<link rel=stylesheet href=https://evil.example/x.css>")]
    [InlineData("<meta http-equiv=refresh content=0;url=https://evil.example>")]
    [InlineData("<object data=https://evil.example></object>")]
    [InlineData("<embed src=https://evil.example>")]
    [InlineData("<base href=https://evil.example/>")]
    [InlineData("<form action=https://evil.example><button>x</button></form>")]
    [InlineData("<math><mi xlink:href=javascript:x>x</mi></math>")]
    public void NeverEmitsActiveContent(string markdown)
    {
        string html = Render(markdown);

        Assert.DoesNotContain("<script", html, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("<iframe", html, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("<style", html, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("<link", html, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("<meta", html, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("<object", html, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("<embed", html, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("<base", html, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("<form", html, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("<svg", html, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain("<math", html, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain(" onerror", html, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain(" onclick", html, StringComparison.OrdinalIgnoreCase);
        Assert.DoesNotContain(" onload", html, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void KeepsUnsafeImageSourcesOut()
    {
        string html = Render("![x](javascript:alert(1))");

        Assert.DoesNotContain("javascript", html);
        Assert.DoesNotContain("src=", html);
        Assert.Contains("alt=\"x\"", html);
    }

    [Fact]
    public void EscapesTextInAttributes()
    {
        string html = Render("[a\"onmouseover=\"x](https://x.y \"t\"onfocus=\"y\")");

        Assert.DoesNotContain("\"onmouseover", html);
    }
}
