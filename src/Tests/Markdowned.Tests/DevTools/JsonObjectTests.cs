using System.Text.Json;
using Markdowned.DevTools;

namespace Markdowned.Tests.DevTools;

public sealed record JsonObjectTests
{
    [Fact]
    public void WritesEveryValueKind()
    {
        string text = new JsonObject(
            new KeyValuePair<string, object>("s", "a\"b"),
            new KeyValuePair<string, object>("b", true),
            new KeyValuePair<string, object>("i", 3),
            new KeyValuePair<string, object>("d", 1.5),
            new KeyValuePair<string, object>("o", new JsonObject())
        ).TextValue;

        using JsonDocument document = JsonDocument.Parse(text);
        JsonElement root = document.RootElement;

        Assert.Equal("a\"b", root.GetProperty("s").GetString());
        Assert.True(root.GetProperty("b").GetBoolean());
        Assert.Equal(3, root.GetProperty("i").GetInt32());
        Assert.Equal(1.5, root.GetProperty("d").GetDouble());
        Assert.Equal(JsonValueKind.Object, root.GetProperty("o").ValueKind);
    }

    [Fact]
    public void RejectsUnsupportedValue()
    {
        _ = Assert.Throws<ArgumentException>(() =>
            new JsonObject(new KeyValuePair<string, object>("x", new object())).TextValue
        );
    }

    [Fact]
    public void EnumeratesCharacters()
    {
        Assert.Equal(2, new JsonObject().Count());
    }
}
