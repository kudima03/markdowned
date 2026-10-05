namespace Markdowned.Cli.Commands;

public static class Help
{
    public const string Text = """
        markdowned - render Markdown to a PDF that looks like github.com

        Usage: markdowned <input.md | -> [-o|--output <file.pdf | ->]
                          [--browser <path>] [--offline] [--quiet]
                          [--help] [--version]
               markdowned install-browser

        Without --browser, a pinned chrome-headless-shell is downloaded once into the
        per-user cache. 'install-browser' downloads it ahead of time.

        Exit codes: 0 success, 1 bad arguments or I/O error,
                    2 browser failed to launch, 3 render failure.
        """;
}
