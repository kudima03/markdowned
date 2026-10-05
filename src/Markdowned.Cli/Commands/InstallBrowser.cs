using Markdowned.Abstractions.Browser;
using Markdowned.Abstractions.Output;
using Pure.Primitives.Abstractions.String;

namespace Markdowned.Cli.Commands;

public sealed record InstallBrowser : IOutput
{
    private readonly IBrowserInstall _install;

    public InstallBrowser(IBrowserInstall install)
    {
        _install = install;
    }

    public async IAsyncEnumerator<IString> GetAsyncEnumerator(
        CancellationToken cancellationToken = default
    )
    {
        yield return await _install.Executable;
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
