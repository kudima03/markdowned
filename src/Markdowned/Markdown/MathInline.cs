using Markdig.Syntax.Inlines;

namespace Markdowned.Markdown;

/// <summary>Inline TeX: <c>$x$</c>, <c>$`x`$</c> or, as display math, <c>$$x$$</c>.</summary>
public sealed class MathInline(string tex, bool display) : LeafInline
{
    public string Tex { get; } = tex;

    public bool Display { get; } = display;
}
