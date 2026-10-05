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
    private static IPdf Pdf(FakeBrowser fake)
    {
        return new PdfOfHtml(
            new ChromiumLaunch(new String(fake.Executable)),
            new PageHtml(new MarkdownHtml(new String("# Hi"))),
            new EmbeddedResources(),
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
            )
        );
    }

    [Fact]
    public async Task ServesOwnOriginAndBlocksEverythingElse()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer();
        using FakeBrowser fake = FakeBrowser.Listening(server.Url);

        _ = await Pdf(fake).ToListAsync();

        string[] answers =
        [
            .. server.Messages.Where(message =>
                message.Contains("Fetch.fulfill") || message.Contains("Fetch.fail")
            ),
        ];

        Assert.Equal(3, answers.Length);
        _ = Assert.Single(
            answers,
            answer =>
                answer.Contains("\"requestId\":\"R1\"")
                && answer.Contains("Fetch.fulfillRequest")
                && answer.Contains("text/html")
        );
        _ = Assert.Single(
            answers,
            answer =>
                answer.Contains("\"requestId\":\"R2\"")
                && answer.Contains("Fetch.failRequest")
                && answer.Contains("BlockedByClient")
        );
        _ = Assert.Single(
            answers,
            answer =>
                answer.Contains("\"requestId\":\"R3\"") && answer.Contains("text/css")
        );
        Assert.DoesNotContain(answers, answer => answer.Contains("R0"));
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
