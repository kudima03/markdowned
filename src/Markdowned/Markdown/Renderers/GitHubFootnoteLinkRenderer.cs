using Markdig.Extensions.Footnotes;
using Markdig.Renderers;
using Markdig.Renderers.Html;

namespace Markdowned.Markdown.Renderers;

public sealed class GitHubFootnoteLinkRenderer(FootnoteReferences references)
    : HtmlObjectRenderer<FootnoteLink>
{
    private readonly FootnoteReferences _references = references;

    protected override void Write(HtmlRenderer renderer, FootnoteLink obj)
    {
        if (obj.IsBackLink)
        {
            return;
        }

        string label = (obj.Footnote.Label ?? string.Empty).TrimStart('^');
        int occurrence = _references.Next(label);

        _ = renderer.Write("<sup><a href=\"#user-content-fn-").WriteEscape(label);
        _ = renderer.Write('-').Write(_references.Suffix).Write("\" id=\"");
        _ = renderer.WriteEscape(_references.ReferenceId(label, occurrence));
        _ = renderer.Write(
            "\" data-footnote-ref=\"\" aria-describedby=\"footnote-label\">"
        );
        _ = renderer.Write(
            _references
                .Number(label)
                .ToString(System.Globalization.CultureInfo.InvariantCulture)
        );
        _ = renderer.Write("</a></sup>");
    }
}
