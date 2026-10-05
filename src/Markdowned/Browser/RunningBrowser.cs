using System.Diagnostics;
using Markdowned.Abstractions.Browser;
using Pure.Primitives.Abstractions.String;

namespace Markdowned.Browser;

public sealed record RunningBrowser : IBrowser
{
    private readonly Process _process;

    private readonly string _profile;

    private readonly string _url;

    private readonly int[] _disposed = new int[1];

    internal RunningBrowser(Process process, string profile, string url)
    {
        _process = process;
        _profile = profile;
        _url = url;
    }

    public IString WebSocketUrl => new Pure.Primitives.String.String(_url);

    public async ValueTask DisposeAsync()
    {
        if (Interlocked.Exchange(ref _disposed[0], 1) == 1)
        {
            return;
        }

        try
        {
            if (!_process.HasExited)
            {
                _process.Kill(true);
            }

            await _process.WaitForExitAsync();
        }
        catch (InvalidOperationException)
        {
            // expected during shutdown
        }
        finally
        {
            _process.Dispose();
            Cleanup();
        }
    }

    private void Cleanup()
    {
        for (int attempt = 0; attempt < 5 && Directory.Exists(_profile); attempt++)
        {
            try
            {
                Directory.Delete(_profile, true);
            }
            catch (IOException)
            {
                Thread.Sleep(100);
            }
            catch (UnauthorizedAccessException)
            {
                Thread.Sleep(100);
            }
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
