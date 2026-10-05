using Markdig.Extensions.TaskLists;
using Markdig.Renderers;
using Markdig.Renderers.Html;

namespace Markdowned.Markdown.Renderers;

public sealed class GitHubTaskListRenderer : HtmlObjectRenderer<TaskList>
{
    protected override void Write(HtmlRenderer renderer, TaskList obj)
    {
        _ = renderer.Write(
            obj.Checked
                ? "<input type=\"checkbox\" id=\"\" disabled=\"\" class=\"task-list-item-checkbox\" "
                    + "aria-label=\"Completed task\" checked=\"\">"
                : "<input type=\"checkbox\" id=\"\" disabled=\"\" class=\"task-list-item-checkbox\" "
                    + "aria-label=\"Incomplete task\">"
        );
    }
}
