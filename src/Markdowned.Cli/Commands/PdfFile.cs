using Markdowned.Abstractions.Browser;
using Markdowned.Abstractions.Output;
using Markdowned.Abstractions.Printing;
using Markdowned.Browser;
using Markdowned.DevTools;
using Markdowned.Markdown;
using Markdowned.Page;
using Markdowned.Printing;
using Pure.Primitives.Abstractions.String;
using String = Pure.Primitives.String.String;

namespace Markdowned.Cli.Commands;

public sealed record PdfFile : IOutput
{
    private readonly IString _input;

    private readonly IString _output;

    private readonly IBrowserLaunch _browser;

    private readonly IString _paper;

    private readonly bool _landscape;

    private readonly double _margin;

    private readonly bool _offline;

    private readonly double _timeoutSeconds;

    private readonly TextWriter _warnings;

    public PdfFile(
        IString input,
        IString output,
        IBrowserLaunch browser,
        IString paper,
        bool landscape,
        double margin,
        bool offline,
        double timeoutSeconds,
        TextWriter warnings
    )
    {
        _input = input;
        _output = output;
        _browser = browser;
        _paper = paper;
        _landscape = landscape;
        _margin = margin;
        _offline = offline;
        _timeoutSeconds = timeoutSeconds;
        _warnings = warnings;
    }

    private string Markdown =>
        _input.TextValue == "-"
            ? Console.In.ReadToEnd()
            : File.ReadAllText(_input.TextValue);

    private string Directory =>
        _input.TextValue == "-"
            ? Environment.CurrentDirectory
            : Path.GetDirectoryName(Path.GetFullPath(_input.TextValue))!;

    private string Destination =>
        _output.TextValue.Length > 0 ? _output.TextValue
        : _input.TextValue == "-"
            ? throw new ArgumentException("Reading stdin requires -o <file.pdf | ->.")
        : Path.ChangeExtension(_input.TextValue, ".pdf");

    private TimeSpan Timeout =>
        _timeoutSeconds > 0
            ? TimeSpan.FromSeconds(_timeoutSeconds)
            : throw new ArgumentException("--timeout must be greater than zero seconds.");

    private IPdf Pdf(CancellationTokenSource limit)
    {
        return new PdfOfHtml(
            new TimedLaunch(_browser, limit, Timeout),
            new PageHtml(new MarkdownHtml(new String(Markdown))),
            new EmbeddedResources(),
            new LocalImages(new String(Directory)),
            !_offline,
            _warnings,
            new PrintParameters(_paper, _landscape, _margin)
        );
    }

    public async IAsyncEnumerator<IString> GetAsyncEnumerator(
        CancellationToken cancellationToken = default
    )
    {
        string destination = Destination;
        using MemoryStream bytes = new MemoryStream();
        using CancellationTokenSource limit =
            CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

        try
        {
            await foreach (byte[] chunk in Pdf(limit).WithCancellation(limit.Token))
            {
                bytes.Write(chunk);
            }
        }
        catch (Exception error)
            when (error is DevToolsException or OperationCanceledException
                && limit.IsCancellationRequested
                && !cancellationToken.IsCancellationRequested
            )
        {
            throw new DevToolsException(
                $"Rendering timed out after {_timeoutSeconds} seconds (--timeout)."
            );
        }

        if (destination == "-")
        {
            await using Stream stdout = Console.OpenStandardOutput();
            await stdout.WriteAsync(bytes.ToArray(), cancellationToken);
        }
        else
        {
            await File.WriteAllBytesAsync(
                destination,
                bytes.ToArray(),
                cancellationToken
            );
        }

        yield break;
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
