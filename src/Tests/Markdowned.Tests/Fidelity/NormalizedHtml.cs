using System.Text;
using System.Text.RegularExpressions;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Pure.Primitives.Abstractions.Char;
using Pure.Primitives.Abstractions.String;
using Char = Pure.Primitives.Char.Char;

namespace Markdowned.Tests.Fidelity;

/// <summary>
/// Canonical form of rendered Markdown HTML, so that two renderings can be compared as text.
/// Attribute order and insignificant whitespace are ignored, the page wrapper of GitHub's
/// contents endpoint is removed, syntax-highlight spans (added by GitHub on the server, by
/// starry-night in our page) are unwrapped and random suffixes are dropped.
/// </summary>
public sealed partial record NormalizedHtml : IString
{
    private static readonly HashSet<string> Blocks =
    [
        "address",
        "article",
        "blockquote",
        "body",
        "dd",
        "details",
        "div",
        "dl",
        "dt",
        "figure",
        "h1",
        "h2",
        "h3",
        "h4",
        "h5",
        "h6",
        "hr",
        "li",
        "ol",
        "p",
        "pre",
        "section",
        "summary",
        "table",
        "tbody",
        "td",
        "tfoot",
        "th",
        "thead",
        "tr",
        "ul",
    ];

    private readonly IString _html;

    public NormalizedHtml(IString html)
    {
        _html = html;
    }

    public string TextValue
    {
        get
        {
            IElement body = new HtmlParser().ParseDocument(_html.TextValue).Body!;
            IElement root = body.QuerySelector("article.markdown-body") ?? body;
            StringBuilder text = new StringBuilder();
            Node tree = new Node(
                "#root",
                string.Empty,
                Convert(root, false),
                string.Empty
            );
            Render(text, tree.Children, true, 0, false);

            return text.ToString();
        }
    }

    [GeneratedRegex("-[0-9a-f]{32}\\b")]
    private static partial Regex Hash();

    [GeneratedRegex("^https://camo\\.githubusercontent\\.com/[0-9a-f]{64}/([0-9a-f]+)$")]
    private static partial Regex Camo();

    [GeneratedRegex("\\s+")]
    private static partial Regex Whitespace();

    private sealed record Node(
        string Name,
        string Attributes,
        List<Node> Children,
        string Text
    )
    {
        public bool IsText => Name == "#text";

        public bool IsBlock => Blocks.Contains(Name);
    }

    private static bool IsHighlight(INode node)
    {
        return node is IHtmlSpanElement span
            && span.ClassList.Any(name =>
                name.StartsWith("pl-", StringComparison.Ordinal)
            );
    }

    private static List<Node> Convert(INode parent, bool preformatted)
    {
        List<Node> nodes = [];

        foreach (INode child in parent.ChildNodes)
        {
            if (IsHighlight(child))
            {
                nodes.AddRange(Convert(child, preformatted));
            }
            else if (child is IText text)
            {
                nodes.Add(new Node("#text", string.Empty, [], text.Data));
            }
            else if (child is IElement element)
            {
                bool inPre = preformatted || element.LocalName == "pre";
                nodes.Add(
                    new Node(
                        element.LocalName,
                        Attributes(element),
                        Convert(element, inPre),
                        string.Empty
                    )
                );
            }
        }

        List<Node> merged = [];

        foreach (Node node in nodes)
        {
            if (node.IsText && merged.Count > 0 && merged[^1].IsText)
            {
                merged[^1] = merged[^1] with { Text = merged[^1].Text + node.Text };
            }
            else
            {
                merged.Add(node);
            }
        }

        return merged;
    }

    private static string Dehash(string value)
    {
        return Hash().Replace(value, string.Empty);
    }

    private static string Decamo(string value)
    {
        Match camo = Camo().Match(value);

        return camo.Success
            ? Encoding.UTF8.GetString(System.Convert.FromHexString(camo.Groups[1].Value))
            : value;
    }

    private static string Attributes(IElement element)
    {
        return string.Concat(
            element
                .Attributes.Where(attribute =>
                    attribute.Name != "data-canonical-src"
                    && attribute.Name != "data-run-id"
                    && !attribute.Name.StartsWith(
                        "data-markdowned-",
                        StringComparison.Ordinal
                    )
                )
                .Select(attribute =>
                    (attribute.Name, Value: Dehash(Decamo(attribute.Value)))
                )
                .OrderBy(attribute => attribute.Name, StringComparer.Ordinal)
                .Select(attribute => $" {attribute.Name}=\"{attribute.Value}\"")
        );
    }

    private static void Render(
        StringBuilder output,
        List<Node> siblings,
        bool parentIsBlock,
        int depth,
        bool preformatted
    )
    {
        string indent = new string(' ', depth * 2);

        for (int index = 0; index < siblings.Count; index++)
        {
            Node node = siblings[index];

            if (node.IsText)
            {
                string text = node.Text;

                if (!preformatted)
                {
                    text = Whitespace().Replace(text, " ");
                    Node? before = index > 0 ? siblings[index - 1] : null;
                    Node? after = index < siblings.Count - 1 ? siblings[index + 1] : null;

                    if (
                        before is null
                            ? parentIsBlock
                            : before.IsBlock || before.Name == "br"
                    )
                    {
                        text = text.TrimStart();
                    }

                    if (
                        after is null
                            ? parentIsBlock
                            : after.IsBlock || after.Name == "br"
                    )
                    {
                        text = text.TrimEnd();
                    }
                }

                if (text.Length > 0)
                {
                    _ = output
                        .Append(indent)
                        .Append('"')
                        .Append(
                            Dehash(text).Replace("\n", "\\n", StringComparison.Ordinal)
                        )
                        .Append("\"\n");
                }
            }
            else
            {
                _ = output
                    .Append(indent)
                    .Append('<')
                    .Append(node.Name)
                    .Append(node.Attributes);
                _ = output.Append(">\n");
                Render(
                    output,
                    node.Children,
                    node.IsBlock,
                    depth + 1,
                    preformatted || node.Name == "pre"
                );
                _ = output.Append(indent).Append("</").Append(node.Name).Append(">\n");
            }
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
