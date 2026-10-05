using System.Text.Json;
using Markdowned.Printing;
using String = Pure.Primitives.String.String;

namespace Markdowned.Tests.Printing;

public sealed record PrintParametersTests
{
    private static JsonElement Parse(string paper, bool landscape, double margin)
    {
        using JsonDocument document = JsonDocument.Parse(
            new PrintParameters(new String(paper), landscape, margin).TextValue
        );

        return document.RootElement.Clone();
    }

    [Fact]
    public void PrintsA4PortraitWithBackground()
    {
        JsonElement parameters = Parse("A4", false, 15);

        Assert.Equal(210 / 25.4, parameters.GetProperty("paperWidth").GetDouble(), 6);
        Assert.Equal(297 / 25.4, parameters.GetProperty("paperHeight").GetDouble(), 6);
        Assert.False(parameters.GetProperty("landscape").GetBoolean());
        Assert.True(parameters.GetProperty("printBackground").GetBoolean());
    }

    [Theory]
    [InlineData("Letter", 8.5, 11)]
    [InlineData("legal", 8.5, 14)]
    public void KnowsUsPapers(string paper, double width, double height)
    {
        JsonElement parameters = Parse(paper, false, 15);

        Assert.Equal(width, parameters.GetProperty("paperWidth").GetDouble());
        Assert.Equal(height, parameters.GetProperty("paperHeight").GetDouble());
    }

    [Fact]
    public void RejectsUnknownPaper()
    {
        ArgumentException error = Assert.Throws<ArgumentException>(() =>
            Parse("B5", false, 15)
        );

        Assert.Contains("A4, Letter or Legal", error.Message);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void RejectsMarginsOutOfRange(double margin)
    {
        _ = Assert.Throws<ArgumentException>(() => Parse("A4", false, margin));
    }

    [Fact]
    public void FitsGitHubWidthToPrintableWidth()
    {
        double printable = ((210 / 25.4) - (2 * 15 / 25.4)) * 96;

        Assert.Equal(
            printable / 838,
            Parse("A4", false, 15).GetProperty("scale").GetDouble(),
            6
        );
    }

    [Fact]
    public void UsesPaperHeightAsWidthInLandscape()
    {
        double portrait = Parse("A4", false, 15).GetProperty("scale").GetDouble();
        double landscape = Parse("A4", true, 15).GetProperty("scale").GetDouble();

        Assert.True(Parse("A4", true, 15).GetProperty("landscape").GetBoolean());
        Assert.True(landscape > portrait);
    }

    [Fact]
    public void ClampsScale()
    {
        Assert.Equal(
            14 * 96 / 838.0,
            Parse("Legal", true, 0).GetProperty("scale").GetDouble(),
            3
        );
        Assert.Equal(0.1, Parse("A4", false, 100).GetProperty("scale").GetDouble());
    }

    [Fact]
    public void AddsFooterOutlineAndTags()
    {
        JsonElement parameters = Parse("A4", false, 15);

        Assert.True(parameters.GetProperty("displayHeaderFooter").GetBoolean());
        Assert.Contains(
            "pageNumber",
            parameters.GetProperty("footerTemplate").GetString()
        );
        Assert.Contains(
            "totalPages",
            parameters.GetProperty("footerTemplate").GetString()
        );
        Assert.True(parameters.GetProperty("generateDocumentOutline").GetBoolean());
        Assert.True(parameters.GetProperty("generateTaggedPDF").GetBoolean());
        Assert.Equal(
            "ReturnAsStream",
            parameters.GetProperty("transferMode").GetString()
        );
    }

    [Fact]
    public void EnumeratesCharacters()
    {
        PrintParameters parameters = new PrintParameters(new String("A4"), false, 15);

        Assert.Equal(parameters.TextValue.Length, parameters.Count());
    }
}
