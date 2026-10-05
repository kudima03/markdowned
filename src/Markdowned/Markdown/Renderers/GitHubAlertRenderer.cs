using Markdig.Extensions.Alerts;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markdowned.Markdown.Renderers;

public sealed class GitHubAlertRenderer : HtmlObjectRenderer<AlertBlock>
{
    private static readonly string[] Known =
    [
        "note",
        "tip",
        "important",
        "warning",
        "caution",
    ];

    private static void WriteQuote(HtmlRenderer renderer, AlertBlock obj)
    {
        _ = renderer.EnsureLine();
        _ = renderer.WriteLine("<blockquote>");
        _ = renderer
            .Write("<p dir=\"auto\">[!")
            .WriteEscape(obj.Kind.ToString())
            .WriteLine("]");

        for (int index = 0; index < obj.Count; index++)
        {
            if (index == 0 && obj[0] is ParagraphBlock first)
            {
                _ = renderer.WriteLeafInline(first);
                _ = renderer.WriteLine("</p>");
            }
            else
            {
                renderer.Write(obj[index]);
            }
        }

        _ = renderer.WriteLine("</blockquote>");
    }

    protected override void Write(HtmlRenderer renderer, AlertBlock obj)
    {
        string kind = obj.Kind.ToString().ToLowerInvariant();

        if (!Known.Contains(kind))
        {
            WriteQuote(renderer, obj);

            return;
        }

        string title = char.ToUpperInvariant(kind[0]) + kind[1..];

        _ = renderer.EnsureLine();
        _ = renderer.Write("<div class=\"markdown-alert markdown-alert-").Write(kind);
        _ = renderer.WriteLine("\" dir=\"auto\">");
        _ = renderer.Write("<p class=\"markdown-alert-title\" dir=\"auto\">");
        _ = renderer.Write(new AlertIcon(kind).TextValue).Write(title).WriteLine("</p>");
        renderer.WriteChildren(obj);
        _ = renderer.WriteLine("</div>");
    }
}
