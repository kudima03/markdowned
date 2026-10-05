using Markdowned.Cli.Commands;
using Pure.Primitives.Abstractions.String;
using CliVersion = Markdowned.Cli.Commands.Version;

namespace Markdowned.Cli.Tests.Commands;

public sealed record CommandTests
{
    private static async Task<List<string>> Lines(params string[] arguments)
    {
        List<string> lines = [];

        await foreach (IString line in new Command(arguments))
        {
            lines.Add(line.TextValue);
        }

        return lines;
    }

    [Fact]
    public async Task PrintsVersion()
    {
        Assert.Equal(CliVersion.Text, Assert.Single(await Lines("--version")));
    }

    [Fact]
    public async Task PrintsHelp()
    {
        Assert.Equal(Help.Text, Assert.Single(await Lines("--help")));
    }

    [Fact]
    public async Task PrintsHelpWithoutInput()
    {
        Assert.Equal(Help.Text, Assert.Single(await Lines()));
    }

    [Fact]
    public async Task PrintsHtmlOfInputFile()
    {
        string path = Path.GetTempFileName();
        await File.WriteAllTextAsync(path, "# Title");

        try
        {
            Assert.Contains("<h1", Assert.Single(await Lines(path)));
        }
        finally
        {
            File.Delete(path);
        }
    }
}
