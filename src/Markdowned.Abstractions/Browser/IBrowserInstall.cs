using Pure.Primitives.Abstractions.String;

namespace Markdowned.Abstractions.Browser;

public interface IBrowserInstall
{
    public Task<IString> Executable { get; }
}
