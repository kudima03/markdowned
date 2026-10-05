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

        await foreach (IString line in new Command(new HttpClient(), arguments))
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
    public async Task RefusesToDownloadBrowserOffline()
    {
        if (!OperatingSystem.IsLinux())
        {
            return;
        }

        using TempDirectory cache = new TempDirectory();
        string? previous = Environment.GetEnvironmentVariable("XDG_CACHE_HOME");
        Environment.SetEnvironmentVariable("XDG_CACHE_HOME", cache.Path);

        try
        {
            string input = Path.Combine(cache.Path, "in.md");
            await File.WriteAllTextAsync(input, "# T");

            BrowserException error = await Assert.ThrowsAsync<BrowserException>(() =>
                Lines(input, "--offline")
            );

            Assert.Contains("--offline", error.Message);
        }
        finally
        {
            Environment.SetEnvironmentVariable("XDG_CACHE_HOME", previous);
        }
    }

    [Fact]
    public async Task PrintsPathOfInstalledBrowser()
    {
        if (!OperatingSystem.IsLinux())
        {
            return;
        }

        using TempDirectory cache = new TempDirectory();
        string? previous = Environment.GetEnvironmentVariable("XDG_CACHE_HOME");
        Environment.SetEnvironmentVariable("XDG_CACHE_HOME", cache.Path);

        try
        {
            string executable = new CachedBrowser(
                new CacheDirectory(),
                new PinnedDownload(new ChromePlatform())
            ).TextValue;
            _ = Directory.CreateDirectory(Path.GetDirectoryName(executable)!);
            await File.WriteAllTextAsync(executable, string.Empty);

            Assert.Equal(executable, Assert.Single(await Lines("install-browser")));
        }
        finally
        {
            Environment.SetEnvironmentVariable("XDG_CACHE_HOME", previous);
        }
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
    public async Task RejectsUnknownPaper()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer();
        using FakeBrowser fake = FakeBrowser.Listening(server.Url);
        using TempDirectory directory = new TempDirectory();
        string input = Path.Combine(directory.Path, "doc.md");
        await File.WriteAllTextAsync(input, "# Title");

        ArgumentException error = await Assert.ThrowsAsync<ArgumentException>(() =>
            Lines(input, "--browser", fake.Executable, "--paper", "B5")
        );

        Assert.Contains("A4, Letter or Legal", error.Message);
    }

    [Fact]
    public async Task RejectsMarginThatIsNotANumber()
    {
        ArgumentException error = await Assert.ThrowsAsync<ArgumentException>(() =>
            Lines("in.md", "--margin", "wide", "--browser", "x")
        );

        Assert.Contains("--margin expects a number", error.Message);
    }

    [Fact]
    public async Task PassesPaperLandscapeAndMarginToPrinting()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer();
        using FakeBrowser fake = FakeBrowser.Listening(server.Url);
        using TempDirectory directory = new TempDirectory();
        string input = Path.Combine(directory.Path, "doc.md");
        await File.WriteAllTextAsync(input, "# Title");

        _ = await Lines(
            input,
            "--browser",
            fake.Executable,
            "--paper",
            "Letter",
            "--landscape",
            "--margin",
            "10.5"
        );

        string printing = server.Messages.Single(message =>
            message.Contains("Page.printToPDF")
        );

        Assert.Contains("\"paperWidth\":8.5", printing);
        Assert.Contains("\"landscape\":true", printing);
        Assert.Contains($"\"marginTop\":{10.5 / 25.4}".Replace(',', '.'), printing);
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
