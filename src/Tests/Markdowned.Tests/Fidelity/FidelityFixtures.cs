namespace Markdowned.Tests.Fidelity;

public sealed record FidelityFixtures : IEnumerable<object[]>
{
    private static string Directory => Path.Combine(AppContext.BaseDirectory, "Fidelity");

    public IEnumerator<object[]> GetEnumerator()
    {
        return System
            .IO.Directory.EnumerateFiles(Directory, "*.md")
            .Select(path => new object[] { Path.GetFileNameWithoutExtension(path) })
            .OrderBy(item => (string)item[0], StringComparer.Ordinal)
            .GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
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
