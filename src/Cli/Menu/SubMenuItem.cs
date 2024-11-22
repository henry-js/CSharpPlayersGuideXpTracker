using Spectre.Console;

namespace Cli.Menu;

public class SubMenuItem : IMenuItem
{
    public SubMenuItem(string title, IEnumerable<IMenuItem> children)
    {
        Title = title;
        Children = children.ToList();
        int count = Children.Count;
        Children.Add(new BackMenuItem(count++));
        Children.Add(new ExitMenuItem(count++));
        Prompt = new SelectionPrompt<IMenuItem>()
            .AddChoices(Children)
            .Title(title)
            .UseConverter(mi => mi.Title);
    }
    public string Title { get; }
    public bool HasChildren => Children.Count != 0;
    public List<IMenuItem> Children { get; set; } = [];
    public Action? Action { get; } = null;
    public SelectionPrompt<IMenuItem>? Prompt { get; }
    public MenuItemType Type { get; } = MenuItemType.SubMenu;
}
