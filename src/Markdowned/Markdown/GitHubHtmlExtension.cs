using Markdig;
using Markdig.Extensions.Alerts;
using Markdig.Extensions.Footnotes;
using Markdig.Extensions.Tables;
using Markdig.Extensions.TaskLists;
using Markdig.Extensions.Yaml;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Renderers.Html.Inlines;
using Markdowned.Markdown.Renderers;

namespace Markdowned.Markdown;

public sealed class GitHubHtmlExtension : IMarkdownExtension
{
    public void Setup(MarkdownPipelineBuilder pipeline)
    {
        if (!pipeline.InlineParsers.Contains<EmailAutolinkParser>())
        {
            pipeline.InlineParsers.Insert(0, new EmailAutolinkParser());
            pipeline.InlineParsers.Insert(0, new MathInlineParser());
        }
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
        if (renderer is not HtmlRenderer html)
        {
            return;
        }

        html.ObjectRenderers.AddIfNotAlready(new GitHubMathRenderer());
        FootnoteReferences references = new FootnoteReferences();
        HeadingSlugs slugs = new HeadingSlugs();
        HtmlSanitizer sanitizer = new HtmlSanitizer(slugs);
        _ = html.ObjectRenderers.ReplaceOrAdd<HtmlBlockRenderer>(
            new GitHubHtmlBlockRenderer(sanitizer)
        );
        _ = html.ObjectRenderers.ReplaceOrAdd<HtmlInlineRenderer>(
            new GitHubHtmlInlineRenderer(sanitizer)
        );
        _ = html.ObjectRenderers.ReplaceOrAdd<AlertBlockRenderer>(
            new GitHubAlertRenderer()
        );
        _ = html.ObjectRenderers.ReplaceOrAdd<HtmlFootnoteLinkRenderer>(
            new GitHubFootnoteLinkRenderer(references)
        );
        _ = html.ObjectRenderers.ReplaceOrAdd<HtmlFootnoteGroupRenderer>(
            new GitHubFootnoteGroupRenderer(references)
        );
        _ = html.ObjectRenderers.ReplaceOrAdd<YamlFrontMatterHtmlRenderer>(
            new GitHubFrontMatterRenderer()
        );
        _ = html.ObjectRenderers.ReplaceOrAdd<HeadingRenderer>(
            new GitHubHeadingRenderer(slugs)
        );
        _ = html.ObjectRenderers.ReplaceOrAdd<ParagraphRenderer>(
            new GitHubParagraphRenderer()
        );
        _ = html.ObjectRenderers.ReplaceOrAdd<ListRenderer>(new GitHubListRenderer());
        _ = html.ObjectRenderers.ReplaceOrAdd<CodeBlockRenderer>(
            new GitHubCodeBlockRenderer()
        );
        _ = html.ObjectRenderers.ReplaceOrAdd<LinkInlineRenderer>(
            new GitHubLinkRenderer()
        );
        _ = html.ObjectRenderers.ReplaceOrAdd<AutolinkInlineRenderer>(
            new GitHubAutolinkRenderer()
        );
        _ = html.ObjectRenderers.ReplaceOrAdd<HtmlTableRenderer>(
            new GitHubTableRenderer()
        );
        _ = html.ObjectRenderers.ReplaceOrAdd<HtmlTaskListRenderer>(
            new GitHubTaskListRenderer()
        );
    }
}
