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

    private readonly IString _parameters;

    public PdfOfHtml(
        IBrowserLaunch launch,
        IHtml html,
        IPageResources assets,
        IString parameters
    )
    {
        _launch = launch;
        _html = html;
        _assets = assets;
        _parameters = parameters;
    }

    public async IAsyncEnumerator<byte[]> GetAsyncEnumerator(
        CancellationToken cancellationToken = default
    )
    {
        await using IBrowser browser = await _launch.Browser;
        await using IDevToolsSession session = await new DevToolsConnect(
            browser.WebSocketUrl
        ).Session;

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
            new CombinedResources(_assets, new SingleResource(new HtmlResource(_html))),
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

        _ = await session.Send(
            new PageCommand(
                sessionId,
                "Runtime.evaluate",
                new JsonObject(
                    new KeyValuePair<string, object>(
                        "expression",
                        "window.markdownedReady"
                    ),
                    new KeyValuePair<string, object>("awaitPromise", true)
                )
            )
        );

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
