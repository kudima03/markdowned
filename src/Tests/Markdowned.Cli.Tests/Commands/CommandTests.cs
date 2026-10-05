using Markdowned.Browser;
using Markdowned.Tests.Fakes;
using Pure.Primitives.Abstractions.String;
using CliVersion = Markdowned.Cli.Commands.Version;
using Command = Markdowned.Cli.Commands.Command;
using Help = Markdowned.Cli.Commands.Help;

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
    public async Task RequiresBrowser()
    {
        ArgumentException error = await Assert.ThrowsAsync<ArgumentException>(() =>
            Lines("in.md")
        );

        Assert.Contains("--browser", error.Message);
    }

    [Fact]
    public async Task RequiresOutputForStdin()
    {
        ArgumentException error = await Assert.ThrowsAsync<ArgumentException>(() =>
            Lines("-", "--browser", "x")
        );

        Assert.Contains("-o", error.Message);
    }

    [Fact]
    public async Task FailsOnMissingInput()
    {
        _ = await Assert.ThrowsAnyAsync<IOException>(() =>
            Lines("/nonexistent/in.md", "--browser", "x")
        );
    }

    [Fact]
    public async Task WritesPdfNextToInput()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer();
        using FakeBrowser fake = FakeBrowser.Listening(server.Url);
        string directory = Directory.CreateTempSubdirectory().FullName;
        string input = Path.Combine(directory, "doc.md");
        await File.WriteAllTextAsync(input, "# Title");

        try
        {
            Assert.Empty(await Lines(input, "--browser", fake.Executable));
            Assert.Equal(
                "%PDF-fake",
                await File.ReadAllTextAsync(Path.Combine(directory, "doc.pdf"))
            );
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [Fact]
    public async Task WritesPdfToChosenOutput()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer();
        using FakeBrowser fake = FakeBrowser.Listening(server.Url);
        string directory = Directory.CreateTempSubdirectory().FullName;
        string input = Path.Combine(directory, "doc.md");
        string output = Path.Combine(directory, "out.pdf");
        await File.WriteAllTextAsync(input, "# Title");

        try
        {
            _ = await Lines(input, "-o", output, "--browser", fake.Executable);

            Assert.True(File.Exists(output));
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }

    [Fact]
    public async Task ReportsBrowserFailure()
    {
        using FakeBrowser fake = FakeBrowser.Failing("boom");
        string directory = Directory.CreateTempSubdirectory().FullName;
        string input = Path.Combine(directory, "doc.md");
        await File.WriteAllTextAsync(input, "# Title");

        try
        {
            _ = await Assert.ThrowsAsync<BrowserException>(() =>
                Lines(input, "--browser", fake.Executable)
            );
        }
        finally
        {
            Directory.Delete(directory, true);
        }
    }
}
