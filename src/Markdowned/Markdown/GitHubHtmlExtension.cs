using Markdig;
using Markdig.Extensions.Tables;
using Markdig.Extensions.TaskLists;
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
        }
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
        if (renderer is not HtmlRenderer html)
        {
            return;
        }

        _ = html.ObjectRenderers.ReplaceOrAdd<HeadingRenderer>(
            new GitHubHeadingRenderer()
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
