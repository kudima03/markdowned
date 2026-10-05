using System.Text;
using Pure.Primitives.Abstractions.Char;
using Pure.Primitives.Abstractions.String;
using Char = Pure.Primitives.Char.Char;

namespace Markdowned.DevTools;

public sealed record JsonArray : IString
{
    private readonly IEnumerable<IString> _items;

    public JsonArray(params IEnumerable<IString> items)
    {
        _items = items;
    }

    public string TextValue =>
        new StringBuilder()
            .Append('[')
            .AppendJoin(',', _items.Select(item => item.TextValue))
            .Append(']')
            .ToString();

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
