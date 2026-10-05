using System.Runtime.InteropServices;
using Pure.Primitives.Abstractions.Char;
using Pure.Primitives.Abstractions.String;
using Char = Pure.Primitives.Char.Char;

namespace Markdowned.Browser;

public sealed record ChromePlatform : IString
{
    public string TextValue =>
        RuntimeInformation.ProcessArchitecture switch
        {
            Architecture.X64 when OperatingSystem.IsLinux() => "linux64",
            Architecture.Arm64 when OperatingSystem.IsLinux() => "linux-arm64",
            Architecture.Arm64 when OperatingSystem.IsMacOS() => "mac-arm64",
            Architecture.X64 when OperatingSystem.IsWindows() => "win64",
            Architecture.X86 => throw new NotImplementedException(),
            Architecture.Arm => throw new NotImplementedException(),
            Architecture.Wasm => throw new NotImplementedException(),
            Architecture.S390x => throw new NotImplementedException(),
            Architecture.LoongArch64 => throw new NotImplementedException(),
            Architecture.Armv6 => throw new NotImplementedException(),
            Architecture.Ppc64le => throw new NotImplementedException(),
            Architecture.RiscV64 => throw new NotImplementedException(),
            _ => throw new BrowserException(
                $"No pinned browser for {RuntimeInformation.RuntimeIdentifier}. "
                    + "Pass --browser <path> to use a Chromium-based browser."
            ),
        };

    public IEnumerator<IChar> GetEnumerator()
    {
        return TextValue.Select(symbol => new Char(symbol)).Cast<IChar>().GetEnumerator();
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
