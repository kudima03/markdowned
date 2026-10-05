using Markdig.Renderers;
using Markdig.Renderers.Html;

namespace Markdowned.Markdown.Renderers;

/// <summary>
/// GitHub's server-side math markup. The TeX stays in the element; MathJax typesets it in the
/// page, as GitHub's client does.
/// </summary>
public sealed class GitHubMathRenderer : HtmlObjectRenderer<MathInline>
{
    public const string RunId = "86d8c3fdeeb0723f50d01513040dc130";

    public static void Write(HtmlRenderer renderer, string tex, bool display)
    {
        string delimiter = display ? "$$" : "$";

        _ = renderer.Write(
            display
                ? "<math-renderer class=\"js-display-math\" style=\"display: block\""
                : "<math-renderer class=\"js-inline-math\" style=\"display: inline-block\""
        );
        _ = renderer.Write(" data-run-id=\"").Write(RunId).Write("\">");
        _ = renderer
            .Write(delimiter)
            .WriteEscape(tex)
            .Write(delimiter)
            .Write("</math-renderer>");
    }

    protected override void Write(HtmlRenderer renderer, MathInline obj)
    {
        Write(renderer, obj.Tex, obj.Display);
    }
}
