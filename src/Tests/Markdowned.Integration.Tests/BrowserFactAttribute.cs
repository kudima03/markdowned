namespace Markdowned.Integration.Tests;

/// <summary>A test that needs a real Chromium: it runs when MARKDOWNED_BROWSER names one.</summary>
public sealed class BrowserFactAttribute : FactAttribute
{
    public BrowserFactAttribute()
    {
        if (
            Environment.GetEnvironmentVariable("MARKDOWNED_BROWSER")
            is not { Length: > 0 }
        )
        {
            Skip = "Set MARKDOWNED_BROWSER to a Chromium-based browser to run this test.";
        }
    }
}
