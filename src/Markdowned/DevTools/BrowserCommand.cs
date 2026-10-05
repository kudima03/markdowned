using Markdowned.Abstractions.DevTools;
using Pure.Primitives.Abstractions.String;
using String = Pure.Primitives.String.String;

namespace Markdowned.DevTools;

public sealed record BrowserCommand : IDevToolsCommand
{
    private readonly string _method;

    public BrowserCommand(string method, IString parameters)
    {
        _method = method;
        Parameters = parameters;
    }

    public IString Method => new String(_method);

    public IString Parameters { get; }

    public IString SessionId => new String(string.Empty);

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
