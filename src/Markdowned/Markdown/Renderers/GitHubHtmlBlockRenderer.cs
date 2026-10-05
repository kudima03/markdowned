using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markdowned.Markdown.Renderers;

public sealed class GitHubHtmlBlockRenderer(HtmlSanitizer sanitizer)
    : HtmlObjectRenderer<HtmlBlock>
{
    private readonly HtmlSanitizer _sanitizer = sanitizer;

    protected override void Write(HtmlRenderer renderer, HtmlBlock obj)
    {
        string raw = obj.Lines.ToString();
        string html = _sanitizer.Sanitize(raw);
        string start = raw.TrimStart();
        bool image =
            start.StartsWith("<img", StringComparison.OrdinalIgnoreCase)
            || (
                start.StartsWith("<a ", StringComparison.OrdinalIgnoreCase)
                && raw.Contains("<img", StringComparison.OrdinalIgnoreCase)
            );

        _ = renderer.EnsureLine();
        _ = renderer.WriteLine(
            image ? $"<p dir=\"auto\">{html.Trim()}</p>" : html.Trim('\n')
        );
    }
}
