using Markdowned.Abstractions.Browser;
using Markdowned.Abstractions.Output;
using Markdowned.Abstractions.Printing;
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

    public PdfFile(IString input, IString output, IBrowserLaunch browser)
    {
        _input = input;
        _output = output;
        _browser = browser;
    }

    private string Markdown =>
        _input.TextValue == "-"
            ? Console.In.ReadToEnd()
            : File.ReadAllText(_input.TextValue);

    private string Destination =>
        _output.TextValue.Length > 0 ? _output.TextValue
        : _input.TextValue == "-"
            ? throw new ArgumentException("Reading stdin requires -o <file.pdf | ->.")
        : Path.ChangeExtension(_input.TextValue, ".pdf");

    private IPdf Pdf =>
        new PdfOfHtml(
            _browser,
            new HtmlDocument(new MarkdownHtml(new String(Markdown))),
            new PrintParameters()
        );

    public async IAsyncEnumerator<IString> GetAsyncEnumerator(
        CancellationToken cancellationToken = default
    )
    {
        string destination = Destination;
        using MemoryStream bytes = new MemoryStream();

        await foreach (byte[] chunk in Pdf.WithCancellation(cancellationToken))
        {
            bytes.Write(chunk);
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
