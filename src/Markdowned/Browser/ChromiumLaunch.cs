using System.Diagnostics;
using Markdowned.Abstractions.Browser;
using Pure.Primitives.Abstractions.String;

namespace Markdowned.Browser;

public sealed record ChromiumLaunch : IBrowserLaunch
{
    private const string LibrariesHint =
        "Install the shared libraries Chromium needs, e.g. on Debian/Ubuntu: "
        + "sudo apt install libnss3 libgbm1 libasound2t64 libatk-bridge2.0-0 "
        + "libcups2 libxkbcommon0 libxcomposite1 libxdamage1 libxrandr2 libpango-1.0-0";

    private const string SandboxHint =
        "Chromium's sandbox is unavailable (unprivileged user namespaces are disabled, "
        + "e.g. by AppArmor on Ubuntu 23.10+). Allow them, run in a container as root, "
        + "or point --browser at a wrapper script that adds --no-sandbox.";

    private readonly IString _executable;

    public ChromiumLaunch(IString executable)
    {
        _executable = executable;
    }

    public Task<IBrowser> Browser => Start();

    private async Task<IBrowser> Start()
    {
        string profile = Path.Combine(
            Path.GetTempPath(),
            $"markdowned-{Guid.NewGuid():N}"
        );
        ProcessStartInfo info = new ProcessStartInfo(_executable.TextValue)
        {
            RedirectStandardError = true,
            RedirectStandardOutput = true,
            UseShellExecute = false,
        };

        foreach (
            string argument in new[]
            {
                "--headless",
                "--remote-debugging-port=0",
                $"--user-data-dir={profile}",
                "--no-first-run",
                "--disable-gpu",
            }
        )
        {
            info.ArgumentList.Add(argument);
        }

        if (Environment.IsPrivilegedProcess)
        {
            info.ArgumentList.Add("--no-sandbox");
        }

        Process process;

        try
        {
            Process? started = Process.Start(info);
            process = started ?? throw new BrowserException("The browser did not start.");
        }
        catch (Exception error)
            when (error is System.ComponentModel.Win32Exception or IOException)
        {
            throw new BrowserException(
                $"Cannot start the browser '{_executable.TextValue}': {error.Message}"
            );
        }

        _ = process.StandardOutput.ReadToEndAsync();
        string url = await WebSocketAddress(process, profile);
        RunningBrowser running = new RunningBrowser(process, profile, url);

        return running;
    }

    private async Task<string> WebSocketAddress(Process process, string profile)
    {
        List<string> lines = [];
        using CancellationTokenSource timeout = new CancellationTokenSource(
            TimeSpan.FromSeconds(30)
        );

        try
        {
            while (
                await process.StandardError.ReadLineAsync(timeout.Token) is string line
            )
            {
                lines.Add(line);
                const string marker = "DevTools listening on ";
                int at = line.IndexOf(marker, StringComparison.Ordinal);

                if (at >= 0)
                {
                    _ = process.StandardError.ReadToEndAsync();
                    return line[(at + marker.Length)..].Trim();
                }
            }
        }
        catch (OperationCanceledException)
        {
            // expected during shutdown
        }

        await new RunningBrowser(process, profile, string.Empty).DisposeAsync();
        string output = string.Join(Environment.NewLine, lines);

        throw new BrowserException(
            output.Contains(
                "error while loading shared libraries",
                StringComparison.Ordinal
            )
                ? $"{output}{Environment.NewLine}{LibrariesHint}"
            : output.Contains("No usable sandbox", StringComparison.Ordinal)
                ? $"{output}{Environment.NewLine}{SandboxHint}"
            : $"The browser '{_executable.TextValue}' did not report a DevTools address.{Environment.NewLine}{output}"
        );
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
