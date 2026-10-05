using System.Text.Json;
using Markdowned.Abstractions.Browser;
using Pure.Primitives.Abstractions.String;
using String = Pure.Primitives.String.String;

namespace Markdowned.Browser;

public sealed record PinnedDownload : IBrowserDownload
{
    public PinnedDownload(IString platform)
    {
        Platform = platform;
    }

    private static JsonElement Pin
    {
        get
        {
            using Stream stream =
                typeof(PinnedDownload).Assembly.GetManifestResourceStream(
                    "Markdowned.Browser.browser.json"
                ) ?? throw new InvalidOperationException("browser.json is not embedded.");

            return JsonDocument.Parse(stream).RootElement;
        }
    }

    public IString Version => new String(Pin.GetProperty("version").GetString()!);

    public IString Platform { get; }

    public IString Url =>
        new String(
            $"{Pin.GetProperty("baseUrl").GetString()}/{Version.TextValue}/{Platform.TextValue}/"
                + $"chrome-headless-shell-{Platform.TextValue}.zip"
        );

    public IString Sha256 =>
        Pin.GetProperty("sha256").TryGetProperty(Platform.TextValue, out JsonElement hash)
            ? new String(hash.GetString()!)
            : throw new BrowserException(
                $"No pinned browser for {Platform.TextValue}. "
                    + "Pass --browser <path> to use a Chromium-based browser."
            );

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
