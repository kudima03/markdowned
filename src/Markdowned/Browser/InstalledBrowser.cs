using System.IO.Compression;
using System.Security.Cryptography;
using Markdowned.Abstractions.Browser;
using Pure.Primitives.Abstractions.String;
using String = Pure.Primitives.String.String;

namespace Markdowned.Browser;

public sealed record InstalledBrowser : IBrowserInstall
{
    private const int ProgressStepPercent = 10;

    private readonly IString _cache;

    private readonly IBrowserDownload _download;

    private readonly HttpClient _client;

    private readonly bool _offline;

    private readonly TextWriter _progress;

    public InstalledBrowser(
        IString cache,
        IBrowserDownload download,
        HttpClient client,
        bool offline,
        TextWriter progress
    )
    {
        _cache = cache;
        _download = download;
        _client = client;
        _offline = offline;
        _progress = progress;
    }

    private string CachedPath => new CachedBrowser(_cache, _download).TextValue;

    public Task<IString> Executable => Install();

    private async Task<IString> Install()
    {
        if (File.Exists(CachedPath))
        {
            return new String(CachedPath);
        }

        if (_offline)
        {
            throw new BrowserException(
                "No browser is cached and --offline is set. Run 'markdowned install-browser' "
                    + "while online, or pass --browser <path>."
            );
        }

        string root = Path.Combine(_cache.TextValue, "chrome-headless-shell");
        _ = Directory.CreateDirectory(root);

        await using FileStream guard = await Lock(Path.Combine(root, ".lock"));

        if (!File.Exists(CachedPath))
        {
            await Download(root);
        }

        return new String(CachedPath);
    }

    private static async Task<FileStream> Lock(string path)
    {
        DateTime giveUp = DateTime.UtcNow.AddMinutes(15);

        while (true)
        {
            try
            {
                return new FileStream(
                    path,
                    FileMode.OpenOrCreate,
                    FileAccess.ReadWrite,
                    FileShare.None
                );
            }
            catch (IOException) when (DateTime.UtcNow < giveUp)
            {
                await Task.Delay(TimeSpan.FromMilliseconds(250));
            }
        }
    }

    private async Task Download(string root)
    {
        string version = Path.Combine(root, _download.Version.TextValue);
        string target = Path.Combine(version, _download.Platform.TextValue);
        string archive = Path.Combine(
            version,
            $"{_download.Platform.TextValue}.zip.part"
        );
        string staging = $"{target}.staging-{Guid.NewGuid():N}";
        _ = Directory.CreateDirectory(version);

        try
        {
            await Fetch(archive);
            ZipFile.ExtractToDirectory(archive, staging);

            if (Directory.Exists(target))
            {
                Directory.Delete(target, true);
            }

            Directory.Move(staging, target);

            if (!File.Exists(CachedPath))
            {
                throw new BrowserException(
                    "The downloaded archive has an unexpected layout."
                );
            }

            if (!OperatingSystem.IsWindows())
            {
                File.SetUnixFileMode(
                    CachedPath,
                    UnixFileMode.UserRead
                        | UnixFileMode.UserWrite
                        | UnixFileMode.UserExecute
                        | UnixFileMode.GroupRead
                        | UnixFileMode.GroupExecute
                        | UnixFileMode.OtherRead
                        | UnixFileMode.OtherExecute
                );
            }
        }
        finally
        {
            File.Delete(archive);

            if (Directory.Exists(staging))
            {
                Directory.Delete(staging, true);
            }
        }
    }

    private async Task Fetch(string archive)
    {
        string label =
            $"chrome-headless-shell {_download.Version.TextValue} ({_download.Platform.TextValue})";
        await _progress.WriteLineAsync($"Downloading {label}");

        try
        {
            using HttpResponseMessage response = await _client.GetAsync(
                _download.Url.TextValue,
                HttpCompletionOption.ResponseHeadersRead
            );
            _ = response.EnsureSuccessStatusCode();
            long total = response.Content.Headers.ContentLength ?? 0;

            using IncrementalHash hash = IncrementalHash.CreateHash(
                HashAlgorithmName.SHA256
            );
            await using (Stream source = await response.Content.ReadAsStreamAsync())
            await using (FileStream destination = File.Create(archive))
            {
                byte[] buffer = new byte[81920];
                long done = 0;
                int reported = 0;
                int read;

                while ((read = await source.ReadAsync(buffer)) > 0)
                {
                    hash.AppendData(buffer, 0, read);
                    await destination.WriteAsync(buffer.AsMemory(0, read));
                    done += read;
                    int percent = total > 0 ? (int)(done * 100 / total) : 0;

                    if (percent >= reported + ProgressStepPercent)
                    {
                        reported = percent / ProgressStepPercent * ProgressStepPercent;
                        await _progress.WriteLineAsync(
                            $"  {reported}% ({done / 1_000_000} of {total / 1_000_000} MB)"
                        );
                    }
                }
            }

            string actual = Convert.ToHexStringLower(hash.GetHashAndReset());

            if (
                !string.Equals(
                    actual,
                    _download.Sha256.TextValue,
                    StringComparison.Ordinal
                )
            )
            {
                throw new BrowserException(
                    $"SHA-256 mismatch for {_download.Url.TextValue}: expected "
                        + $"{_download.Sha256.TextValue}, got {actual}."
                );
            }
        }
        catch (HttpRequestException error)
        {
            throw new BrowserException(
                $"Cannot download {_download.Url.TextValue}: {error.Message}"
            );
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
