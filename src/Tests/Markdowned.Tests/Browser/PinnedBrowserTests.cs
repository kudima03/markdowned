using Markdowned.Browser;
using Markdowned.Tests.Fakes;
using String = Pure.Primitives.String.String;

namespace Markdowned.Tests.Browser;

public sealed record PinnedBrowserTests
{
    private static readonly string[] Platforms =
    [
        "linux64",
        "linux-arm64",
        "mac-arm64",
        "win64",
    ];

    [Fact]
    public void PinsEveryReleasePlatform()
    {
        foreach (string platform in Platforms)
        {
            PinnedDownload download = new PinnedDownload(new String(platform));

            Assert.Matches("^[0-9a-f]{64}$", download.Sha256.TextValue);
            Assert.Equal(
                $"https://storage.googleapis.com/chrome-for-testing-public/"
                    + $"{download.Version.TextValue}/{platform}/chrome-headless-shell-{platform}.zip",
                download.Url.TextValue
            );
            Assert.Equal(platform, download.Platform.TextValue);
        }
    }

    [Fact]
    public void RejectsUnpinnedPlatform()
    {
        _ = Assert.Throws<BrowserException>(() =>
            new PinnedDownload(new String("mac-x64")).Sha256
        );
    }

    [Fact]
    public void DetectsPlatformOfThisMachine()
    {
        Assert.Contains(new ChromePlatform().TextValue, Platforms);
        Assert.Equal(new ChromePlatform().TextValue.Length, new ChromePlatform().Count());
    }

    [Fact]
    public void NamesExecutableInsideVersionedCache()
    {
        string path = new CachedBrowser(
            new String("/cache"),
            new FixedDownload("u", "h")
        ).TextValue;

        Assert.Equal(
            Path.Combine(
                "/cache",
                "chrome-headless-shell",
                "1.0.0",
                "test",
                "chrome-headless-shell-test",
                "chrome-headless-shell"
            ),
            path
        );
    }

    [Fact]
    public void AddsExeSuffixOnWindowsPlatform()
    {
        Assert.EndsWith(
            "chrome-headless-shell.exe",
            new CachedBrowser(
                new String("/c"),
                new PinnedDownload(new String("win64"))
            ).TextValue
        );
    }

    [Fact]
    public void HonoursXdgCacheHome()
    {
        if (!OperatingSystem.IsLinux())
        {
            return;
        }

        string? previous = Environment.GetEnvironmentVariable("XDG_CACHE_HOME");
        Environment.SetEnvironmentVariable("XDG_CACHE_HOME", "/xdg");

        try
        {
            Assert.Equal(
                Path.Combine("/xdg", "markdowned"),
                new CacheDirectory().TextValue
            );
            Assert.Equal(
                Path.Combine("/xdg", "markdowned").Length,
                new CacheDirectory().Count()
            );
        }
        finally
        {
            Environment.SetEnvironmentVariable("XDG_CACHE_HOME", previous);
        }
    }

    [Fact]
    public void FallsBackToDotCache()
    {
        if (!OperatingSystem.IsLinux())
        {
            return;
        }

        string? previous = Environment.GetEnvironmentVariable("XDG_CACHE_HOME");
        Environment.SetEnvironmentVariable("XDG_CACHE_HOME", null);

        try
        {
            Assert.EndsWith(
                Path.Combine(".cache", "markdowned"),
                new CacheDirectory().TextValue
            );
        }
        finally
        {
            Environment.SetEnvironmentVariable("XDG_CACHE_HOME", previous);
        }
    }
}
