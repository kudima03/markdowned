namespace Markdowned.Cli.Commands;

public static class Help
{
    public const string Text = """
        markdowned - render Markdown to a PDF that looks like github.com

        Usage: markdowned <input.md> [--help] [--version]

        PDF output is not implemented yet; the input is printed as HTML.
        """;
}
