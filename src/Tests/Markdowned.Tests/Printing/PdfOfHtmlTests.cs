using Markdowned.Abstractions.Printing;
using Markdowned.Browser;
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
            new HtmlDocument(new MarkdownHtml(new String("# Hi"))),
            new PrintParameters()
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
                "Page.getFrameTree",
                "Page.setDocumentContent",
                "Runtime.evaluate",
                "Page.printToPDF",
                "IO.read",
                "IO.close",
            ],
            server.Methods
        );
    }

    [Fact]
    public async Task SendsHtmlToFrame()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer();
        using FakeBrowser fake = FakeBrowser.Listening(server.Url);

        _ = await Pdf(fake).ToListAsync();

        string content = server.Messages.Single(message =>
            message.Contains("setDocumentContent")
        );

        Assert.Contains("\"frameId\":\"F1\"", content);
        Assert.Contains("\\u003Ch1", content);
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

    [Fact]
    public void ProducesA4WithBackground()
    {
        Assert.Contains("\"paperWidth\":8.27", new PrintParameters().TextValue);
        Assert.Contains("\"printBackground\":true", new PrintParameters().TextValue);
        Assert.Equal(
            new PrintParameters().TextValue.Length,
            new PrintParameters().Count()
        );
    }
}
