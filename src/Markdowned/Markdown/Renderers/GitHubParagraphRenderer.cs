using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markdowned.Markdown.Renderers;

public sealed class GitHubParagraphRenderer : HtmlObjectRenderer<ParagraphBlock>
{
    protected override void Write(HtmlRenderer renderer, ParagraphBlock obj)
    {
        if (renderer.ImplicitParagraph)
        {
            _ = renderer.WriteLeafInline(obj);

            return;
        }

        _ = renderer.EnsureLine();
        _ = renderer.Write("<p dir=\"auto\">");
        _ = renderer.WriteLeafInline(obj);
        _ = renderer.WriteLine("</p>");
    }
}
