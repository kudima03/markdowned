using Markdig.Extensions.Yaml;
using Markdig.Renderers;
using Markdig.Renderers.Html;

namespace Markdowned.Markdown.Renderers;

public sealed class GitHubFrontMatterRenderer : HtmlObjectRenderer<YamlFrontMatterBlock>
{
    protected override void Write(HtmlRenderer renderer, YamlFrontMatterBlock obj)
    {
        _ = renderer.EnsureLine();
        _ = renderer.Write(
            new FrontMatterTable(
                new Pure.Primitives.String.String(obj.Lines.ToString())
            ).TextValue
        );
        _ = renderer.WriteLine();
    }
}
