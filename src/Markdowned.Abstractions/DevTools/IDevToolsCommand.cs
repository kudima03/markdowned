using Pure.Primitives.Abstractions.String;

namespace Markdowned.Abstractions.DevTools;

public interface IDevToolsCommand
{
    public IString Method { get; }

    public IString Parameters { get; }

    public IString SessionId { get; }
}
