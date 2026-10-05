using Markdowned.Markdown;
using String = Pure.Primitives.String.String;

namespace Markdowned.Tests.Fidelity;

public sealed record FidelityTests
{
    private static string Read(string name, string extension)
    {
        return File.ReadAllText(
            Path.Combine(AppContext.BaseDirectory, "Fidelity", name + extension)
        );
    }

    /// <summary>Fixtures that differ only in features that land in a later PR.</summary>
    private static readonly Dictionary<string, string> Pending = new Dictionary<
        string,
        string
    >
    {
        ["mermaid"] =
            "GitHub's server markup carries an iframe loader and random identities",
    };

    [Theory]
    [ClassData(typeof(FidelityFixtures))]
    public void RendersTheHtmlOfGitHub(string name)
    {
        if (Pending.ContainsKey(name))
        {
            return;
        }

        string expected = new NormalizedHtml(new String(Read(name, ".html"))).TextValue;
        string actual = new NormalizedHtml(
            new MarkdownHtml(new String(Read(name, ".md")))
        ).TextValue;

        if (expected != actual)
        {
            string directory = Path.Combine(AppContext.BaseDirectory, "Fidelity", "diff");
            _ = Directory.CreateDirectory(directory);
            File.WriteAllText(Path.Combine(directory, $"{name}.expected.txt"), expected);
            File.WriteAllText(Path.Combine(directory, $"{name}.actual.txt"), actual);
        }

        Assert.Equal(expected, actual);
    }
}
