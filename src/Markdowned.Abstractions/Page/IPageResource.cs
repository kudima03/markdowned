using Pure.Primitives.Abstractions.String;

namespace Markdowned.Abstractions.Page;

public interface IPageResource
{
    public IString Path { get; }

    public IString ContentType { get; }

    public byte[] Content { get; }
}
