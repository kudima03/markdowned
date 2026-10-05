using Markdowned.Abstractions.Page;

namespace Markdowned.Page;

public sealed record EmbeddedResources : IPageResources
{
    public IEnumerator<IPageResource> GetEnumerator()
    {
        return typeof(EmbeddedResources)
            .Assembly.GetManifestResourceNames()
            .Where(name => name.StartsWith("assets/", StringComparison.Ordinal))
            .Select(name => new EmbeddedResource(name))
            .Cast<IPageResource>()
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
