using Pure.Primitives.Abstractions.String;

namespace Markdowned.Abstractions.Browser;

public interface IBrowserDownload
{
    public IString Version { get; }

    public IString Platform { get; }

    public IString Url { get; }

    public IString Sha256 { get; }
}
