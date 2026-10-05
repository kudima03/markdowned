using Markdowned.Abstractions.Markdown;
using Pure.Primitives.Abstractions.Char;
using Char = Pure.Primitives.Char.Char;

namespace Markdowned.Page;

public sealed record PageHtml : IHtml
{
    private readonly IHtml _body;

    public PageHtml(IHtml body)
    {
        _body = body;
    }

    public string TextValue =>
        "<!doctype html><html lang=\"en\" data-color-mode=\"light\" data-light-theme=\"light\">"
        + "<head><meta charset=\"utf-8\"><title>markdowned</title>"
        + "<link rel=\"stylesheet\" href=\"/assets/fonts.css\">"
        + "<link rel=\"stylesheet\" href=\"/assets/github-markdown-light.css\">"
        + "<link rel=\"stylesheet\" href=\"/assets/page.css\">"
        + "<script>window.markdownedReady = new Promise(resolve => "
        + "window.addEventListener('load', async () => {"
        + "do { void document.body.offsetHeight; await document.fonts.ready; } "
        + "while ([...document.fonts].some(font => font.status === 'loading'));"
        + "resolve(); }));</script>"
        + "</head><body><article class=\"markdown-body entry-content\">"
        + _body.TextValue
        + "</article></body></html>";

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
