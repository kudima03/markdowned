using Markdowned.Markdown;
using Markdowned.Page;
using String = Pure.Primitives.String.String;

namespace Markdowned.Tests.Markdown;

public sealed record MermaidTests
{
    private const string Diagram = "```mermaid\nflowchart LR\n  A --> B\n```";

    [Fact]
    public void KeepsTheSourceInAGitHubStyleSection()
    {
        string html = new MarkdownHtml(new String(Diagram)).TextValue;

        Assert.Contains("<section class=\"js-render-needs-enrichment", html);
        Assert.Contains("data-type=\"mermaid\"", html);
        Assert.Contains(
            "<pre lang=\"mermaid\" aria-label=\"Raw mermaid code\">flowchart LR",
            html
        );
        Assert.Contains("A --&gt; B", html);
    }

    [Fact]
    public void LoadsMermaidOnlyForPagesWithDiagrams()
    {
        string with = new PageHtml(new MarkdownHtml(new String(Diagram))).TextValue;
        string without = new PageHtml(new MarkdownHtml(new String("text"))).TextValue;

        Assert.Contains("/assets/js/mermaid.js", with);
        Assert.Contains("theme: 'default'", with);
        Assert.Contains("securityLevel: 'strict'", with);
        Assert.DoesNotContain("mermaid", without, StringComparison.OrdinalIgnoreCase);
    }
}
