using System.Net;
using System.Text;
using Markdowned.Markdown.Renderers;

namespace Markdowned.Markdown;

/// <summary>
/// Sanitises the raw HTML of a Markdown document the way GitHub does: the GFM tag filter, an
/// allow-list of elements and attributes, safe URL protocols, <c>user-content-</c> ids, and
/// the wrappers GitHub adds (<c>dir="auto"</c>, image links, pictures, tables, headings).
/// It keeps the stack of open elements between calls, because the parser hands over inline
/// tags one at a time.
/// </summary>
public sealed class HtmlSanitizer
{
    private static readonly HashSet<string> TagFilter =
    [
        "title",
        "textarea",
        "style",
        "xmp",
        "iframe",
        "noembed",
        "noframes",
        "script",
        "plaintext",
    ];

    private static readonly HashSet<string> Elements =
    [
        "a",
        "b",
        "blockquote",
        "br",
        "caption",
        "code",
        "dd",
        "del",
        "details",
        "div",
        "dl",
        "dt",
        "em",
        "h1",
        "h2",
        "h3",
        "h4",
        "h5",
        "h6",
        "hr",
        "i",
        "img",
        "ins",
        "kbd",
        "li",
        "mark",
        "ol",
        "p",
        "picture",
        "pre",
        "q",
        "rp",
        "rt",
        "ruby",
        "s",
        "samp",
        "section",
        "source",
        "span",
        "strike",
        "strong",
        "sub",
        "summary",
        "sup",
        "table",
        "tbody",
        "td",
        "tfoot",
        "th",
        "thead",
        "tr",
        "tt",
        "ul",
        "var",
    ];

    private static readonly HashSet<string> Voids = ["br", "hr", "img", "source"];

    private static readonly HashSet<string> DirAuto =
    [
        "div",
        "h1",
        "h2",
        "h3",
        "h4",
        "h5",
        "h6",
        "ol",
        "p",
        "ul",
    ];

    private static readonly HashSet<string> Attributes =
    [
        "abbr",
        "accept",
        "accept-charset",
        "accesskey",
        "action",
        "align",
        "alt",
        "axis",
        "cellpadding",
        "cellspacing",
        "char",
        "charoff",
        "charset",
        "checked",
        "clear",
        "color",
        "cols",
        "colspan",
        "compact",
        "coords",
        "datetime",
        "dir",
        "disabled",
        "enctype",
        "for",
        "frame",
        "headers",
        "height",
        "hreflang",
        "hspace",
        "ismap",
        "itemprop",
        "itemscope",
        "itemtype",
        "label",
        "lang",
        "maxlength",
        "media",
        "method",
        "multiple",
        "nohref",
        "noshade",
        "nowrap",
        "open",
        "prompt",
        "readonly",
        "rev",
        "rows",
        "rowspan",
        "rules",
        "scope",
        "selected",
        "shape",
        "size",
        "span",
        "start",
        "summary",
        "tabindex",
        "title",
        "type",
        "usemap",
        "valign",
        "value",
        "width",
    ];

    private readonly HeadingSlugs _slugs;

    private readonly List<string> _open = [];

    private readonly List<int> _droppedAnchors = [];

    public HtmlSanitizer(HeadingSlugs slugs)
    {
        _slugs = slugs;
    }

    private bool Inside(string name) => _open.Contains(name);

    private static bool IsSafe(string url, params string[] schemes)
    {
        string cleaned = new string(
            WebUtility
                .HtmlDecode(url)
                .Where(symbol => !char.IsControl(symbol) && symbol != ' ')
                .ToArray()
        );
        int colon = cleaned.IndexOf(':', StringComparison.Ordinal);
        int slash = cleaned.IndexOfAny(['/', '?', '#']);

        return colon < 0
            || (slash >= 0 && slash < colon)
            || schemes.Contains(cleaned[..colon], StringComparer.OrdinalIgnoreCase);
    }

    private static bool IsAbsolute(string url) =>
        url.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
        || url.StartsWith("https://", StringComparison.OrdinalIgnoreCase);

    private static string Quote(string value) =>
        value.Replace("\"", "&quot;", StringComparison.Ordinal);

    private static List<(string Name, string? Value)> ParseAttributes(string text)
    {
        List<(string Name, string? Value)> attributes = [];
        int index = 0;

        while (index < text.Length)
        {
            while (
                index < text.Length
                && (char.IsWhiteSpace(text[index]) || text[index] == '/')
            )
            {
                index++;
            }

            int start = index;

            while (
                index < text.Length
                && !char.IsWhiteSpace(text[index])
                && text[index] != '='
                && text[index] != '/'
            )
            {
                index++;
            }

            if (index == start)
            {
                break;
            }

            string name = text[start..index].ToLowerInvariant();
            string? value = null;

            while (index < text.Length && char.IsWhiteSpace(text[index]))
            {
                index++;
            }

            if (index < text.Length && text[index] == '=')
            {
                index++;

                while (index < text.Length && char.IsWhiteSpace(text[index]))
                {
                    index++;
                }

                if (index < text.Length && text[index] is '"' or '\'')
                {
                    char quote = text[index++];
                    int end = text.IndexOf(quote, index);
                    end = end < 0 ? text.Length : end;
                    value = text[index..end];
                    index = Math.Min(end + 1, text.Length);
                }
                else
                {
                    int begin = index;

                    while (index < text.Length && !char.IsWhiteSpace(text[index]))
                    {
                        index++;
                    }

                    value = text[begin..index];
                }
            }

            attributes.Add((name, value));
        }

        return attributes;
    }

    private static int TagEnd(string html, int start)
    {
        char quote = '\0';

        for (int index = start; index < html.Length; index++)
        {
            char symbol = html[index];

            if (quote != '\0')
            {
                quote = symbol == quote ? '\0' : quote;
            }
            else if (symbol is '"' or '\'')
            {
                quote = symbol;
            }
            else if (symbol == '>')
            {
                return index;
            }
        }

        return -1;
    }

    public string Sanitize(string html)
    {
        StringBuilder output = new StringBuilder();
        int index = 0;

        while (index < html.Length)
        {
            if (html[index] != '<')
            {
                int next = html.IndexOf('<', index);
                next = next < 0 ? html.Length : next;
                _ = output.Append(html, index, next - index);
                index = next;

                continue;
            }

            if (string.CompareOrdinal(html, index, "<!--", 0, 4) == 0)
            {
                int end = html.IndexOf("-->", index + 4, StringComparison.Ordinal);
                index = end < 0 ? html.Length : end + 3;

                continue;
            }

            bool closing = index + 1 < html.Length && html[index + 1] == '/';
            int nameStart = index + (closing ? 2 : 1);
            int nameEnd = nameStart;

            while (
                nameEnd < html.Length
                && (char.IsAsciiLetterOrDigit(html[nameEnd]) || html[nameEnd] == '-')
            )
            {
                nameEnd++;
            }

            int tagEnd = TagEnd(html, nameEnd);

            if (
                nameEnd == nameStart
                || !char.IsAsciiLetter(html[nameStart])
                || tagEnd < 0
            )
            {
                _ = output.Append(
                    index + 1 < html.Length && html[index + 1] is '!' or '?'
                        ? string.Empty
                        : "&lt;"
                );
                index++;

                continue;
            }

            string name = html[nameStart..nameEnd].ToLowerInvariant();
            string attributes = html[nameEnd..tagEnd];

            if (TagFilter.Contains(name))
            {
                _ = output.Append("&lt;").Append(html, index + 1, tagEnd - index);
            }
            else if (Elements.Contains(name))
            {
                index = closing
                    ? Close(output, name, tagEnd)
                    : Open(output, html, name, attributes, tagEnd);

                continue;
            }

            index = tagEnd + 1;
        }

        return output.ToString();
    }

    private int Close(StringBuilder output, string name, int tagEnd)
    {
        int at = _open.LastIndexOf(name);

        if (at < 0)
        {
            return tagEnd + 1;
        }

        bool dropped = name == "a" && _open[at] == "a" && _droppedAnchors.Contains(at);
        _open.RemoveRange(at, _open.Count - at);
        _ = _droppedAnchors.RemoveAll(position => position >= at);

        if (!dropped)
        {
            _ = output.Append("</").Append(name).Append('>');

            if (name == "table" && !Inside("table"))
            {
                _ = output.Append("</markdown-accessiblity-table>");
            }
            else if (name == "picture")
            {
                _ = output.Append("</themed-picture>");
            }
        }

        return tagEnd + 1;
    }

    private int Open(
        StringBuilder output,
        string html,
        string name,
        string attributes,
        int tagEnd
    )
    {
        List<(string Name, string? Value)> kept = Filter(
            name,
            ParseAttributes(attributes)
        );

        if (name == "a")
        {
            bool linked = kept.Any(attribute =>
                attribute.Name is "href" or "name" or "id"
            );

            if (!linked)
            {
                _droppedAnchors.Add(_open.Count);
            }

            _open.Add("a");

            if (linked)
            {
                _ = output.Append(Tag("a", kept));
            }

            return tagEnd + 1;
        }

        if (name == "img")
        {
            WriteImage(output, kept);

            return tagEnd + 1;
        }

        if (
            name.Length == 2
            && name[0] == 'h'
            && char.IsAsciiDigit(name[1])
            && _open.Count == 0
        )
        {
            int close = html.IndexOf(
                $"</{name}",
                tagEnd,
                StringComparison.OrdinalIgnoreCase
            );
            int closeEnd = close < 0 ? -1 : html.IndexOf('>', close);

            if (closeEnd >= 0)
            {
                WriteHeading(output, name, kept, html[(tagEnd + 1)..close]);

                return closeEnd + 1;
            }
        }

        if (name == "table" && !Inside("table"))
        {
            _ = output.Append("<markdown-accessiblity-table>");
        }
        else if (name == "picture")
        {
            _ = output.Append("<themed-picture data-catalyst-inline=\"true\">");
        }

        if (DirAuto.Contains(name) && kept.All(attribute => attribute.Name != "dir"))
        {
            kept.Add(("dir", "auto"));
        }

        _ = output.Append(Tag(name, kept));

        if (!Voids.Contains(name))
        {
            _open.Add(name);
        }

        return tagEnd + 1;
    }

    private static string Tag(
        string name,
        List<(string Name, string? Value)> attributes
    ) =>
        $"<{name}"
        + string.Concat(
            attributes.Select(attribute =>
                attribute.Value is null
                    ? $" {attribute.Name}=\"\""
                    : $" {attribute.Name}=\"{Quote(attribute.Value)}\""
            )
        )
        + ">";

    private static List<(string Name, string? Value)> Filter(
        string element,
        List<(string Name, string? Value)> attributes
    )
    {
        List<(string Name, string? Value)> kept = [];

        foreach ((string name, string? value) in attributes)
        {
            string text = value ?? string.Empty;

            if (name is "id" or "name")
            {
                kept.Add(
                    (
                        name,
                        text.StartsWith("user-content-", StringComparison.Ordinal)
                            ? text
                            : $"user-content-{text}"
                    )
                );
            }
            else if (
                name == "href"
                && element == "a"
                && IsSafe(text, "http", "https", "mailto")
            )
            {
                kept.Add((name, text));
            }
            else if (name == "src" && element == "img" && IsSafe(text, "http", "https"))
            {
                kept.Add((name, text));
            }
            else if (name == "srcset" && element is "source" or "img")
            {
                kept.Add((name, text));
            }
            else if (Attributes.Contains(name))
            {
                kept.Add((name, value));
            }
        }

        if (element == "a" && kept.Any(attribute => attribute.Name == "href"))
        {
            string href =
                kept.First(attribute => attribute.Name == "href").Value ?? string.Empty;

            if (IsAbsolute(href.Trim()))
            {
                kept.Add(("rel", "nofollow"));
            }
        }

        return kept;
    }

    private void WriteImage(StringBuilder output, List<(string Name, string? Value)> kept)
    {
        string? src = kept.FirstOrDefault(attribute => attribute.Name == "src").Value;

        if (Inside("picture"))
        {
            _ = output.Append(Tag("img", kept));

            return;
        }

        string? width = kept.FirstOrDefault(attribute => attribute.Name == "width").Value;
        string? height = kept.FirstOrDefault(attribute =>
            attribute.Name == "height"
        ).Value;
        bool sized = width is not null && height is not null;
        List<(string Name, string? Value)> image = [.. kept];
        image.Add(
            (
                "style",
                sized
                    ? $"max-width: 100%; height: auto; max-height: {height}px;; aspect-ratio: {width} / {height}; "
                        + "background-color: var(--bgColor-muted); border-radius: 6px"
                    : "max-width: 100%;"
            )
        );

        if (sized)
        {
            image.Add(("class", "js-gh-image-fallback"));
        }

        bool wrap = !Inside("a");

        if (wrap)
        {
            _ = output
                .Append("<a target=\"_blank\" rel=\"")
                .Append(
                    src is not null && IsAbsolute(src)
                        ? "noopener noreferrer nofollow"
                        : "noopener noreferrer"
                )
                .Append("\" href=\"")
                .Append(Quote(src ?? string.Empty))
                .Append("\">");
        }

        _ = output.Append(Tag("img", image));

        if (wrap)
        {
            _ = output.Append("</a>");
        }
    }

    private static string StripTags(string html)
    {
        StringBuilder text = new StringBuilder();
        bool inside = false;

        foreach (char symbol in html)
        {
            if (symbol == '<')
            {
                inside = true;
            }
            else if (symbol == '>' && inside)
            {
                inside = false;
            }
            else if (!inside)
            {
                _ = text.Append(symbol);
            }
        }

        return text.ToString();
    }

    private void WriteHeading(
        StringBuilder output,
        string name,
        List<(string Name, string? Value)> kept,
        string inner
    )
    {
        string content = Sanitize(inner);
        string text = WebUtility.HtmlDecode(StripTags(content)).Trim();
        string slug = _slugs.Unique(
            new HeadingSlug(new Pure.Primitives.String.String(text)).TextValue
        );
        List<(string Name, string? Value)> attributes = [.. kept];
        attributes.Add(("class", "heading-element"));

        if (attributes.All(attribute => attribute.Name != "dir"))
        {
            attributes.Add(("dir", "auto"));
        }

        _ = output
            .Append("<div class=\"markdown-heading\" dir=\"auto\">")
            .Append(Tag(name, attributes))
            .Append(content)
            .Append("</")
            .Append(name)
            .Append("><a id=\"user-content-")
            .Append(Quote(slug))
            .Append("\" class=\"anchor\" aria-label=\"Permalink: ")
            .Append(Quote(text))
            .Append("\" href=\"#")
            .Append(Quote(slug))
            .Append("\">")
            .Append(GitHubHeadingRenderer.LinkIcon)
            .Append("</a></div>");
    }
}
