using Markdig.Extensions.TaskLists;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;

namespace Markdowned.Markdown.Renderers;

public sealed class GitHubListRenderer : HtmlObjectRenderer<ListBlock>
{
    private static bool IsTask(ListItemBlock item)
    {
        return item.FirstOrDefault() is ParagraphBlock { Inline.FirstChild: TaskList };
    }

    protected override void Write(HtmlRenderer renderer, ListBlock obj)
    {
        bool tasks = obj.OfType<ListItemBlock>().Any(IsTask);
        string tag = obj.IsOrdered ? "ol" : "ul";

        _ = renderer.EnsureLine();
        _ = renderer.Write('<').Write(tag);

        _ = tasks
            ? renderer.Write(" class=\"contains-task-list\"")
            : renderer.Write(" dir=\"auto\"");

        if (obj.IsOrdered && obj.OrderedStart is { } start && start != "1")
        {
            _ = renderer.Write(" start=\"").Write(start).Write('"');
        }

        _ = renderer.WriteLine(">");
        bool previous = renderer.ImplicitParagraph;

        foreach (ListItemBlock item in obj.OfType<ListItemBlock>())
        {
            _ = renderer.EnsureLine();
            _ = renderer.Write(IsTask(item) ? "<li class=\"task-list-item\">" : "<li>");
            renderer.ImplicitParagraph = !obj.IsLoose;
            renderer.WriteChildren(item);
            renderer.ImplicitParagraph = previous;
            _ = renderer.WriteLine("</li>");
        }

        _ = renderer.Write("</").Write(tag).WriteLine(">");
    }
}
