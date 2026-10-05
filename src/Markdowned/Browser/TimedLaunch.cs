using Markdowned.Abstractions.Browser;

namespace Markdowned.Browser;

/// <summary>Starts a countdown that cancels <c>limit</c> once the browser is running.</summary>
public sealed record TimedLaunch : IBrowserLaunch
{
    private readonly IBrowserLaunch _inner;

    private readonly CancellationTokenSource _limit;

    private readonly TimeSpan _timeout;

    public TimedLaunch(
        IBrowserLaunch inner,
        CancellationTokenSource limit,
        TimeSpan timeout
    )
    {
        _inner = inner;
        _limit = limit;
        _timeout = timeout;
    }

    public Task<IBrowser> Browser => Start();

    private async Task<IBrowser> Start()
    {
        IBrowser browser = await _inner.Browser;
        _limit.CancelAfter(_timeout);

        return browser;
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
