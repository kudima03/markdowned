using Markdowned.Abstractions.Browser;
using Pure.Primitives.Abstractions.String;
using String = Pure.Primitives.String.String;

namespace Markdowned.Tests.Fakes;

public sealed record FixedDownload : IBrowserDownload
{
    private readonly string _url;

    private readonly string _sha256;

    public FixedDownload(string url, string sha256)
    {
        _url = url;
        _sha256 = sha256;
    }

    public IString Version => new String("1.0.0");

    public IString Platform => new String("test");

    public IString Url => new String(_url);

    public IString Sha256 => new String(_sha256);

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
