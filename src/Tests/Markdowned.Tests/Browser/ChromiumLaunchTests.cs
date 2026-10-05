using Markdowned.Abstractions.Browser;
using Markdowned.Browser;
using Markdowned.Tests.Fakes;
using String = Pure.Primitives.String.String;

namespace Markdowned.Tests.Browser;

public sealed record ChromiumLaunchTests
{
    [Fact]
    public async Task ReadsDevToolsAddress()
    {
        using FakeBrowser fake = FakeBrowser.Listening(
            "ws://127.0.0.1:9/devtools/browser/x"
        );

        await using IBrowser browser = await new ChromiumLaunch(
            new String(fake.Executable)
        ).Browser;

        Assert.Equal(
            "ws://127.0.0.1:9/devtools/browser/x",
            browser.WebSocketUrl.TextValue
        );
    }

    [Fact]
    public async Task PassesHeadlessArguments()
    {
        using FakeBrowser fake = FakeBrowser.Listening("ws://127.0.0.1:9/x");

        await using (
            IBrowser browser = await new ChromiumLaunch(
                new String(fake.Executable)
            ).Browser
        )
        {
            string profile = (await File.ReadAllTextAsync(fake.ProfileRecord)).Trim();
            string arguments = await File.ReadAllTextAsync(Path.Combine(profile, "args"));

            Assert.Contains("--headless", arguments);
            Assert.Contains("--remote-debugging-port=0", arguments);
            Assert.Contains("--no-first-run", arguments);
            Assert.Equal(
                Environment.IsPrivilegedProcess,
                arguments.Contains("--no-sandbox", StringComparison.Ordinal)
            );
        }
    }

    [Fact]
    public async Task RemovesProfileOnDispose()
    {
        using FakeBrowser fake = FakeBrowser.Listening("ws://127.0.0.1:9/x");

        await (
            await new ChromiumLaunch(new String(fake.Executable)).Browser
        ).DisposeAsync();

        string profile = (await File.ReadAllTextAsync(fake.ProfileRecord)).Trim();

        Assert.False(Directory.Exists(profile));
    }

    [Fact]
    public async Task NamesPackagesWhenLibrariesAreMissing()
    {
        using FakeBrowser fake = FakeBrowser.Failing(
            "error while loading shared libraries: libnss3.so"
        );

        BrowserException error = await Assert.ThrowsAsync<BrowserException>(() =>
            new ChromiumLaunch(new String(fake.Executable)).Browser
        );

        Assert.Contains("apt install libnss3", error.Message);
    }

    [Fact]
    public async Task ExplainsMissingSandbox()
    {
        using FakeBrowser fake = FakeBrowser.Failing("FATAL: No usable sandbox!");

        BrowserException error = await Assert.ThrowsAsync<BrowserException>(() =>
            new ChromiumLaunch(new String(fake.Executable)).Browser
        );

        Assert.Contains("--no-sandbox", error.Message);
    }

    [Fact]
    public async Task ReportsUnknownFailure()
    {
        using FakeBrowser fake = FakeBrowser.Failing("something else");

        BrowserException error = await Assert.ThrowsAsync<BrowserException>(() =>
            new ChromiumLaunch(new String(fake.Executable)).Browser
        );

        Assert.Contains("did not report a DevTools address", error.Message);
    }

    [Fact]
    public async Task FailsWhenExecutableIsMissing()
    {
        BrowserException error = await Assert.ThrowsAsync<BrowserException>(() =>
            new ChromiumLaunch(new String("/nonexistent/browser")).Browser
        );

        Assert.Contains("/nonexistent/browser", error.Message);
    }
}
