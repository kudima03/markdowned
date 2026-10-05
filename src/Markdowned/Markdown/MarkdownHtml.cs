using Markdig;
using Markdig.Extensions.EmphasisExtras;
using Markdowned.Abstractions.Markdown;
using Pure.Primitives.Abstractions.Char;
using Pure.Primitives.Abstractions.String;
using Char = Pure.Primitives.Char.Char;

namespace Markdowned.Markdown;

public sealed record MarkdownHtml : IHtml
{
    private readonly IString _markdown;

    public MarkdownHtml(IString markdown)
    {
        _markdown = markdown;
    }

    private static MarkdownPipeline Pipeline =>
        new MarkdownPipelineBuilder()
            .UsePipeTables()
            .UseTaskLists()
            .UseEmphasisExtras(EmphasisExtraOptions.Strikethrough)
            .UseAutoLinks()
            .Use<GitHubHtmlExtension>()
            .Build();

    public string TextValue => Markdig.Markdown.ToHtml(_markdown.TextValue, Pipeline);

    public IEnumerator<IChar> GetEnumerator()
    {
        return TextValue.Select(symbol => new Char(symbol)).Cast<IChar>().GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
