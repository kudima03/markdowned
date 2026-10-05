using Markdig.Helpers;
using Markdig.Parsers;
using Markdig.Syntax.Inlines;

namespace Markdowned.Markdown;

/// <summary>GFM extended email autolink: <c>user@example.com</c> outside angle brackets.</summary>
public sealed class EmailAutolinkParser : InlineParser
{
    public EmailAutolinkParser()
    {
        OpeningCharacters = ['@'];
    }

    private static bool IsLocal(char symbol)
    {
        return char.IsAsciiLetterOrDigit(symbol) || symbol is '.' or '-' or '_' or '+';
    }

    private static bool IsDomain(char symbol)
    {
        return char.IsAsciiLetterOrDigit(symbol) || symbol is '.' or '-' or '_';
    }

    public override bool Match(InlineProcessor processor, ref StringSlice slice)
    {
        if (processor.Inline is not LiteralInline previous)
        {
            return false;
        }

        string text = previous.Content.ToString();
        int local = text.Length;

        while (local > 0 && IsLocal(text[local - 1]))
        {
            local--;
        }

        if (local == text.Length)
        {
            return false;
        }

        int end = slice.Start + 1;

        while (end <= slice.End && IsDomain(slice.Text[end]))
        {
            end++;
        }

        string domain = slice.Text[(slice.Start + 1)..end].TrimEnd('.', '-', '_');
        string[] labels = domain.Split('.');

        if (labels.Length < 2 || labels.Any(label => label.Length == 0))
        {
            return false;
        }

        string address = text[local..] + "@" + domain;
        previous.Content.End -= text.Length - local;
        LinkInline link = new LinkInline($"mailto:{address}", string.Empty)
        {
            IsClosed = true,
            IsAutoLink = true,
        };
        _ = link.AppendChild(new LiteralInline(address));
        processor.Inline = link;
        slice.Start += 1 + domain.Length;

        return true;
    }
}
