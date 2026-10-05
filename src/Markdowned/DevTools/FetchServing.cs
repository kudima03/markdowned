using System.Text.Json;
using Markdowned.Abstractions.DevTools;
using Markdowned.Abstractions.Page;

namespace Markdowned.DevTools;

/// <summary>
/// Answers every paused request (our own origin is served, everything else fails) and
/// reports when the page has loaded.
/// </summary>
public sealed record FetchServing
{
    public const string Origin = "https://markdowned.local";

    private readonly IDevToolsSession _session;

    private readonly string _sessionId;

    private readonly IEnumerable<IPageResource> _resources;

    private readonly TaskCompletionSource<bool> _loaded;

    public FetchServing(
        IDevToolsSession session,
        string sessionId,
        IEnumerable<IPageResource> resources,
        TaskCompletionSource<bool> loaded
    )
    {
        _session = session;
        _sessionId = sessionId;
        _resources = resources;
        _loaded = loaded;
    }

    public Task Served => Serve();

    private async Task Serve()
    {
        await foreach (JsonElement message in _session.Events)
        {
            if (
                !message.TryGetProperty("sessionId", out JsonElement session)
                || session.GetString() != _sessionId
            )
            {
                continue;
            }

            if (message.GetProperty("method").GetString() == "Page.loadEventFired")
            {
                _ = _loaded.TrySetResult(true);
            }

            if (message.GetProperty("method").GetString() != "Fetch.requestPaused")
            {
                continue;
            }

            JsonElement paused = message.GetProperty("params");
            string requestId = paused.GetProperty("requestId").GetString()!;
            string url = paused.GetProperty("request").GetProperty("url").GetString()!;
            IPageResource? resource = _resources.FirstOrDefault(candidate =>
                $"{Origin}/{candidate.Path.TextValue}" == url.Split('?', '#')[0]
            );

            _ = await _session.Send(
                resource is null
                    ? new PageCommand(
                        _sessionId,
                        "Fetch.failRequest",
                        new JsonObject(
                            new KeyValuePair<string, object>("requestId", requestId),
                            new KeyValuePair<string, object>(
                                "errorReason",
                                "BlockedByClient"
                            )
                        )
                    )
                    : new PageCommand(
                        _sessionId,
                        "Fetch.fulfillRequest",
                        new JsonObject(
                            new KeyValuePair<string, object>("requestId", requestId),
                            new KeyValuePair<string, object>("responseCode", 200),
                            new KeyValuePair<string, object>(
                                "responseHeaders",
                                new JsonArray(
                                    new JsonObject(
                                        new KeyValuePair<string, object>(
                                            "name",
                                            "Content-Type"
                                        ),
                                        new KeyValuePair<string, object>(
                                            "value",
                                            resource.ContentType.TextValue
                                        )
                                    )
                                )
                            ),
                            new KeyValuePair<string, object>(
                                "body",
                                Convert.ToBase64String(resource.Content)
                            )
                        )
                    )
            );
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
