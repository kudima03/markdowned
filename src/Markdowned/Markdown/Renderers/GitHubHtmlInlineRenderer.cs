using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax.Inlines;

namespace Markdowned.Markdown.Renderers;

public sealed class GitHubHtmlInlineRenderer(HtmlSanitizer sanitizer)
    : HtmlObjectRenderer<HtmlInline>
{
    private readonly HtmlSanitizer _sanitizer = sanitizer;

    protected override void Write(HtmlRenderer renderer, HtmlInline obj)
    {
        _ = renderer.Write(_sanitizer.Sanitize(obj.Tag));
    }
}
