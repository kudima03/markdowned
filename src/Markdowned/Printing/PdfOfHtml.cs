using System.Text.Json;
using Markdowned.Abstractions.Browser;
using Markdowned.Abstractions.DevTools;
using Markdowned.Abstractions.Markdown;
using Markdowned.Abstractions.Printing;
using Markdowned.DevTools;
using Pure.Primitives.Abstractions.String;

namespace Markdowned.Printing;

public sealed record PdfOfHtml : IPdf
{
    private readonly IBrowserLaunch _launch;

    private readonly IHtml _html;

    private readonly IString _parameters;

    public PdfOfHtml(IBrowserLaunch launch, IHtml html, IString parameters)
    {
        _launch = launch;
        _html = html;
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

        string frameId = (
            await session.Send(
                new PageCommand(sessionId, "Page.getFrameTree", new JsonObject())
            )
        )
            .GetProperty("frameTree")
            .GetProperty("frame")
            .GetProperty("id")
            .GetString()!;

        _ = await session.Send(
            new PageCommand(
                sessionId,
                "Page.setDocumentContent",
                new JsonObject(
                    new KeyValuePair<string, object>("frameId", frameId),
                    new KeyValuePair<string, object>("html", _html.TextValue)
                )
            )
        );

        _ = await session.Send(
            new PageCommand(
                sessionId,
                "Runtime.evaluate",
                new JsonObject(
                    new KeyValuePair<string, object>(
                        "expression",
                        "document.fonts.ready.then(() => true)"
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
