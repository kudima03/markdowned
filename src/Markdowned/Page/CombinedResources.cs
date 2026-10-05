using Markdowned.Abstractions.Page;

namespace Markdowned.Page;

public sealed record CombinedResources : IPageResources
{
    private readonly IEnumerable<IPageResources> _parts;

    public CombinedResources(params IEnumerable<IPageResources> parts)
    {
        _parts = parts;
    }

    public IEnumerator<IPageResource> GetEnumerator()
    {
        return _parts.SelectMany(part => part).GetEnumerator();
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
