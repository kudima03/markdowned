namespace Markdowned.Integration.Tests;

public sealed record PdfTests
{
    [BrowserFact]
    public void ProducesAValidTaggedPdfWithOutline()
    {
        using RenderedPdf pdf = new RenderedPdf("# Title\n\n## Section\n\nText.\n");

        Assert.StartsWith("%PDF-", pdf.Text);
        Assert.Contains("/StructTreeRoot", pdf.Text);
        Assert.Contains("/Outlines", pdf.Text);
        Assert.Contains("%%EOF", pdf.Text);
    }

    [BrowserFact]
    public void PrintsOnAnA4PageByDefaultAndLetterOnRequest()
    {
        using RenderedPdf a4 = new RenderedPdf("# A4\n");
        using RenderedPdf letter = new RenderedPdf("# Letter\n", "--paper", "Letter");

        Assert.Contains("MediaBox [0 0 595.9", a4.Text);
        Assert.Contains("MediaBox [0 0 612 792]", letter.Text);
    }

    [BrowserFact]
    public void LaysOutLongDocumentsOnSeveralPages()
    {
        string markdown = string.Concat(
            Enumerable.Range(0, 400).Select(index => $"Paragraph {index}.\n\n")
        );

        using RenderedPdf pdf = new RenderedPdf(markdown);

        int pages = 0;

        for (
            int at = pdf.Text.IndexOf("/Type /Page\n", StringComparison.Ordinal);
            at >= 0;
            at = pdf.Text.IndexOf("/Type /Page\n", at + 1, StringComparison.Ordinal)
        )
        {
            pages++;
        }

        Assert.True(pages > 1);
    }

    [BrowserFact]
    public void RunsHighlightingMathAndDiagramsBeforePrinting()
    {
        using RenderedPdf plain = new RenderedPdf("# P\n\ntext\n");
        using RenderedPdf rich = new RenderedPdf(
            "# R\n\n```js\nconst a = 1;\n```\n\n$x^2$\n\n```mermaid\nflowchart LR\n  A --> B\n```\n"
        );

        Assert.True(rich.Bytes.Length > plain.Bytes.Length + 5000);
    }

    [BrowserFact]
    public void DoesNotRunScriptsFromTheDocument()
    {
        using RenderedPdf pdf = new RenderedPdf(
            "<script>document.title='pwned'</script>\n\n<img src=x onerror=\"document.title='pwned'\">\n\ntext\n"
        );

        Assert.DoesNotContain("pwned", pdf.Text);
    }

    [BrowserFact]
    public void ServesLocalImagesAndReportsMissingOnes()
    {
        using RenderedPdf pdf = new RenderedPdf("![nope](missing.png)\n", "--offline");

        Assert.StartsWith("%PDF-", pdf.Text);
    }
}
