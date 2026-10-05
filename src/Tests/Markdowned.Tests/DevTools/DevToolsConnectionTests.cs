using System.Text.Json;
using Markdowned.Abstractions.DevTools;
using Markdowned.DevTools;
using Markdowned.Tests.Fakes;
using String = Pure.Primitives.String.String;

namespace Markdowned.Tests.DevTools;

public sealed record DevToolsConnectionTests
{
    [Fact]
    public async Task ReturnsResultOfCommand()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer();
        await using IDevToolsSession session = await new DevToolsConnect(
            new String(server.Url)
        ).Session;

        JsonElement result = await session.Send(
            new BrowserCommand("Target.createTarget", new JsonObject())
        );

        Assert.Equal("T1", result.GetProperty("targetId").GetString());
    }

    [Fact]
    public async Task SendsSessionIdOfPageCommand()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer();
        await using IDevToolsSession session = await new DevToolsConnect(
            new String(server.Url)
        ).Session;

        _ = await session.Send(new PageCommand("S9", "Page.enable", new JsonObject()));

        Assert.Equal(
            /*lang=json,strict*/
            """{"id":1,"method":"Page.enable","params":{},"sessionId":"S9"}""",
            Assert.Single(server.Messages)
        );
    }

    [Fact]
    public async Task OmitsSessionIdOfBrowserCommand()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer();
        await using IDevToolsSession session = await new DevToolsConnect(
            new String(server.Url)
        ).Session;

        _ = await session.Send(
            new BrowserCommand("Browser.getVersion", new JsonObject())
        );

        Assert.DoesNotContain("sessionId", Assert.Single(server.Messages));
    }

    [Fact]
    public async Task FailsOnProtocolError()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer("Page.enable");
        await using IDevToolsSession session = await new DevToolsConnect(
            new String(server.Url)
        ).Session;

        DevToolsException error = await Assert.ThrowsAsync<DevToolsException>(() =>
            session.Send(new BrowserCommand("Page.enable", new JsonObject()))
        );

        Assert.Equal("boom", error.Message);
    }

    [Fact]
    public async Task FailsWhenBrowserHangsUp()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer();
        await using IDevToolsSession session = await new DevToolsConnect(
            new String(server.Url)
        ).Session;

        _ = await Assert.ThrowsAsync<DevToolsException>(() =>
            session.Send(new BrowserCommand("Fake.hangUp", new JsonObject()))
        );
    }

    [Fact]
    public async Task NumbersCommandsSequentially()
    {
        await using FakeDevToolsServer server = new FakeDevToolsServer();
        await using IDevToolsSession session = await new DevToolsConnect(
            new String(server.Url)
        ).Session;

        _ = await session.Send(new BrowserCommand("A.a", new JsonObject()));
        _ = await session.Send(new BrowserCommand("B.b", new JsonObject()));

        Assert.Equal(["A.a", "B.b"], server.Methods);
        Assert.All(server.Messages, message => Assert.Contains("\"id\":", message));
    }
}
