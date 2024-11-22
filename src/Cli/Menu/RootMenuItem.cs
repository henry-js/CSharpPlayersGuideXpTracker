using Spectre.Console;

namespace Cli.Menu;

public class RootMenuItem : IMenuItem
{
    public RootMenuItem(string title)
    {
        Title = title;
        Prompt = new SelectionPrompt<IMenuItem>().Title(title)
            .UseConverter(mi => mi.Title);
        Type = MenuItemType.Root;
    }

    public string Title { get; }
    public bool HasChildren => Children.Count != 0;
    public List<IMenuItem> Children { get; set; } = [];
    public Action? Action { get; }
    public IPrompt<IMenuItem> Prompt { get; private set; }
    public MenuItemType Type { get; set; }

    public RootMenuItem AddChildren(IEnumerable<IMenuItem> children)
    {
        Children.AddRange(children);
        var prompt = new SelectionPrompt<IMenuItem>()
        .AddChoices(Children);
        int index = Children.Count;
        prompt.AddChoice(new ExitMenuItem(index++));

        Prompt = prompt;
        return this;
    }
}
