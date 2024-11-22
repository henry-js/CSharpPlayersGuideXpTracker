using Spectre.Console;

namespace Cli.Menu;

public abstract class ActionMenuItem : IMenuItem
{
    public ActionMenuItem(int index, string actionName)
    {
        Title = $"{index}. {actionName}";
    }
    public string Title { get; }
    public bool HasChildren { get; } = false;
    public List<IMenuItem> Children { get; set; } = [];
    public Action? Action { get; protected set; }
    public IPrompt<IMenuItem>? Prompt { get; }
    public virtual MenuItemType Type { get; } = MenuItemType.Action;
}
