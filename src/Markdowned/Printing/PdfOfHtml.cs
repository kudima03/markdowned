using System.Text.Json;
using Markdowned.Abstractions.Browser;
using Markdowned.Abstractions.DevTools;
using Markdowned.Abstractions.Markdown;
using Markdowned.Abstractions.Page;
using Markdowned.Abstractions.Printing;
using Markdowned.DevTools;
using Markdowned.Page;
using Pure.Primitives.Abstractions.String;

namespace Markdowned.Printing;

public sealed record PdfOfHtml : IPdf
{
    private readonly IBrowserLaunch _launch;

    private readonly IHtml _html;

    private readonly IPageResources _assets;

    private readonly IResourceLookup _local;

    private readonly bool _remoteImages;

    private readonly TextWriter _warnings;

    private readonly IString _parameters;

    public PdfOfHtml(
        IBrowserLaunch launch,
        IHtml html,
        IPageResources assets,
        IResourceLookup local,
        bool remoteImages,
        TextWriter warnings,
        IString parameters
    )
    {
        _launch = launch;
        _html = html;
        _assets = assets;
        _local = local;
        _remoteImages = remoteImages;
        _warnings = warnings;
        _parameters = parameters;
    }

    private const string BrokenImages =
        "window.markdownedReady.then(() => JSON.stringify([...document.images]"
        + ".filter(image => image.getAttribute('src') && (!image.complete || "
        + "(image.naturalWidth === 0 && !/\\.svg([?#]|$)/i.test(image.src))))"
        + ".map(image => image.src)))";

    private async Task Warn(JsonElement ready)
    {
        if (
            !ready.TryGetProperty("result", out JsonElement result)
            || !result.TryGetProperty("value", out JsonElement value)
            || value.GetString() is not { } json
        )
        {
            return;
        }

        using JsonDocument urls = JsonDocument.Parse(json);

        foreach (JsonElement url in urls.RootElement.EnumerateArray())
        {
            string shown = url.GetString()!
                .Replace(
                    $"{FetchServing.Origin}/",
                    string.Empty,
                    StringComparison.Ordinal
                );

            await _warnings.WriteLineAsync(
                _remoteImages || shown != url.GetString()
                    ? $"warning: image not loaded: {shown}"
                    : $"warning: image not loaded (--offline): {shown}"
            );
        }
    }

    public async IAsyncEnumerator<byte[]> GetAsyncEnumerator(
        CancellationToken cancellationToken = default
    )
    {
        cancellationToken.ThrowIfCancellationRequested();

        await using IBrowser browser = await _launch.Browser;
        await using CancellationTokenRegistration kill = cancellationToken.Register(() =>
            _ = browser.DisposeAsync().AsTask()
        );
        await using IDevToolsSession session = await new DevToolsConnect(
            browser.WebSocketUrl
        ).Session;

        await using CancellationTokenRegistration close = cancellationToken.Register(() =>
            _ = session.DisposeAsync().AsTask()
        );

        string targetId = (
            await session.Send(
                new BrowserCommand(
                    "Target.createTarget",
                    new JsonObject(new KeyValuePair<string, object>("url", "about:blank"))
                )
            )
        )
            .GetProperty("targetId")
            .GetString()!;

        string sessionId = (
            await session.Send(
                new BrowserCommand(
                    "Target.attachToTarget",
                    new JsonObject(
                        new KeyValuePair<string, object>("targetId", targetId),
                        new KeyValuePair<string, object>("flatten", true)
                    )
                )
            )
        )
            .GetProperty("sessionId")
            .GetString()!;

        _ = await session.Send(
            new PageCommand(sessionId, "Page.enable", new JsonObject())
        );

        _ = await session.Send(
            new PageCommand(
                sessionId,
                "Fetch.enable",
                new JsonObject(
                    new KeyValuePair<string, object>(
                        "patterns",
                        new JsonArray(
                            new JsonObject(
                                new KeyValuePair<string, object>("urlPattern", "*")
                            )
                        )
                    )
                )
            )
        );

        TaskCompletionSource<bool> loaded = new TaskCompletionSource<bool>(
            TaskCreationOptions.RunContinuationsAsynchronously
        );
        Task serving = new FetchServing(
            session,
            sessionId,
            new FirstFound(
                new ListedResources(
                    new CombinedResources(
                        _assets,
                        new SingleResource(new HtmlResource(_html))
                    )
                ),
                _local
            ),
            _remoteImages,
            loaded
        ).Served;

        Task ready = Task.WhenAll(
            session.Send(
                new PageCommand(
                    sessionId,
                    "Page.navigate",
                    new JsonObject(
                        new KeyValuePair<string, object>(
                            "url",
                            $"{FetchServing.Origin}/index.html"
                        )
                    )
                )
            ),
            loaded.Task
        );

        if (await Task.WhenAny(ready, serving) != ready)
        {
            await serving;

            throw new DevToolsException("The browser closed before the page loaded.");
        }

        await ready;

        JsonElement evaluated = await session.Send(
            new PageCommand(
                sessionId,
                "Runtime.evaluate",
                new JsonObject(
                    new KeyValuePair<string, object>("expression", BrokenImages),
                    new KeyValuePair<string, object>("awaitPromise", true),
                    new KeyValuePair<string, object>("returnByValue", true)
                )
            )
        );

        await Warn(evaluated);

        string handle = (
            await session.Send(new PageCommand(sessionId, "Page.printToPDF", _parameters))
        )
            .GetProperty("stream")
            .GetString()!;

        bool eof = false;

        while (!eof)
        {
            cancellationToken.ThrowIfCancellationRequested();

            JsonElement chunk = await session.Send(
                new PageCommand(
                    sessionId,
                    "IO.read",
                    new JsonObject(new KeyValuePair<string, object>("handle", handle))
                )
            );

            eof = chunk.GetProperty("eof").GetBoolean();

            yield return Convert.FromBase64String(chunk.GetProperty("data").GetString()!);
        }

        _ = await session.Send(
            new PageCommand(
                sessionId,
                "IO.close",
                new JsonObject(new KeyValuePair<string, object>("handle", handle))
            )
        );

        await session.DisposeAsync();

        try
        {
            await serving;
        }
        catch (DevToolsException)
        {
            // a request that was still paused when the browser was closed
        }
    }

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
