using Markdowned.Abstractions.Page;
using Markdowned.Abstractions.Printing;
using Markdowned.Browser;
using Markdowned.DevTools;
using Markdowned.Markdown;
using Markdowned.Page;
using Markdowned.Printing;
using Markdowned.Tests.Fakes;
using String = Pure.Primitives.String.String;

namespace Markdowned.Tests.Printing;

public sealed record PdfOfHtmlTests
{
    private static int AnswerCount(FakeDevToolsServer server)
    {
        return server.Messages.Count(message =>
            message.Contains("Fetch.fulfill")
            || message.Contains("Fetch.fail")
            || message.Contains("Fetch.continue")
        );
    }

    private static IPdf Pdf(
        FakeBrowser fake,
        bool remoteImages = true,
        TextWriter? warnings = null,
        IResourceLookup? local = null
    )
    {
        return new PdfOfHtml(
            new ChromiumLaunch(new String(fake.Executable)),
            new PageHtml(new MarkdownHtml(new String("# Hi"))),
            new EmbeddedResources(),
            local ?? new ListedResources([]),
            remoteImages,
            warnings ?? TextWriter.Null,
            new PrintParameters(new String("A4"), false, 15)
        );
    }

    [Fact]
    public async Task StreamsPdfBytes()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer();
        using FakeBrowser fake = FakeBrowser.Listening(server.Url);
        List<byte> bytes = [];

        await foreach (byte[] chunk in Pdf(fake))
        {
            bytes.AddRange(chunk);
        }

        Assert.Equal("%PDF-fake", System.Text.Encoding.ASCII.GetString([.. bytes]));
    }

    [Fact]
    public async Task DrivesBrowserInOrder()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer();
        using FakeBrowser fake = FakeBrowser.Listening(server.Url);

        _ = await Pdf(fake).ToListAsync();

        Assert.Equal(
            [
                "Target.createTarget",
                "Target.attachToTarget",
                "Page.enable",
                "Fetch.enable",
                "Page.navigate",
                "Runtime.evaluate",
                "Page.printToPDF",
                "IO.read",
                "IO.close",
            ],
            server.Methods.Where(method =>
                !method.StartsWith("Fetch.f", StringComparison.Ordinal)
                && !method.StartsWith("Fetch.c", StringComparison.Ordinal)
            )
        );
    }

    private static async Task<string[]> Answers(FakeDevToolsServer server, int expected)
    {
        for (int wait = 0; wait < 200 && AnswerCount(server) < expected; wait++)
        {
            await Task.Delay(20);
        }

        return
        [
            .. server.Messages.Where(message =>
                message.Contains("Fetch.fulfill")
                || message.Contains("Fetch.fail")
                || message.Contains("Fetch.continue")
            ),
        ];
    }

    private static bool Answered(string[] answers, string id, string method)
    {
        return answers.Count(answer =>
                answer.Contains($"\"requestId\":\"{id}\"") && answer.Contains(method)
            ) == 1;
    }

    [Fact]
    public async Task ServesOwnOriginAndBlocksEverythingElse()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer();
        using FakeBrowser fake = FakeBrowser.Listening(server.Url);

        _ = await Pdf(fake).ToListAsync();
        string[] answers = await Answers(server, 6);

        Assert.Equal(6, answers.Length);
        Assert.Contains(
            answers,
            answer => answer.Contains("R1") && answer.Contains("text/html")
        );
        Assert.Contains(
            answers,
            answer => answer.Contains("R3") && answer.Contains("text/css")
        );
        Assert.True(Answered(answers, "R1", "Fetch.fulfillRequest"));
        Assert.True(Answered(answers, "R2", "Fetch.failRequest"));
        Assert.True(Answered(answers, "R5", "Fetch.failRequest"));
        Assert.DoesNotContain(answers, answer => answer.Contains("R0"));
    }

    [Fact]
    public async Task LetsRemoteImagesThroughUnlessOffline()
    {
        await using FakeDevToolsServer online = new FakeDevToolsServer();
        using FakeBrowser onlineBrowser = FakeBrowser.Listening(online.Url);
        await using FakeDevToolsServer offline = new FakeDevToolsServer();
        using FakeBrowser offlineBrowser = FakeBrowser.Listening(offline.Url);

        _ = await Pdf(onlineBrowser, remoteImages: true).ToListAsync();
        _ = await Pdf(offlineBrowser, remoteImages: false).ToListAsync();

        Assert.True(Answered(await Answers(online, 6), "R4", "Fetch.continueRequest"));
        Assert.True(Answered(await Answers(offline, 6), "R4", "Fetch.failRequest"));
    }

    [Fact]
    public async Task ServesLocalImages()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer();
        using FakeBrowser fake = FakeBrowser.Listening(server.Url);
        using TempDirectory directory = new TempDirectory();
        _ = Directory.CreateDirectory(Path.Combine(directory.Path, "img"));
        await File.WriteAllBytesAsync(
            Path.Combine(directory.Path, "img", "a.png"),
            [1, 2, 3]
        );

        _ = await Pdf(fake, local: new LocalImages(new String(directory.Path)))
            .ToListAsync();
        string[] answers = await Answers(server, 6);

        Assert.Contains(
            answers,
            answer =>
                answer.Contains("R6")
                && answer.Contains("Fetch.fulfillRequest")
                && answer.Contains("image/png")
                && answer.Contains(Convert.ToBase64String([1, 2, 3]))
        );
    }

    [Fact]
    public async Task WarnsAboutImagesThatDidNotLoad()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer();
        using FakeBrowser fake = FakeBrowser.Listening(server.Url);
        StringWriter warnings = new StringWriter();

        _ = await Pdf(fake, warnings: warnings).ToListAsync();

        Assert.Contains("warning: image not loaded: missing.png", warnings.ToString());
        Assert.Contains(
            "warning: image not loaded: https://example.com/photo.png",
            warnings.ToString()
        );
    }

    [Fact]
    public async Task ExplainsOfflineInWarnings()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer();
        using FakeBrowser fake = FakeBrowser.Listening(server.Url);
        StringWriter warnings = new StringWriter();

        _ = await Pdf(fake, remoteImages: false, warnings: warnings).ToListAsync();

        Assert.Contains(
            "warning: image not loaded (--offline): https://example.com/photo.png",
            warnings.ToString()
        );
        Assert.Contains("warning: image not loaded: missing.png", warnings.ToString());
    }

    [Fact]
    public async Task FailsWhenBrowserClosesBeforeLoad()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer(neverLoads: true);
        using FakeBrowser fake = FakeBrowser.Listening(server.Url);

        DevToolsException error = await Assert.ThrowsAsync<DevToolsException>(async () =>
            _ = await Pdf(fake).ToListAsync()
        );

        Assert.NotEmpty(error.Message);
    }

    [Fact]
    public async Task StopsWhenCancelled()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer();
        using FakeBrowser fake = FakeBrowser.Listening(server.Url);
        using CancellationTokenSource cancellation = new CancellationTokenSource();
        await cancellation.CancelAsync();

        Task<List<byte[]>> printing = Pdf(fake).ToListAsync(cancellation.Token).AsTask();

        _ = await Assert.ThrowsAnyAsync<OperationCanceledException>(() => printing);
    }
}
