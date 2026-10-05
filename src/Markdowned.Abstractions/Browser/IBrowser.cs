using Pure.Primitives.Abstractions.String;

namespace Markdowned.Abstractions.Browser;

public interface IBrowser : IAsyncDisposable
{
    public IString WebSocketUrl { get; }
}
