using System.Text.Json;

namespace Markdowned.Abstractions.DevTools;

public interface IDevToolsSession : IAsyncDisposable
{
    public Task<JsonElement> Send(IDevToolsCommand command);
}
