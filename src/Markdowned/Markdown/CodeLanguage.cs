using System.Text.Json;
using Pure.Primitives.Abstractions.Char;
using Pure.Primitives.Abstractions.String;
using Char = Pure.Primitives.Char.Char;

namespace Markdowned.Markdown;

/// <summary>
/// The TextMate scope (for example <c>source.js</c>) of a code fence's info string, found the way
/// starry-night finds it: by language name first, then by file extension. Empty when unknown.
/// </summary>
public sealed record CodeLanguage : IString
{
    public CodeLanguage(string info)
    {
        TextValue = info;
    }

    private static JsonElement Languages
    {
        get
        {
            using Stream stream =
                typeof(CodeLanguage).Assembly.GetManifestResourceStream(
                    "assets/languages.json"
                )
                ?? throw new InvalidOperationException("languages.json is not embedded.");

            return JsonDocument.Parse(stream).RootElement;
        }
    }

    public string TextValue
    {
        get
        {
            string flag = (
                field
                    .Split((char[]?)null, 2, StringSplitOptions.RemoveEmptyEntries)
                    .FirstOrDefault()
                ?? string.Empty
            )
                .Trim()
                .ToLowerInvariant();

            if (flag.Length == 0)
            {
                return string.Empty;
            }

            JsonElement languages = Languages;
            JsonElement flags = languages.GetProperty("flags");
            JsonElement extensions = languages.GetProperty("extensions");
            int dot = flag.LastIndexOf('.');
            string extension = dot < 0 ? "." + flag : flag[dot..];

            return flags.TryGetProperty(flag, out JsonElement scope) ? scope.GetString()!
                : extensions.TryGetProperty(extension, out JsonElement byExtension)
                    ? byExtension.GetString()!
                : string.Empty;
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
