using Markdowned.Abstractions.Output;
using Pure.Primitives.Abstractions.String;

namespace Markdowned.Output;

public sealed record TextOutput : IOutput
{
    private readonly IString _text;

    public TextOutput(IString text)
    {
        _text = text;
    }

    public async IAsyncEnumerator<IString> GetAsyncEnumerator(
        CancellationToken cancellationToken = default
    )
    {
        await Task.CompletedTask;
        yield return _text;
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
