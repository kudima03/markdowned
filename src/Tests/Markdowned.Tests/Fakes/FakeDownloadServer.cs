using System.IO.Compression;
using System.Net;
using System.Security.Cryptography;

namespace Markdowned.Tests.Fakes;

public sealed class FakeDownloadServer : IAsyncDisposable
{
    private readonly HttpListener _listener = new HttpListener();

    private readonly Task _loop;

    private readonly byte[] _archive;

    private readonly int _status;

    private int _requests;

    public FakeDownloadServer(
        string folder = "chrome-headless-shell-test",
        int status = 200
    )
    {
        _status = status;
        _archive = Archive(folder);
        using System.Net.Sockets.TcpListener probe = new System.Net.Sockets.TcpListener(
            IPAddress.Loopback,
            0
        );
        probe.Start();
        int port = ((IPEndPoint)probe.LocalEndpoint).Port;
        probe.Stop();
        Url = $"http://127.0.0.1:{port}/browser.zip";
        _listener.Prefixes.Add($"http://127.0.0.1:{port}/");
        _listener.Start();
        _loop = Task.Run(Serve);
    }

    public string Url { get; }

    public string Sha256 => Convert.ToHexStringLower(SHA256.HashData(_archive));

    public int Requests => _requests;

    public async ValueTask DisposeAsync()
    {
        _listener.Close();
        await _loop;
    }

    private static byte[] Archive(string folder)
    {
        using MemoryStream stream = new MemoryStream();
        using (ZipArchive archive = new ZipArchive(stream, ZipArchiveMode.Create, true))
        {
            using Stream entry = archive
                .CreateEntry($"{folder}/chrome-headless-shell")
                .Open();
            entry.Write("#!/bin/sh\n"u8);
        }

        return stream.ToArray();
    }

    private async Task Serve()
    {
        try
        {
            while (true)
            {
                HttpListenerContext context = await _listener.GetContextAsync();
                _ = Interlocked.Increment(ref _requests);
                context.Response.StatusCode = _status;
                context.Response.ContentLength64 = _status == 200 ? _archive.Length : 0;

                if (_status == 200)
                {
                    await context.Response.OutputStream.WriteAsync(_archive);
                }

                context.Response.Close();
            }
        }
        catch (HttpListenerException)
        {
            // listener closed
        }
        catch (ObjectDisposedException)
        {
            // listener closed
        }
    }
}
