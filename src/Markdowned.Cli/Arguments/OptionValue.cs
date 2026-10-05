using Pure.Primitives.Abstractions.Char;
using Pure.Primitives.Abstractions.String;
using Char = Pure.Primitives.Char.Char;

namespace Markdowned.Cli.Arguments;

public sealed record OptionValue : IString
{
    private readonly IEnumerable<string> _names;

    private readonly IEnumerable<string> _arguments;

    public OptionValue(IEnumerable<string> names, IEnumerable<string> arguments)
    {
        _names = names;
        _arguments = arguments;
    }

    public string TextValue =>
        _arguments
            .Zip(_arguments.Skip(1), (name, value) => (name, value))
            .Where(pair => _names.Contains(pair.name, StringComparer.Ordinal))
            .Select(pair => pair.value)
            .FirstOrDefault()
        ?? string.Empty;

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
