using Markdowned.Abstractions.DevTools;
using Pure.Primitives.Abstractions.String;
using String = Pure.Primitives.String.String;

namespace Markdowned.DevTools;

public sealed record PageCommand : IDevToolsCommand
{
    private readonly string _sessionId;

    private readonly string _method;

    public PageCommand(string sessionId, string method, IString parameters)
    {
        _sessionId = sessionId;
        _method = method;
        Parameters = parameters;
    }

    public IString Method => new String(_method);

    public IString Parameters { get; }

    public IString SessionId => new String(_sessionId);

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
