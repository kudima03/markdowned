using Markdig.Syntax.Inlines;

namespace Markdowned.Markdown;

/// <summary>Inline TeX: <c>$x$</c>, <c>$`x`$</c> or, as display math, <c>$$x$$</c>.</summary>
public sealed class MathInline : LeafInline
{
    public MathInline(string tex, bool display)
    {
        Tex = tex;
        Display = display;
    }

    public string Tex { get; }

    public bool Display { get; }
}
