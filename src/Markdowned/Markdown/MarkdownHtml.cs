using Markdig;
using Markdig.Extensions.Emoji;
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

    /// <summary>GitHub leaves shortcodes that do not start with a letter or digit untouched.</summary>
    private static EmojiMapping Emoji =>
        new EmojiMapping(
            EmojiMapping
                .GetDefaultEmojiShortcodeToUnicode()
                .Where(entry =>
                    (entry.Key.Length > 1 && char.IsAsciiLetterOrDigit(entry.Key[1]))
                    || entry.Key.StartsWith(":+", StringComparison.Ordinal)
                )
                .ToDictionary(entry => entry.Key, entry => entry.Value),
            new Dictionary<string, string>()
        );

    private static MarkdownPipeline Pipeline =>
        new MarkdownPipelineBuilder()
            .UsePipeTables()
            .UseTaskLists()
            .UseEmphasisExtras(EmphasisExtraOptions.Strikethrough)
            .UseAutoLinks()
            .UseAlertBlocks()
            .UseFootnotes()
            .UseYamlFrontMatter()
            .UseEmojiAndSmiley(Emoji)
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
