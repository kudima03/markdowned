using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markdowned.Markdown.Renderers;

public sealed class GitHubHtmlBlockRenderer : HtmlObjectRenderer<HtmlBlock>
{
    private readonly HtmlSanitizer _sanitizer;

    public GitHubHtmlBlockRenderer(HtmlSanitizer sanitizer)
    {
        _sanitizer = sanitizer;
    }

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

        renderer.EnsureLine();
        _ = renderer.WriteLine(
            image ? $"<p dir=\"auto\">{html.Trim()}</p>" : html.Trim('\n')
        );
    }
}
