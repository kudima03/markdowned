using Markdowned.Output;
using Pure.Primitives.Abstractions.String;
using String = Pure.Primitives.String.String;

namespace Markdowned.Tests.Output;

public sealed record TextOutputTests
{
    [Fact]
    public async Task YieldsSingleLine()
    {
        List<IString> lines = [];

        await foreach (IString line in new TextOutput(new String("text")))
        {
            lines.Add(line);
        }

        Assert.Equal("text", Assert.Single(lines).TextValue);
    }
}
