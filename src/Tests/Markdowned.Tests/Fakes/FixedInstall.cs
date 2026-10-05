using Markdowned.Abstractions.Browser;
using Pure.Primitives.Abstractions.String;
using String = Pure.Primitives.String.String;

namespace Markdowned.Tests.Fakes;

public sealed record FixedInstall : IBrowserInstall
{
    private readonly string _executable;

    public FixedInstall(string executable)
    {
        _executable = executable;
    }

    public Task<IString> Executable => Task.FromResult<IString>(new String(_executable));

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
