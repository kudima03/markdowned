using Markdowned.Abstractions.Page;
using Pure.Primitives.Abstractions.String;

namespace Markdowned.Page;

public sealed record FirstFound : IResourceLookup
{
    private readonly IEnumerable<IResourceLookup> _lookups;

    public FirstFound(params IEnumerable<IResourceLookup> lookups)
    {
        _lookups = lookups;
    }

    public IPageResource? this[IString path] =>
        _lookups
            .Select(lookup => lookup[path])
            .FirstOrDefault(resource => resource is not null);

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
