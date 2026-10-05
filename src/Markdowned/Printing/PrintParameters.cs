using Markdowned.DevTools;
using Pure.Primitives.Abstractions.Char;
using Pure.Primitives.Abstractions.String;
using Char = Pure.Primitives.Char.Char;

namespace Markdowned.Printing;

/// <summary>
/// Parameters of <c>Page.printToPDF</c>. The page is laid out at GitHub's README width and
/// scaled so that this width fills the printable area of the paper.
/// </summary>
public sealed record PrintParameters : IString
{
    public const double ContentWidthPixels = 838;

    private const double MillimetresPerInch = 25.4;

    private const double PixelsPerInch = 96;

    private const string FooterTemplate =
        "<div style=\"width:100%;text-align:center;font-size:9px;color:#59636e;"
        + "font-family:'Noto Sans',sans-serif\"><span class=\"pageNumber\"></span> / "
        + "<span class=\"totalPages\"></span></div>";

    private readonly IString _paper;

    private readonly bool _landscape;

    public PrintParameters(IString paper, bool landscape, double marginMillimetres)
    {
        _paper = paper;
        _landscape = landscape;
        Margin = marginMillimetres;
    }

    private (double Width, double Height) Paper =>
        _paper.TextValue.ToUpperInvariant() switch
        {
            "A4" => (210 / MillimetresPerInch, 297 / MillimetresPerInch),
            "LETTER" => (8.5, 11),
            "LEGAL" => (8.5, 14),
            _ => throw new ArgumentException(
                $"Unknown paper '{_paper.TextValue}'. Use A4, Letter or Legal."
            ),
        };

    private double Margin =>
        field is >= 0 and <= 100
            ? field / MillimetresPerInch
            : throw new ArgumentException(
                "--margin must be between 0 and 100 millimetres."
            );

    private double Scale =>
        Math.Clamp(
            ((_landscape ? Paper.Height : Paper.Width) - (2 * Margin))
                * PixelsPerInch
                / ContentWidthPixels,
            0.1,
            2
        );

    public string TextValue =>
        new JsonObject(
            new KeyValuePair<string, object>("landscape", _landscape),
            new KeyValuePair<string, object>("paperWidth", Paper.Width),
            new KeyValuePair<string, object>("paperHeight", Paper.Height),
            new KeyValuePair<string, object>("marginTop", Margin),
            new KeyValuePair<string, object>("marginBottom", Margin),
            new KeyValuePair<string, object>("marginLeft", Margin),
            new KeyValuePair<string, object>("marginRight", Margin),
            new KeyValuePair<string, object>("scale", Scale),
            new KeyValuePair<string, object>("printBackground", true),
            new KeyValuePair<string, object>("displayHeaderFooter", true),
            new KeyValuePair<string, object>("headerTemplate", "<span></span>"),
            new KeyValuePair<string, object>("footerTemplate", FooterTemplate),
            new KeyValuePair<string, object>("generateDocumentOutline", true),
            new KeyValuePair<string, object>("generateTaggedPDF", true),
            new KeyValuePair<string, object>("transferMode", "ReturnAsStream")
        ).TextValue;

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
