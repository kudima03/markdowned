using System.Collections.Concurrent;
using System.Text.Json;
using System.Threading.Channels;

namespace Markdowned.DevTools;

internal sealed class DevToolsState
{
    public ConcurrentDictionary<int, TaskCompletionSource<JsonElement>> Pending { get; } =
        new ConcurrentDictionary<int, TaskCompletionSource<JsonElement>>();

    public Channel<JsonElement> Events { get; } = Channel.CreateUnbounded<JsonElement>();

    public int NextId => Interlocked.Increment(ref field);
}
