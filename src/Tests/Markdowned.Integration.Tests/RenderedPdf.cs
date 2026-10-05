using System.Text;
using Markdowned.Cli.Commands;

namespace Markdowned.Integration.Tests;

/// <summary>Renders Markdown with the real CLI command and the browser from MARKDOWNED_BROWSER.</summary>
public sealed class RenderedPdf : IDisposable
{
    private readonly string _directory = Directory
        .CreateTempSubdirectory("markdowned-it-")
        .FullName;

    public RenderedPdf(string markdown, params string[] options)
    {
        string input = Path.Combine(_directory, "doc.md");
        File.WriteAllText(input, markdown);
        Run(input, options).GetAwaiter().GetResult();
        Bytes = File.ReadAllBytes(Path.Combine(_directory, "doc.pdf"));
    }

    public byte[] Bytes { get; }

    public string Text => Encoding.Latin1.GetString(Bytes);

    private static async Task Run(string input, string[] options)
    {
        string browser = Environment.GetEnvironmentVariable("MARKDOWNED_BROWSER")!;

        _ = await new Command(
            new HttpClient(),
            [input, "--browser", browser, "--timeout", "90", .. options]
        ).ToListAsync();
    }

    public void Dispose()
    {
        Directory.Delete(_directory, true);
    }
}
