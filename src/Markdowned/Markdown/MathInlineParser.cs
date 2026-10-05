using Markdig.Helpers;
using Markdig.Parsers;

namespace Markdowned.Markdown;

/// <summary>
/// GitHub's math syntax. <c>$x$</c> needs a non-space after the opening and before the closing
/// dollar, and no digit after the closing one (so "$5 and $6" is text); <c>$`x`$</c> takes
/// the content verbatim; <c>$$x$$</c> is display math. Backslash escapes in the content are
/// resolved first, as on GitHub (so write <c>\\,</c> for a thin space).
/// </summary>
public sealed class MathInlineParser : InlineParser
{
    public MathInlineParser()
    {
        OpeningCharacters = ['$'];
    }

    private static string Unescape(string tex)
    {
        System.Text.StringBuilder text = new System.Text.StringBuilder();

        for (int index = 0; index < tex.Length; index++)
        {
            if (
                tex[index] == '\\'
                && index + 1 < tex.Length
                && !char.IsAsciiLetterOrDigit(tex[index + 1])
                && char.IsAscii(tex[index + 1])
                && !char.IsWhiteSpace(tex[index + 1])
            )
            {
                index++;
            }

            _ = text.Append(tex[index]);
        }

        return text.ToString();
    }

    public override bool Match(InlineProcessor processor, ref StringSlice slice)
    {
        string text = slice.Text;
        int start = slice.Start;
        int end = slice.End;

        if (start > 0 && text[start - 1] == '\\')
        {
            return false;
        }

        if (start + 1 <= end && text[start + 1] == '$')
        {
            int close = text.IndexOf("$$", start + 2, StringComparison.Ordinal);

            if (close < 0 || close > end - 1 || close == start + 2)
            {
                return false;
            }

            Emit(
                processor,
                ref slice,
                Unescape(text[(start + 2)..close]),
                true,
                close + 2
            );

            return true;
        }

        if (start + 1 <= end && text[start + 1] == '`')
        {
            int close = text.IndexOf("`$", start + 2, StringComparison.Ordinal);

            if (close < 0 || close > end - 1 || close == start + 2)
            {
                return false;
            }

            Emit(processor, ref slice, text[(start + 2)..close], false, close + 2);

            return true;
        }

        if (start + 1 > end || char.IsWhiteSpace(text[start + 1]))
        {
            return false;
        }

        for (int index = start + 1; index <= end; index++)
        {
            if (text[index] == '\\')
            {
                index++;
            }
            else if (text[index] == '$')
            {
                bool spaced = char.IsWhiteSpace(text[index - 1]);
                bool digit = index + 1 <= end && char.IsAsciiDigit(text[index + 1]);

                if (spaced || digit)
                {
                    return false;
                }

                Emit(
                    processor,
                    ref slice,
                    Unescape(text[(start + 1)..index]),
                    false,
                    index + 1
                );

                return true;
            }
        }

        return false;
    }

    private static void Emit(
        InlineProcessor processor,
        ref StringSlice slice,
        string tex,
        bool display,
        int next
    )
    {
        processor.Inline = new MathInline(tex, display);
        slice.Start = next;
    }
}
