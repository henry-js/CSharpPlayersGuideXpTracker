using Spectre.Console;

namespace Cli.Commands;

public enum MenuItemType { Root, SubMenu, Action, Back, Exit }

public interface IMenuItem
{
    string Title { get; }
    bool HasChildren { get; }
    List<IMenuItem> Children { get; set; }
    Action? Action { get; }
    SelectionPrompt<IMenuItem>? Prompt { get; }
    MenuItemType Type { get; set; }
}

public interface IMenuIterator
{
    IMenuItem Current { get; }
    bool CanMoveNext { get; }
    bool CanMovePrevious { get; }
}

public class MenuItem : IMenuItem
{
    public MenuItem(string title, MenuItemType type, IEnumerable<IMenuItem>? children = null)
    {
        Title = title;
        Children = children?.ToList() ?? [];
        Prompt = new SelectionPrompt<IMenuItem>()
            .UseConverter(mi => mi.Title);
        if (children is not null) Prompt.AddChoices(children);
        Type = type;
    }

    public string Title { get; }
    public bool HasChildren => Children.Count != 0;
    public List<IMenuItem> Children { get; set; } = [];
    public Action? Action { get; }
    public SelectionPrompt<IMenuItem>? Prompt { get; }
    public MenuItemType Type { get; set; }

    public MenuItem AddChildren(IEnumerable<IMenuItem> children)
    {
        Children.AddRange(children);
        Prompt?.AddChoices(children);
        return this;
    }
}

public class MenuNavigator : IMenuIterator
{
    private Stack<IMenuItem> _navigationStack = [];
    private readonly IMenuItem _root;
    private IMenuItem _current;

    public MenuNavigator(IMenuItem root)
    {
        root.Type = MenuItemType.Root;
        _root = root;
        _current = root;
    }
    public IMenuItem Current => _current;
    public bool CanMoveNext => _current.HasChildren;
    public bool CanMovePrevious => _navigationStack.Count > 0;

    public bool MoveNext(int index = 0)
    {
        if (!CanMoveNext) return false;

        _navigationStack.Push(_current);
        _current = _current.Children[index];
        return true;
    }

    public bool MovePrevious()
    {
        if (!CanMovePrevious) return false;

        _current = _navigationStack.Pop();
        return true;
    }

    internal IMenuItem Display(IAnsiConsole console)
    {
        var result = console.Prompt(Current.Prompt);

        while (true)
        {
        }
    }
}
