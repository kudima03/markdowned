namespace Markdowned.Abstractions.Browser;

public interface IBrowserLaunch
{
    public Task<IBrowser> Browser { get; }
}
