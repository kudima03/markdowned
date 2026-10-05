using System.Net;
using System.Text;
using Pure.Primitives.Abstractions.Char;
using Pure.Primitives.Abstractions.String;
using Char = Pure.Primitives.Char.Char;

namespace Markdowned.Markdown;

/// <summary>
/// The table GitHub shows for YAML front matter. Supports the subset GitHub renders as
/// nested tables: scalars, flow lists (<c>[a, b]</c>), block lists and one level of maps.
/// </summary>
public sealed record FrontMatterTable : IString
{
    private readonly IString _yaml;

    public FrontMatterTable(IString yaml)
    {
        _yaml = yaml;
    }

    private static string Escape(string text)
    {
        return WebUtility.HtmlEncode(text);
    }

    private static string Unquote(string value)
    {
        string trimmed = value.Trim();

        return
            trimmed.Length >= 2
            && (
                (trimmed[0] == '"' && trimmed[^1] == '"')
                || (trimmed[0] == '\'' && trimmed[^1] == '\'')
            )
            ? trimmed[1..^1]
            : trimmed;
    }

    private static string Cell(string value)
    {
        return $"<td><div dir=\"auto\">{Escape(Unquote(value))}</div></td>";
    }

    private static string ListTable(IEnumerable<string> items)
    {
        return "<table>\n  <tbody>\n  <tr>\n  "
            + string.Concat(items.Select(Cell))
            + "\n  </tr>\n  </tbody>\n</table>\n";
    }

    private static string MapTable(IEnumerable<(string Key, string Value)> entries)
    {
        List<(string Key, string Value)> list = [.. entries];

        return "<table>\n  <thead>\n  <tr>\n  "
            + string.Concat(list.Select(entry => $"<th>{Escape(entry.Key)}</th>"))
            + "\n  </tr>\n  </thead>\n  <tbody>\n  <tr>\n  "
            + string.Concat(list.Select(entry => Cell(entry.Value)))
            + "\n  </tr>\n  </tbody>\n</table>\n";
    }

    private static IEnumerable<string> FlowList(string value)
    {
        return value
            .Trim()[1..^1]
            .Split(',')
            .Select(item => item.Trim())
            .Where(item => item.Length > 0);
    }

    private static (string Key, string Value)? Split(string line)
    {
        int colon = line.IndexOf(':', StringComparison.Ordinal);

        return colon <= 0 || (colon + 1 < line.Length && line[colon + 1] != ' ')
            ? null
            : (line[..colon].Trim(), line[(colon + 1)..].Trim());
    }

    public string TextValue
    {
        get
        {
            string[] lines =
            [
                .. _yaml
                    .TextValue.Replace("\r", string.Empty, StringComparison.Ordinal)
                    .Split('\n')
                    .Where(line =>
                        line.Trim().Length > 0 && !line.TrimStart().StartsWith('#')
                    ),
            ];
            StringBuilder rows = new StringBuilder();

            for (int index = 0; index < lines.Length; index++)
            {
                if (
                    Split(lines[index]) is not ({ } key, { } value)
                    || lines[index][0] == ' '
                )
                {
                    continue;
                }

                List<string> children =
                [
                    .. lines
                        .Skip(index + 1)
                        .TakeWhile(line => line[0] == ' ' || line.StartsWith('-'))
                        .Select(line => line.Trim()),
                ];

                string cell =
                    value.StartsWith('[') && value.EndsWith(']')
                        ? ListTable(FlowList(value))
                    : value.Length > 0 ? Escape(Unquote(value))
                    : children.Count > 0
                    && children.All(child =>
                        child.StartsWith("- ", StringComparison.Ordinal)
                    )
                        ? ListTable(children.Select(child => child[2..]))
                    : children.Count > 0
                        ? MapTable(
                            children
                                .Select(Split)
                                .Where(entry => entry is not null)
                                .Select(entry => entry!.Value)
                        )
                    : string.Empty;

                _ = rows.Append("  <tr>\n    <th>")
                    .Append(Escape(key))
                    .Append("</th>\n    <td>")
                    .Append(cell)
                    .Append("</td>\n  </tr>\n");
            }

            return "<markdown-accessiblity-table><table>\n  <tbody>\n"
                + rows
                + "  </tbody>\n</table></markdown-accessiblity-table>\n";
        }
    }

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
