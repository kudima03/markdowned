using System.Text.Json;

namespace Markdowned.Abstractions.DevTools;

public interface IDevToolsSession : IAsyncDisposable
{
    public IAsyncEnumerable<JsonElement> Events { get; }

    public Task<JsonElement> Send(IDevToolsCommand command);
}
