using Pure.Primitives.Abstractions.Char;
using Pure.Primitives.Abstractions.String;
using Char = Pure.Primitives.Char.Char;

namespace Markdowned.Cli.Arguments;

public sealed record InputPath : IString
{
    private static readonly string[] ValueOptions = ["-o", "--output", "--browser"];

    private readonly IEnumerable<string> _arguments;

    public InputPath(IEnumerable<string> arguments)
    {
        _arguments = arguments;
    }

    public string TextValue =>
        _arguments
            .Select((argument, index) => (argument, index))
            .Where(item =>
                (item.argument == "-" || !item.argument.StartsWith('-'))
                && (
                    item.index == 0
                    || !ValueOptions.Contains(
                        _arguments.ElementAt(item.index - 1),
                        StringComparer.Ordinal
                    )
                )
            )
            .Select(item => item.argument)
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
