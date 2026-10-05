using System.Net.WebSockets;
using System.Text.Json;
using Markdowned.Abstractions.DevTools;

namespace Markdowned.DevTools;

public sealed record DevToolsConnection : IDevToolsSession
{
    private readonly ClientWebSocket _socket;

    private readonly DevToolsState _state;

    private readonly Task _reader;

    internal DevToolsConnection(ClientWebSocket socket)
    {
        _socket = socket;
        _state = new DevToolsState();
        _reader = Task.Run(Read);
    }

    public IAsyncEnumerable<JsonElement> Events => _state.Events.Reader.ReadAllAsync();

    public async Task<JsonElement> Send(IDevToolsCommand command)
    {
        int id = _state.NextId;
        TaskCompletionSource<JsonElement> response =
            new TaskCompletionSource<JsonElement>(
                TaskCreationOptions.RunContinuationsAsynchronously
            );
        _state.Pending[id] = response;

        using MemoryStream stream = new MemoryStream();
        using (Utf8JsonWriter writer = new Utf8JsonWriter(stream))
        {
            writer.WriteStartObject();
            writer.WriteNumber("id", id);
            writer.WriteString("method", command.Method.TextValue);
            writer.WritePropertyName("params");
            writer.WriteRawValue(command.Parameters.TextValue);

            if (command.SessionId.TextValue.Length > 0)
            {
                writer.WriteString("sessionId", command.SessionId.TextValue);
            }

            writer.WriteEndObject();
        }

        try
        {
            await _socket.SendAsync(
                stream.ToArray(),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None
            );
        }
        catch (Exception error)
            when (error
                    is WebSocketException
                        or ObjectDisposedException
                        or InvalidOperationException
            )
        {
            _ = _state.Pending.TryRemove(id, out TaskCompletionSource<JsonElement>? _);

            throw new DevToolsException("The browser closed the DevTools connection.");
        }

        return await response.Task;
    }

    public async ValueTask DisposeAsync()
    {
        if (_socket.State == WebSocketState.Open)
        {
            try
            {
                await _socket.CloseOutputAsync(
                    WebSocketCloseStatus.NormalClosure,
                    string.Empty,
                    CancellationToken.None
                );
            }
            catch (WebSocketException)
            {
                // expected during shutdown
            }
        }

        _ = await Task.WhenAny(_reader, Task.Delay(TimeSpan.FromSeconds(2)));
        _socket.Abort();
        await _reader;
        _socket.Dispose();
    }

    private async Task Read()
    {
        byte[] buffer = new byte[64 * 1024];

        try
        {
            while (true)
            {
                using MemoryStream message = new MemoryStream();
                WebSocketReceiveResult result;

                do
                {
                    result = await _socket.ReceiveAsync(buffer, CancellationToken.None);
                    message.Write(buffer, 0, result.Count);
                } while (!result.EndOfMessage);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    break;
                }

                Dispatch(message.ToArray());
            }
        }
        catch (Exception error)
            when (error
                    is WebSocketException
                        or ObjectDisposedException
                        or OperationCanceledException
            )
        {
            // expected during shutdown
        }
        finally
        {
            _ = _state.Events.Writer.TryComplete();

            foreach (TaskCompletionSource<JsonElement> pending in _state.Pending.Values)
            {
                _ = pending.TrySetException(
                    new DevToolsException("The browser closed the DevTools connection.")
                );
            }
        }
    }

    private void Dispatch(byte[] payload)
    {
        using JsonDocument document = JsonDocument.Parse(payload);
        JsonElement root = document.RootElement;

        if (root.TryGetProperty("method", out JsonElement _))
        {
            _ = _state.Events.Writer.TryWrite(root.Clone());

            return;
        }

        if (
            !root.TryGetProperty("id", out JsonElement id)
            || !_state.Pending.TryRemove(
                id.GetInt32(),
                out TaskCompletionSource<JsonElement>? waiter
            )
        )
        {
            return;
        }

        _ = root.TryGetProperty("error", out JsonElement error)
            ? waiter.TrySetException(
                new DevToolsException(error.GetProperty("message").GetString() ?? "error")
            )
            : waiter.TrySetResult(root.GetProperty("result").Clone());
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
