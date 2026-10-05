using Markdowned.Abstractions.Browser;

namespace Markdowned.Browser;

public sealed record InstalledBrowserLaunch : IBrowserLaunch
{
    private readonly IBrowserInstall _install;

    public InstalledBrowserLaunch(IBrowserInstall install)
    {
        _install = install;
    }

    public Task<IBrowser> Browser => Start();

    private async Task<IBrowser> Start()
    {
        return await new ChromiumLaunch(await _install.Executable).Browser;
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
