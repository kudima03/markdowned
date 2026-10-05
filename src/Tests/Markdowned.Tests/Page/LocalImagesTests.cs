using Markdowned.Abstractions.Page;
using Markdowned.Page;
using Markdowned.Tests.Fakes;
using String = Pure.Primitives.String.String;

namespace Markdowned.Tests.Page;

public sealed record LocalImagesTests
{
    private static IPageResource? Find(string directory, string path)
    {
        return new LocalImages(new String(directory))[new String(path)];
    }

    [Fact]
    public void ServesImagesBelowTheDirectory()
    {
        using TempDirectory directory = new TempDirectory();
        _ = Directory.CreateDirectory(Path.Combine(directory.Path, "img"));
        File.WriteAllBytes(Path.Combine(directory.Path, "img", "a b.PNG"), [1, 2]);

        IPageResource? resource = Find(directory.Path, "img/a%20b.PNG");

        Assert.NotNull(resource);
        Assert.Equal("image/png", resource.ContentType.TextValue);
        Assert.Equal([1, 2], resource.Content);
        Assert.Equal("img/a%20b.PNG", resource.Path.TextValue);
    }

    [Theory]
    [InlineData("a.svg", "image/svg+xml")]
    [InlineData("a.jpg", "image/jpeg")]
    [InlineData("a.gif", "image/gif")]
    [InlineData("a.webp", "image/webp")]
    public void KnowsImageTypes(string name, string type)
    {
        using TempDirectory directory = new TempDirectory();
        File.WriteAllBytes(Path.Combine(directory.Path, name), [1]);

        Assert.Equal(type, Find(directory.Path, name)!.ContentType.TextValue);
    }

    [Theory]
    [InlineData("notes.txt")]
    [InlineData("secret.pem")]
    [InlineData("script.js")]
    [InlineData("page.html")]
    [InlineData("noextension")]
    public void RefusesFilesThatAreNotImages(string name)
    {
        using TempDirectory directory = new TempDirectory();
        File.WriteAllText(Path.Combine(directory.Path, name), "x");

        Assert.Null(Find(directory.Path, name));
    }

    [Theory]
    [InlineData("../outside.png")]
    [InlineData("..%2Foutside.png")]
    [InlineData("sub/../../outside.png")]
    [InlineData("/../outside.png")]
    public void RefusesPathsOutsideTheDirectory(string path)
    {
        using TempDirectory parent = new TempDirectory();
        string inside = Path.Combine(parent.Path, "inside");
        _ = Directory.CreateDirectory(Path.Combine(inside, "sub"));
        File.WriteAllBytes(Path.Combine(parent.Path, "outside.png"), [1]);

        Assert.Null(Find(inside, path));
    }

    [Fact]
    public void RefusesSiblingDirectoriesWithTheSamePrefix()
    {
        using TempDirectory parent = new TempDirectory();
        string inside = Path.Combine(parent.Path, "docs");
        _ = Directory.CreateDirectory(inside);
        _ = Directory.CreateDirectory(Path.Combine(parent.Path, "docs-private"));
        File.WriteAllBytes(Path.Combine(parent.Path, "docs-private", "a.png"), [1]);

        Assert.Null(Find(inside, "../docs-private/a.png"));
    }

    [Fact]
    public void RefusesSymbolicLinksLeavingTheDirectory()
    {
        if (OperatingSystem.IsWindows())
        {
            return;
        }

        using TempDirectory parent = new TempDirectory();
        string inside = Path.Combine(parent.Path, "inside");
        _ = Directory.CreateDirectory(inside);
        File.WriteAllBytes(Path.Combine(parent.Path, "outside.png"), [1]);
        _ = File.CreateSymbolicLink(
            Path.Combine(inside, "link.png"),
            Path.Combine(parent.Path, "outside.png")
        );
        File.WriteAllBytes(Path.Combine(inside, "real.png"), [2]);
        _ = File.CreateSymbolicLink(
            Path.Combine(inside, "ok.png"),
            Path.Combine(inside, "real.png")
        );

        Assert.Null(Find(inside, "link.png"));
        Assert.NotNull(Find(inside, "ok.png"));
    }

    [Fact]
    public void FindsNothingForMissingFilesAndTheRoot()
    {
        using TempDirectory directory = new TempDirectory();

        Assert.Null(Find(directory.Path, "missing.png"));
        Assert.Null(Find(directory.Path, string.Empty));
        Assert.Null(Find(directory.Path, "/"));
    }

    [Fact]
    public void LooksThroughSeveralSources()
    {
        using TempDirectory directory = new TempDirectory();
        File.WriteAllBytes(Path.Combine(directory.Path, "a.png"), [1]);
        IResourceLookup lookup = new FirstFound(
            new ListedResources(new EmbeddedResources()),
            new LocalImages(new String(directory.Path))
        );

        Assert.NotNull(lookup[new String("assets/page.css")]);
        Assert.NotNull(lookup[new String("a.png")]);
        Assert.Null(lookup[new String("b.png")]);
    }
}
