using Pure.Primitives.Abstractions.String;

namespace Markdowned.Abstractions.Page;

/// <summary>Finds a resource of the virtual origin by its path, without a leading slash.</summary>
public interface IResourceLookup
{
    public IPageResource? this[IString path] { get; }
}
