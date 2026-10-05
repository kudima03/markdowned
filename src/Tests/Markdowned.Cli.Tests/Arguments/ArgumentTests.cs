using Markdowned.Cli.Arguments;

namespace Markdowned.Cli.Tests.Arguments;

public sealed record ArgumentTests
{
    [Fact]
    public void ReadsValueOfOption()
    {
        Assert.Equal(
            "out.pdf",
            new OptionValue(["-o", "--output"], ["in.md", "-o", "out.pdf"]).TextValue
        );
    }

    [Fact]
    public void ReadsEmptyWhenOptionIsAbsent()
    {
        Assert.Empty(new OptionValue(["-o"], ["in.md"]).TextValue);
    }

    [Fact]
    public void ReadsEmptyWhenOptionHasNoValue()
    {
        Assert.Empty(new OptionValue(["-o"], ["in.md", "-o"]).TextValue);
    }

    [Fact]
    public void FindsInputBeforeOptions()
    {
        Assert.Equal("in.md", new InputPath(["in.md", "-o", "out.pdf"]).TextValue);
    }

    [Fact]
    public void SkipsOptionValues()
    {
        Assert.Equal(
            "in.md",
            new InputPath([
                "--browser",
                "/usr/bin/chrome",
                "-o",
                "x.pdf",
                "in.md",
            ]).TextValue
        );
    }

    [Fact]
    public void TreatsDashAsStdin()
    {
        Assert.Equal("-", new InputPath(["-", "-o", "-"]).TextValue);
    }

    [Fact]
    public void IgnoresFlags()
    {
        Assert.Empty(new InputPath(["--help"]).TextValue);
    }

    [Fact]
    public void EnumeratesCharacters()
    {
        Assert.Equal(5, new InputPath(["in.md"]).Count());
        Assert.Equal(3, new OptionValue(["-o"], ["-o", "a.b"]).Count());
    }
}
