namespace Markdowned.Tests.Fakes;

public sealed class FakeBrowser : IDisposable
{
    private readonly string _directory = Directory
        .CreateTempSubdirectory("fake-browser-")
        .FullName;

    public FakeBrowser(string script)
    {
        Executable = Path.Combine(_directory, "browser.sh");
        File.WriteAllText(Executable, script);
        if (!OperatingSystem.IsWindows())
        {
            File.SetUnixFileMode(
                Executable,
                UnixFileMode.UserRead | UnixFileMode.UserWrite | UnixFileMode.UserExecute
            );
        }
    }

    public string Executable { get; }

    public string ProfileRecord => Path.Combine(_directory, "profile");

    public static FakeBrowser Listening(string url)
    {
        return new FakeBrowser(
            Script($"echo \"DevTools listening on {url}\" >&2\nexec sleep 60")
        );
    }

    public static FakeBrowser Failing(string message)
    {
        return new FakeBrowser($"#!/bin/sh\necho \"{message}\" >&2\nexit 127\n");
    }

    public static string Script(string tail)
    {
        return "#!/bin/sh\n"
            + "for a in \"$@\"; do case $a in --user-data-dir=*) d=${a#*=}; mkdir -p \"$d\";"
            + " echo \"$d\" > \"$(dirname \"$0\")/profile\"; echo \"$@\" > \"$d/args\";; esac; done\n"
            + tail
            + "\n";
    }

    public void Dispose()
    {
        Directory.Delete(_directory, true);
    }
}
