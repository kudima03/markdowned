namespace Markdowned.Abstractions.DevTools;

public interface IDevToolsConnect
{
    public Task<IDevToolsSession> Session { get; }
}
