using Markdowned.DevTools;
using Pure.Primitives.Abstractions.Char;
using Pure.Primitives.Abstractions.String;
using Char = Pure.Primitives.Char.Char;

namespace Markdowned.Printing;

public sealed record PrintParameters : IString
{
    public string TextValue =>
        new JsonObject(
            new KeyValuePair<string, object>("paperWidth", 8.27),
            new KeyValuePair<string, object>("paperHeight", 11.69),
            new KeyValuePair<string, object>("printBackground", true),
            new KeyValuePair<string, object>("transferMode", "ReturnAsStream")
        ).TextValue;

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
