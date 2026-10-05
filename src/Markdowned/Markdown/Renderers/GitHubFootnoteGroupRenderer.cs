using System.Globalization;
using Markdig.Extensions.Footnotes;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markdowned.Markdown.Renderers;

public sealed class GitHubFootnoteGroupRenderer(FootnoteReferences references)
    : HtmlObjectRenderer<FootnoteGroup>
{
    private readonly FootnoteReferences _references = references;

    protected override void Write(HtmlRenderer renderer, FootnoteGroup obj)
    {
        _ = renderer.EnsureLine();
        _ = renderer.WriteLine("<section data-footnotes=\"\" class=\"footnotes\">");
        _ = renderer.WriteLine(
            "<h2 id=\"footnote-label\" class=\"sr-only\" dir=\"auto\">Footnotes</h2>"
        );
        _ = renderer.WriteLine("<ol dir=\"auto\">");

        foreach (
            Footnote footnote in obj.OfType<Footnote>()
                .Where(item => _references.IsReferenced(LabelOf(item)))
                .OrderBy(item => _references.Number(LabelOf(item)))
        )
        {
            WriteFootnote(renderer, footnote);
        }

        _ = renderer.WriteLine("</ol>");
        _ = renderer.WriteLine("</section>");
    }

    private static string LabelOf(Footnote footnote)
    {
        return (footnote.Label ?? string.Empty).TrimStart('^');
    }

    private void WriteFootnote(HtmlRenderer renderer, Footnote footnote)
    {
        _ = renderer.Write("<li id=\"user-content-fn-").WriteEscape(LabelOf(footnote));
        _ = renderer.Write('-').Write(_references.Suffix).WriteLine("\">");
        List<Block> blocks = [.. footnote];
        bool previous = renderer.ImplicitParagraph;
        renderer.ImplicitParagraph = false;

        for (int index = 0; index < blocks.Count; index++)
        {
            if (index == blocks.Count - 1 && blocks[index] is ParagraphBlock last)
            {
                _ = renderer.Write("<p dir=\"auto\">");
                _ = renderer.WriteLeafInline(last);
                bool onlyBackLinks =
                    last.Inline?.All(child => child is FootnoteLink { IsBackLink: true })
                    ?? true;
                _ = renderer
                    .Write(onlyBackLinks ? string.Empty : " ")
                    .Write(BackReferences(footnote))
                    .WriteLine("</p>");
            }
            else
            {
                renderer.Write(blocks[index]);
            }
        }

        if (blocks.Count == 0 || blocks[^1] is not ParagraphBlock)
        {
            _ = renderer
                .Write("<p dir=\"auto\">")
                .Write(BackReferences(footnote))
                .WriteLine("</p>");
        }

        renderer.ImplicitParagraph = previous;
        _ = renderer.WriteLine("</li>");
    }

    private string BackReferences(Footnote footnote)
    {
        int count = Math.Max(1, footnote.Links.Count(link => !link.IsBackLink));
        string order = _references
            .Number(LabelOf(footnote))
            .ToString(CultureInfo.InvariantCulture);

        return string.Join(
            ' ',
            Enumerable
                .Range(1, count)
                .Select(occurrence =>
                    $"<a href=\"#{_references.ReferenceId(LabelOf(footnote), occurrence)}\" "
                    + "data-footnote-backref=\"\" aria-label=\"Back to reference "
                    + (occurrence == 1 ? order : $"{order}-{occurrence}")
                    + "\" class=\"data-footnote-backref\">↩"
                    + (occurrence == 1 ? string.Empty : $"<sup>{occurrence}</sup>")
                    + "</a>"
                )
        );
    }
}
