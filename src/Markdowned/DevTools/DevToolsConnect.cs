using System.Net.WebSockets;
using Markdowned.Abstractions.DevTools;
using Pure.Primitives.Abstractions.String;

namespace Markdowned.DevTools;

public sealed record DevToolsConnect : IDevToolsConnect
{
    private readonly IString _url;

    public DevToolsConnect(IString url)
    {
        _url = url;
    }

    public Task<IDevToolsSession> Session => Open();

    private async Task<IDevToolsSession> Open()
    {
        ClientWebSocket socket = new ClientWebSocket();
        socket.Options.KeepAliveInterval = TimeSpan.Zero;

        await socket.ConnectAsync(new Uri(_url.TextValue), CancellationToken.None);

        return new DevToolsConnection(socket);
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
