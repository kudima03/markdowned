using Markdowned.Abstractions.Browser;
using Markdowned.Abstractions.Output;
using Markdowned.Browser;
using Markdowned.Cli.Arguments;
using Markdowned.Output;
using Pure.Primitives.Abstractions.String;
using String = Pure.Primitives.String.String;

namespace Markdowned.Cli.Commands;

public sealed record Command : IOutput
{
    private readonly HttpClient _client;

    private readonly IEnumerable<string> _arguments;

    public Command(HttpClient client, params IEnumerable<string> arguments)
    {
        _client = client;
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

    private IBrowserInstall Install =>
        new InstalledBrowser(
            new CacheDirectory(),
            new PinnedDownload(new ChromePlatform()),
            _client,
            Has("--offline"),
            Has("--quiet") ? TextWriter.Null : Console.Error
        );

    private IBrowserLaunch Launch
    {
        get
        {
            IString explicitPath = new OptionValue(["--browser"], _arguments);

            return explicitPath.TextValue.Length > 0
                ? new ChromiumLaunch(explicitPath)
                : new InstalledBrowserLaunch(Install);
        }
    }

    private IString Paper
    {
        get
        {
            IString requested = new OptionValue(["--paper"], _arguments);

            return requested.TextValue.Length > 0 ? requested : new String("A4");
        }
    }

    private double Margin
    {
        get
        {
            string requested = new OptionValue(["--margin"], _arguments).TextValue;

            return requested.Length == 0 ? 15
                : double.TryParse(
                    requested,
                    System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture,
                    out double millimetres
                )
                    ? millimetres
                : throw new ArgumentException(
                    $"--margin expects a number, got '{requested}'."
                );
        }
    }

    private IOutput Chosen =>
        Has("--version") ? new TextOutput(new String(Version.Text))
        : Has("--help") || Input.TextValue.Length == 0
            ? new TextOutput(new String(Help.Text))
        : Input.TextValue == "install-browser" ? new InstallBrowser(Install)
        : new PdfFile(Input, Output, Launch, Paper, Has("--landscape"), Margin);

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
