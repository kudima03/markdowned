using Markdowned.Abstractions.Browser;
using Markdowned.Browser;
using Markdowned.Tests.Fakes;

namespace Markdowned.Tests.Browser;

public sealed record TimedLaunchTests
{
    [Fact]
    public async Task StartsTheCountdownOnceTheBrowserRuns()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer();
        using FakeBrowser fake = FakeBrowser.Listening(server.Url);
        using CancellationTokenSource limit = new CancellationTokenSource();

        await using IBrowser browser = await new TimedLaunch(
            new ChromiumLaunch(new Pure.Primitives.String.String(fake.Executable)),
            limit,
            TimeSpan.FromMilliseconds(50)
        ).Browser;

        Assert.Equal(server.Url, browser.WebSocketUrl.TextValue);
        await Task.Delay(500);
        Assert.True(limit.IsCancellationRequested);
    }

    [Fact]
    public async Task DoesNotCountDownWhenTheBrowserFailsToStart()
    {
        using FakeBrowser fake = FakeBrowser.Failing("boom");
        using CancellationTokenSource limit = new CancellationTokenSource();

        _ = await Assert.ThrowsAsync<BrowserException>(() =>
            new TimedLaunch(
                new ChromiumLaunch(new Pure.Primitives.String.String(fake.Executable)),
                limit,
                TimeSpan.FromMilliseconds(10)
            ).Browser
        );

        await Task.Delay(100);
        Assert.False(limit.IsCancellationRequested);
    }
}
