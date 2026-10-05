using Markdowned.Abstractions.Page;
using Pure.Primitives.Abstractions.String;
using String = Pure.Primitives.String.String;

namespace Markdowned.Page;

public sealed record EmbeddedResource : IPageResource
{
    private readonly string _name;

    public EmbeddedResource(string name)
    {
        _name = name;
    }

    public IString Path => new String(_name);

    public IString ContentType => new ContentType(Path);

    public byte[] Content
    {
        get
        {
            using Stream stream =
                typeof(EmbeddedResource).Assembly.GetManifestResourceStream(_name)
                ?? throw new InvalidOperationException($"{_name} is not embedded.");
            using MemoryStream bytes = new MemoryStream();
            stream.CopyTo(bytes);

            return bytes.ToArray();
        }
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
