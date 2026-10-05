using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax.Inlines;

namespace Markdowned.Markdown.Renderers;

public sealed class GitHubLinkRenderer : HtmlObjectRenderer<LinkInline>
{
    private static bool IsExternal(string? url)
    {
        return url is not null
            && (
                url.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
                || url.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
            );
    }

    private static bool InsideLink(LinkInline link)
    {
        for (
            ContainerInline? parent = link.Parent;
            parent is not null;
            parent = parent.Parent
        )
        {
            if (parent is LinkInline { IsImage: false })
            {
                return true;
            }
        }

        return false;
    }

    protected override void Write(HtmlRenderer renderer, LinkInline obj)
    {
        if (obj.IsImage)
        {
            WriteImage(renderer, obj);

            return;
        }

        _ = renderer
            .Write("<a href=\"")
            .WriteEscapeUrl(obj.GetDynamicUrl?.Invoke() ?? obj.Url);
        _ = renderer.Write('"');

        if (!string.IsNullOrEmpty(obj.Title))
        {
            _ = renderer.Write(" title=\"").WriteEscape(obj.Title).Write('"');
        }

        if (IsExternal(obj.Url))
        {
            _ = renderer.Write(" rel=\"nofollow\"");
        }

        _ = renderer.Write('>');
        renderer.WriteChildren(obj);
        _ = renderer.Write("</a>");
    }

    private static void WriteImage(HtmlRenderer renderer, LinkInline obj)
    {
        bool wrap = IsExternal(obj.Url) && !InsideLink(obj);

        if (wrap)
        {
            _ = renderer.Write(
                "<a target=\"_blank\" rel=\"noopener noreferrer nofollow\" href=\""
            );
            _ = renderer.WriteEscapeUrl(obj.Url).Write("\">");
        }

        _ = renderer.Write("<img src=\"").WriteEscapeUrl(obj.Url).Write("\" alt=\"");
        _ = renderer.WriteEscape(new InlineText(obj).TextValue).Write('"');

        if (!string.IsNullOrEmpty(obj.Title))
        {
            _ = renderer.Write(" title=\"").WriteEscape(obj.Title).Write('"');
        }

        _ = renderer.Write(" style=\"max-width: 100%;\">");

        if (wrap)
        {
            _ = renderer.Write("</a>");
        }
    }
}
