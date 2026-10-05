using Markdowned.Abstractions.Output;
using Markdowned.Cli.Arguments;
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

    private IString Input => new InputPath(_arguments);

    private IString Output
    {
        get
        {
            IString longForm = new OptionValue(["--output"], _arguments);

            return longForm.TextValue.Length > 0
                ? longForm
                : new OptionValue(["-o"], _arguments);
        }
    }

    private IOutput Chosen =>
        Has("--version") ? new TextOutput(new String(Version.Text))
        : Has("--help") || Input.TextValue.Length == 0
            ? new TextOutput(new String(Help.Text))
        : new PdfFile(Input, Output, new OptionValue(["--browser"], _arguments));

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
