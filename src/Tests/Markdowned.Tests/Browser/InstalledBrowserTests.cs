using Markdowned.Abstractions.Browser;
using Markdowned.Browser;
using Markdowned.Tests.Fakes;
using String = Pure.Primitives.String.String;

namespace Markdowned.Tests.Browser;

public sealed record InstalledBrowserTests
{
    private static InstalledBrowser Install(
        string cache,
        IBrowserDownload download,
        bool offline = false,
        StringWriter? progress = null
    )
    {
        return new InstalledBrowser(
            new String(cache),
            download,
            new HttpClient(),
            offline,
            progress ?? new StringWriter()
        );
    }

    [Fact]
    public async Task DownloadsVerifiesAndExtracts()
    {
        await using FakeDownloadServer server = new FakeDownloadServer();
        using TempDirectory cache = new TempDirectory();
        StringWriter progress = new StringWriter();

        string path = (
            await Install(
                cache.Path,
                new FixedDownload(server.Url, server.Sha256),
                progress: progress
            ).Executable
        ).TextValue;

        Assert.True(File.Exists(path));
        Assert.Equal(
            new CachedBrowser(
                new String(cache.Path),
                new FixedDownload(server.Url, server.Sha256)
            ).TextValue,
            path
        );
        Assert.Contains(
            "Downloading chrome-headless-shell 1.0.0 (test)",
            progress.ToString()
        );
        Assert.Contains("100%", progress.ToString());
    }

    [Fact]
    public async Task MarksExecutableOnUnix()
    {
        if (OperatingSystem.IsWindows())
        {
            return;
        }

        await using FakeDownloadServer server = new FakeDownloadServer();
        using TempDirectory cache = new TempDirectory();

        string path = (
            await Install(
                cache.Path,
                new FixedDownload(server.Url, server.Sha256)
            ).Executable
        ).TextValue;

        Assert.True(File.GetUnixFileMode(path).HasFlag(UnixFileMode.UserExecute));
    }

    [Fact]
    public async Task ReusesCachedBrowser()
    {
        await using FakeDownloadServer server = new FakeDownloadServer();
        using TempDirectory cache = new TempDirectory();
        InstalledBrowser install = Install(
            cache.Path,
            new FixedDownload(server.Url, server.Sha256)
        );

        _ = await install.Executable;
        _ = await install.Executable;

        Assert.Equal(1, server.Requests);
    }

    [Fact]
    public async Task DownloadsOnceForParallelRuns()
    {
        await using FakeDownloadServer server = new FakeDownloadServer();
        using TempDirectory cache = new TempDirectory();
        FixedDownload download = new FixedDownload(server.Url, server.Sha256);

        _ = await Task.WhenAll(
            Install(cache.Path, download).Executable,
            Install(cache.Path, download).Executable,
            Install(cache.Path, download).Executable
        );

        Assert.Equal(1, server.Requests);
    }

    [Fact]
    public async Task RejectsWrongChecksum()
    {
        await using FakeDownloadServer server = new FakeDownloadServer();
        using TempDirectory cache = new TempDirectory();

        BrowserException error = await Assert.ThrowsAsync<BrowserException>(() =>
            Install(cache.Path, new FixedDownload(server.Url, "00")).Executable
        );

        Assert.Contains("SHA-256 mismatch", error.Message);
        Assert.Empty(
            Directory.EnumerateFileSystemEntries(
                Path.Combine(cache.Path, "chrome-headless-shell", "1.0.0")
            )
        );
    }

    [Fact]
    public async Task RejectsUnexpectedArchiveLayout()
    {
        await using FakeDownloadServer server = new FakeDownloadServer("other-folder");
        using TempDirectory cache = new TempDirectory();

        _ = await Assert.ThrowsAsync<BrowserException>(() =>
            Install(cache.Path, new FixedDownload(server.Url, server.Sha256)).Executable
        );
    }

    [Fact]
    public async Task ReportsHttpFailure()
    {
        await using FakeDownloadServer server = new FakeDownloadServer(status: 404);
        using TempDirectory cache = new TempDirectory();

        BrowserException error = await Assert.ThrowsAsync<BrowserException>(() =>
            Install(cache.Path, new FixedDownload(server.Url, "00")).Executable
        );

        Assert.Contains("Cannot download", error.Message);
    }

    [Fact]
    public async Task RefusesToDownloadOffline()
    {
        await using FakeDownloadServer server = new FakeDownloadServer();
        using TempDirectory cache = new TempDirectory();

        BrowserException error = await Assert.ThrowsAsync<BrowserException>(() =>
            Install(
                cache.Path,
                new FixedDownload(server.Url, server.Sha256),
                true
            ).Executable
        );

        Assert.Contains("install-browser", error.Message);
        Assert.Equal(0, server.Requests);
    }

    [Fact]
    public async Task UsesCachedBrowserOffline()
    {
        await using FakeDownloadServer server = new FakeDownloadServer();
        using TempDirectory cache = new TempDirectory();
        FixedDownload download = new FixedDownload(server.Url, server.Sha256);
        _ = await Install(cache.Path, download).Executable;

        Assert.NotEmpty((await Install(cache.Path, download, true).Executable).TextValue);
    }

    [Fact]
    public async Task LaunchesInstalledBrowser()
    {
        await using FakeDevToolsServer devTools = new FakeDevToolsServer();
        using FakeBrowser fake = FakeBrowser.Listening(devTools.Url);

        await using IBrowser browser = await new InstalledBrowserLaunch(
            new FixedInstall(fake.Executable)
        ).Browser;

        Assert.Equal(devTools.Url, browser.WebSocketUrl.TextValue);
    }
}
