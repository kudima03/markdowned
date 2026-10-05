using Markdowned.Abstractions.Page;
using Pure.Primitives.Abstractions.String;

namespace Markdowned.Page;

public sealed record ListedResources : IResourceLookup
{
    private readonly IEnumerable<IPageResource> _resources;

    public ListedResources(IEnumerable<IPageResource> resources)
    {
        _resources = resources;
    }

    public IPageResource? this[IString path] =>
        _resources.FirstOrDefault(resource => resource.Path.TextValue == path.TextValue);

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
