using Lib;
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
    public IPrompt<IMenuItem>? Prompt { get; }
    public MenuItemType Type { get; } = MenuItemType.SubMenu;
}

public class ChallengeSubMenuItem : IMenuItem
{
    public ChallengeSubMenuItem(string title, IEnumerable<Challenge> challenges)
    {
        Prompt = new MultiSelectionPrompt<Challenge>()
            .Title(title)
            .Required()
            .AddChoices(challenges);
    }
    public string Title { get; }
    public bool HasChildren { get; }
    public List<IMenuItem> Children { get; set; }
    public Action? Action { get; }
    public IPrompt<IMenuItem>? Prompt { get; }
    public MenuItemType Type { get; }
}
