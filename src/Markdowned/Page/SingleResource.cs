using Markdowned.Abstractions.Page;

namespace Markdowned.Page;

public sealed record SingleResource : IPageResources
{
    private readonly IPageResource _resource;

    public SingleResource(IPageResource resource)
    {
        _resource = resource;
    }

    public IEnumerator<IPageResource> GetEnumerator()
    {
        return new[] { _resource }.AsEnumerable().GetEnumerator();
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
