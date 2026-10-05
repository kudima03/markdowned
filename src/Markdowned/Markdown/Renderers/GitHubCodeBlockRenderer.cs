using System.Text;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markdowned.Markdown.Renderers;

public sealed class GitHubCodeBlockRenderer : HtmlObjectRenderer<CodeBlock>
{
    protected override void Write(HtmlRenderer renderer, CodeBlock obj)
    {
        StringBuilder code = new StringBuilder();

        for (int index = 0; index < obj.Lines.Count; index++)
        {
            _ = code.Append(obj.Lines.Lines[index].Slice.ToString()).Append('\n');
        }

        _ = renderer.EnsureLine();
        _ = renderer.Write(
            "<div class=\"snippet-clipboard-content notranslate position-relative overflow-auto\" "
                + "data-snippet-clipboard-copy-content=\""
        );
        _ = renderer.WriteEscape(code.ToString().TrimEnd('\n'));
        _ = renderer.Write("\"><pre class=\"notranslate\"><code>");
        _ = renderer.WriteEscape(code.ToString());
        _ = renderer.WriteLine("</code></pre></div>");
    }
}
