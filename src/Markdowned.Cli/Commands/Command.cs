using Markdowned.Abstractions.Output;
using Markdowned.Markdown;
using Markdowned.Output;
using Pure.Primitives.Abstractions.String;
using String = Pure.Primitives.String.String;

namespace Markdowned.Cli.Commands;

public sealed record Command : IOutput
{
    private readonly IEnumerable<string> _arguments;

    public Command(params IEnumerable<string> arguments)
    {
        _arguments = arguments;
    }

    private bool Has(string name)
    {
        return _arguments.Contains(name, StringComparer.Ordinal);
    }

    private string? Input =>
        _arguments.FirstOrDefault(argument =>
            !argument.StartsWith("--", StringComparison.Ordinal)
        );

    private IOutput Chosen =>
        Has("--version") ? new TextOutput(new String(Version.Text))
        : Has("--help") || Input is null ? new TextOutput(new String(Help.Text))
        : new TextOutput(new MarkdownHtml(new String(File.ReadAllText(Input))));

    public IAsyncEnumerator<IString> GetAsyncEnumerator(
        CancellationToken cancellationToken = default
    )
    {
        return Chosen.GetAsyncEnumerator(cancellationToken);
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
