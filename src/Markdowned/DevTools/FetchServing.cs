using System.Text.Json;
using Markdowned.Abstractions.DevTools;
using Markdowned.Abstractions.Page;
using String = Pure.Primitives.String.String;

namespace Markdowned.DevTools;

/// <summary>
/// Answers every paused request and reports when the page has loaded. Our own origin is served
/// from the resources, images from the network pass only when remote images are allowed, and
/// everything else is blocked.
/// </summary>
public sealed record FetchServing
{
    public const string Origin = "https://markdowned.local";

    private readonly IDevToolsSession _session;

    private readonly string _sessionId;

    private readonly IResourceLookup _resources;

    private readonly bool _remoteImages;

    private readonly TaskCompletionSource<bool> _loaded;

    public FetchServing(
        IDevToolsSession session,
        string sessionId,
        IResourceLookup resources,
        bool remoteImages,
        TaskCompletionSource<bool> loaded
    )
    {
        _session = session;
        _sessionId = sessionId;
        _resources = resources;
        _remoteImages = remoteImages;
        _loaded = loaded;
    }

    public Task Served => Serve();

    private IDevToolsCommand Answer(string requestId, string url, string type)
    {
        IPageResource? resource = url.StartsWith($"{Origin}/", StringComparison.Ordinal)
            ? _resources[new String(url[(Origin.Length + 1)..].Split('?', '#')[0])]
            : null;

        if (resource is not null)
        {
            return Command(
                "Fetch.fulfillRequest",
                new KeyValuePair<string, object>("requestId", requestId),
                new KeyValuePair<string, object>("responseCode", 200),
                new KeyValuePair<string, object>(
                    "responseHeaders",
                    new JsonArray(
                        new JsonObject(
                            new KeyValuePair<string, object>("name", "Content-Type"),
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
            );
        }

        bool remoteImage =
            _remoteImages
            && type == "Image"
            && (
                url.StartsWith("https://", StringComparison.OrdinalIgnoreCase)
                || url.StartsWith("http://", StringComparison.OrdinalIgnoreCase)
            )
            && !url.StartsWith($"{Origin}/", StringComparison.Ordinal);

        return remoteImage
            ? Command(
                "Fetch.continueRequest",
                new KeyValuePair<string, object>("requestId", requestId)
            )
            : Command(
                "Fetch.failRequest",
                new KeyValuePair<string, object>("requestId", requestId),
                new KeyValuePair<string, object>("errorReason", "BlockedByClient")
            );
    }

    private IDevToolsCommand Command(
        string method,
        params IEnumerable<KeyValuePair<string, object>> parameters
    )
    {
        return new PageCommand(_sessionId, method, new JsonObject(parameters));
    }

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

            string method = message.GetProperty("method").GetString()!;

            if (method == "Page.loadEventFired")
            {
                _ = _loaded.TrySetResult(true);
            }

            if (method != "Fetch.requestPaused")
            {
                continue;
            }

            JsonElement paused = message.GetProperty("params");

            _ = await _session.Send(
                Answer(
                    paused.GetProperty("requestId").GetString()!,
                    paused.GetProperty("request").GetProperty("url").GetString()!,
                    paused.TryGetProperty("resourceType", out JsonElement type)
                        ? type.GetString() ?? string.Empty
                        : string.Empty
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
