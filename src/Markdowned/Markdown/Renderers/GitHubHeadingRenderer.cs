using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markdowned.Markdown.Renderers;

public sealed class GitHubHeadingRenderer(HeadingSlugs slugs)
    : HtmlObjectRenderer<HeadingBlock>
{
    public const string LinkIcon =
        "<svg data-component=\"Octicon\" class=\"octicon octicon-link\" viewBox=\"0 0 16 16\" version=\"1.1\" width=\"16\" "
        + "height=\"16\" aria-hidden=\"true\"><path d=\"m7.775 3.275 1.25-1.25a3.5 3.5 0 1 1 "
        + "4.95 4.95l-2.5 2.5a3.5 3.5 0 0 1-4.95 0 .751.751 0 0 1 .018-1.042.751.751 0 0 1 "
        + "1.042-.018 1.998 1.998 0 0 0 2.83 0l2.5-2.5a2.002 2.002 0 0 0-2.83-2.83l-1.25 "
        + "1.25a.751.751 0 0 1-1.042-.018.751.751 0 0 1-.018-1.042Zm-4.69 9.64a1.998 1.998 0 0 0 "
        + "2.83 0l1.25-1.25a.751.751 0 0 1 1.042.018.751.751 0 0 1 .018 1.042l-1.25 1.25a3.5 "
        + "3.5 0 1 1-4.95-4.95l2.5-2.5a3.5 3.5 0 0 1 4.95 0 .751.751 0 0 1-.018 1.042.751.751 "
        + "0 0 1-1.042.018 1.998 1.998 0 0 0-2.83 0l-2.5 2.5a1.998 1.998 0 0 0 0 2.83Z\">"
        + "</path></svg>";

    private readonly HeadingSlugs _slugs = slugs;

    protected override void Write(HtmlRenderer renderer, HeadingBlock obj)
    {
        string text = new InlineText(obj.Inline).TextValue;
        string slug = _slugs.Unique(
            new HeadingSlug(new Pure.Primitives.String.String(text)).TextValue
        );

        _ = renderer.EnsureLine();
        _ = renderer.Write("<div class=\"markdown-heading\" dir=\"auto\"><h");
        _ = renderer.Write(
            obj.Level.ToString(System.Globalization.CultureInfo.InvariantCulture)
        );
        _ = renderer.Write(" class=\"heading-element\" dir=\"auto\">");
        _ = renderer.WriteLeafInline(obj);
        _ = renderer.Write("</h");
        _ = renderer.Write(
            obj.Level.ToString(System.Globalization.CultureInfo.InvariantCulture)
        );
        _ = renderer.Write("><a id=\"user-content-");
        _ = renderer.WriteEscape(slug);
        _ = renderer.Write("\" class=\"anchor\" aria-label=\"Permalink: ");
        _ = renderer.WriteEscape(text);
        _ = renderer.Write("\" href=\"#");
        _ = renderer.WriteEscape(slug);
        _ = renderer.Write("\">");
        _ = renderer.Write(LinkIcon);
        _ = renderer.WriteLine("</a></div>");
    }
}
