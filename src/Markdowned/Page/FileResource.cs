using Markdowned.Abstractions.Page;
using Pure.Primitives.Abstractions.String;
using String = Pure.Primitives.String.String;

namespace Markdowned.Page;

public sealed record FileResource : IPageResource
{
    private readonly string _path;

    private readonly string _file;

    public FileResource(string path, string file)
    {
        _path = path;
        _file = file;
    }

    public IString Path => new String(_path);

    public IString ContentType => new ContentType(new String(_file));

    public byte[] Content => File.ReadAllBytes(_file);

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
