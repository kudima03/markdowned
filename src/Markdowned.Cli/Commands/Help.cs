namespace Markdowned.Cli.Commands;

public static class Help
{
    public const string Text = """
        markdowned - render Markdown to a PDF that looks like github.com

        Usage: markdowned <input.md | -> [-o|--output <file.pdf | ->]
                          --browser <path> [--help] [--version]

        Exit codes: 0 success, 1 bad arguments or I/O error,
                    2 browser failed to launch, 3 render failure.
        """;
}
