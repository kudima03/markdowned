using Markdig.Extensions.Tables;
using Markdig.Renderers;
using Markdig.Renderers.Html;

namespace Markdowned.Markdown.Renderers;

public sealed class GitHubTableRenderer : HtmlObjectRenderer<Table>
{
    protected override void Write(HtmlRenderer renderer, Table obj)
    {
        _ = renderer.EnsureLine();
        _ = renderer.WriteLine("<markdown-accessiblity-table><table>");
        List<TableRow> rows = [.. obj.OfType<TableRow>()];
        List<TableRow> header = [.. rows.TakeWhile(row => row.IsHeader)];
        List<TableRow> body = [.. rows.Skip(header.Count)];

        if (header.Count > 0)
        {
            _ = renderer.WriteLine("<thead>");
            WriteRows(renderer, obj, header, "th");
            _ = renderer.WriteLine("</thead>");
        }

        if (body.Count > 0)
        {
            _ = renderer.WriteLine("<tbody>");
            WriteRows(renderer, obj, body, "td");
            _ = renderer.WriteLine("</tbody>");
        }

        _ = renderer.WriteLine("</table></markdown-accessiblity-table>");
    }

    private static void WriteRows(
        HtmlRenderer renderer,
        Table table,
        List<TableRow> rows,
        string cellTag
    )
    {
        foreach (TableRow row in rows)
        {
            _ = renderer.WriteLine("<tr>");
            int column = 0;

            foreach (TableCell cell in row.OfType<TableCell>())
            {
                int index = cell.ColumnIndex >= 0 ? cell.ColumnIndex : column;
                TableColumnAlign? align =
                    index < table.ColumnDefinitions.Count
                        ? table.ColumnDefinitions[index].Alignment
                        : null;
                _ = renderer.Write('<').Write(cellTag);

                _ = align switch
                {
                    TableColumnAlign.Left => renderer.Write(" align=\"left\""),
                    TableColumnAlign.Center => renderer.Write(" align=\"center\""),
                    TableColumnAlign.Right => renderer.Write(" align=\"right\""),
                    _ => renderer,
                };

                if (cell.ColumnSpan > 1)
                {
                    _ = renderer
                        .Write(" colspan=\"")
                        .Write(cell.ColumnSpan.ToString())
                        .Write('"');
                }

                _ = renderer.Write('>');
                bool previous = renderer.ImplicitParagraph;
                renderer.ImplicitParagraph = true;
                renderer.WriteChildren(cell);
                renderer.ImplicitParagraph = previous;
                _ = renderer.Write("</").Write(cellTag).WriteLine(">");
                column += Math.Max(1, cell.ColumnSpan);
            }

            _ = renderer.WriteLine("</tr>");
        }
    }
}
