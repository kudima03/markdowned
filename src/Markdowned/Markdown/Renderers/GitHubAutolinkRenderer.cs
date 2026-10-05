using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax.Inlines;

namespace Markdowned.Markdown.Renderers;

public sealed class GitHubAutolinkRenderer : HtmlObjectRenderer<AutolinkInline>
{
    protected override void Write(HtmlRenderer renderer, AutolinkInline obj)
    {
        string href = obj.IsEmail ? $"mailto:{obj.Url}" : obj.Url;

        _ = renderer.Write("<a href=\"").WriteEscapeUrl(href).Write('"');

        if (!obj.IsEmail)
        {
            _ = renderer.Write(" rel=\"nofollow\"");
        }

        _ = renderer.Write('>').WriteEscape(obj.Url).Write("</a>");
    }
}
