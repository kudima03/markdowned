using System.Text;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markdowned.Markdown.Renderers;

public sealed class GitHubCodeBlockRenderer : HtmlObjectRenderer<CodeBlock>
{
    private const string Classes = "notranslate position-relative overflow-auto";

    protected override void Write(HtmlRenderer renderer, CodeBlock obj)
    {
        StringBuilder code = new StringBuilder();

        for (int index = 0; index < obj.Lines.Count; index++)
        {
            _ = code.Append(obj.Lines.Lines[index].Slice.ToString()).Append('\n');
        }

        string info = (obj as FencedCodeBlock)?.Info ?? string.Empty;
        string scope = new CodeLanguage(info).TextValue;
        string trimmed = code.ToString().TrimEnd('\n');

        _ = renderer.EnsureLine();

        if (
            info.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()
            == "mermaid"
        )
        {
            // GitHub renders diagrams in an iframe; the page renders them in place from this source.
            _ = renderer.Write(
                "<section class=\"js-render-needs-enrichment render-needs-enrichment position-relative\" "
                    + "data-type=\"mermaid\" aria-label=\"mermaid rendered output container\">"
                    + "<div class=\"js-render-enrichment-target\" dir=\"auto\">"
                    + "<div class=\"render-plaintext-hidden\" dir=\"auto\">"
                    + "<pre lang=\"mermaid\" aria-label=\"Raw mermaid code\">"
            );
            _ = renderer.WriteEscape(code.ToString());
            _ = renderer.WriteLine("</pre></div></div></section>");

            return;
        }

        if (
            info.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()
            == "math"
        )
        {
            GitHubMathRenderer.Write(renderer, trimmed, true);
            _ = renderer.WriteLine();

            return;
        }

        if (scope.Length > 0)
        {
            // starry-night adds the highlighting spans in the page, from the scope.
            _ = renderer.Write("<div class=\"highlight highlight-");
            _ = renderer.Write(scope.Replace('.', '-')).Write(' ').Write(Classes);
            _ = renderer
                .Write("\" dir=\"auto\" data-markdowned-scope=\"")
                .WriteEscape(scope);
            _ = renderer
                .Write("\" data-snippet-clipboard-copy-content=\"")
                .WriteEscape(trimmed);
            _ = renderer.Write("\"><pre>").WriteEscape(trimmed).WriteLine("</pre></div>");

            return;
        }

        _ = renderer.Write("<div class=\"snippet-clipboard-content ").Write(Classes);
        _ = renderer
            .Write("\" data-snippet-clipboard-copy-content=\"")
            .WriteEscape(trimmed);
        _ = renderer.Write("\"><pre");

        string language =
            info.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault()
            ?? string.Empty;

        if (language.Length > 0)
        {
            _ = renderer.Write(" lang=\"").WriteEscape(language).Write('"');
        }

        _ = renderer.Write(" class=\"notranslate\"><code>").WriteEscape(code.ToString());
        _ = renderer.WriteLine("</code></pre></div>");
    }
}
