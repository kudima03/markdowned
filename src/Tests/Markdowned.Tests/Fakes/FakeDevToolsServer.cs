using System.Collections.Concurrent;
using System.Net;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace Markdowned.Tests.Fakes;

public sealed class FakeDevToolsServer : IAsyncDisposable
{
    private readonly HttpListener _listener = new HttpListener();

    private readonly Task _loop;

    private readonly string _failMethod;

    private readonly bool _neverLoads;

    public FakeDevToolsServer(string failMethod = "", bool neverLoads = false)
    {
        _failMethod = failMethod;
        _neverLoads = neverLoads;
        int port = FreePort();
        Url = $"ws://127.0.0.1:{port}/devtools/browser/fake";
        _listener.Prefixes.Add($"http://127.0.0.1:{port}/");
        _listener.Start();
        _loop = Task.Run(Accept);
    }

    public string Url { get; }

    public ConcurrentQueue<string> Messages { get; } = new ConcurrentQueue<string>();

    public IEnumerable<string> Methods =>
        Messages.Select(message =>
        {
            using JsonDocument document = JsonDocument.Parse(message);
            return document.RootElement.GetProperty("method").GetString()!;
        });

    public async ValueTask DisposeAsync()
    {
        _listener.Close();
        await _loop;
    }

    private static int FreePort()
    {
        using System.Net.Sockets.TcpListener probe = new System.Net.Sockets.TcpListener(
            IPAddress.Loopback,
            0
        );
        probe.Start();

        return ((IPEndPoint)probe.LocalEndpoint).Port;
    }

    private async Task Accept()
    {
        try
        {
            HttpListenerContext context = await _listener.GetContextAsync();
            HttpListenerWebSocketContext socket = await context.AcceptWebSocketAsync(
                null
            );
            await Serve(socket.WebSocket);
        }
        catch (HttpListenerException)
        {
            // expected during shutdown
        }
        catch (ObjectDisposedException)
        {
            // expected during shutdown
        }
    }

    private async Task Serve(WebSocket socket)
    {
        byte[] buffer = new byte[64 * 1024];

        while (socket.State == WebSocketState.Open)
        {
            WebSocketReceiveResult received = await socket.ReceiveAsync(
                buffer,
                CancellationToken.None
            );

            if (received.MessageType == WebSocketMessageType.Close)
            {
                break;
            }

            string message = Encoding.UTF8.GetString(buffer, 0, received.Count);
            Messages.Enqueue(message);

            using JsonDocument document = JsonDocument.Parse(message);
            int id = document.RootElement.GetProperty("id").GetInt32();
            string method = document.RootElement.GetProperty("method").GetString()!;

            if (method == "Fake.hangUp")
            {
                socket.Abort();
                break;
            }

            string reply =
                method == _failMethod
                    ? $$$"""{"id":{{{id}}},"error":{"message":"boom"}}"""
                    : $$$"""{"id":{{{id}}},"result":{{{Result(method)}}}}""";

            await Push(socket, reply);

            if (method == "Page.navigate")
            {
                if (_neverLoads)
                {
                    await socket.CloseOutputAsync(
                        WebSocketCloseStatus.NormalClosure,
                        string.Empty,
                        CancellationToken.None
                    );

                    break;
                }

                foreach (string paused in Paused())
                {
                    await Push(socket, paused);
                }
            }
        }
    }

    private static Task Push(WebSocket socket, string message)
    {
        return socket.SendAsync(
            Encoding.UTF8.GetBytes(message),
            WebSocketMessageType.Text,
            true,
            CancellationToken.None
        );
    }

    private static IEnumerable<string> Paused()
    {
        yield return /*lang=json,strict*/
        """{"method":"Page.loadEventFired","sessionId":"S1","params":{}}""";
        yield return Request("R0", "https://markdowned.local/index.html", "OTHER");
        yield return Request("R1", "https://markdowned.local/index.html", "S1");
        yield return Request("R2", "https://example.com/tracker.png", "S1");
        yield return Request(
            "R3",
            "https://markdowned.local/assets/page.css?v=1#x",
            "S1"
        );
    }

    private static string Request(string id, string url, string session)
    {
        return $$$$"""{"method":"Fetch.requestPaused","sessionId":"{{{{session}}}}","params":{"requestId":"{{{{id}}}}","request":{"url":"{{{{url}}}}"}}}""";
    }

    private static string Result(string method)
    {
        return method switch
        {
            "Target.createTarget" => /*lang=json,strict*/
            """{"targetId":"T1"}""",
            "Target.attachToTarget" => /*lang=json,strict*/
            """{"sessionId":"S1"}""",
            "Page.getFrameTree" => /*lang=json,strict*/
            """{"frameTree":{"frame":{"id":"F1"}}}""",
            "Page.printToPDF" => /*lang=json,strict*/
            """{"stream":"H1"}""",
            "IO.read" =>
                $$$"""{"data":"{{{Convert.ToBase64String("%PDF-fake"u8)}}}","base64Encoded":true,"eof":true}""",
            _ => "{}",
        };
    }
}
