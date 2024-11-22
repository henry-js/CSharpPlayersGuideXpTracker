using Spectre.Console;

namespace Cli.Menu;

public class MenuNavigator : IMenuIterator
{
    private readonly Stack<IMenuItem> _navigationStack = [];
    private readonly IMenuItem _root;
    private IMenuItem _current;

    public MenuNavigator(IMenuItem root)
    {
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

    internal void Display(IAnsiConsole console)
    {
        var result = console.Prompt(Current.Prompt);
        if (result.HasChildren)
            MoveNext(Current.Children.IndexOf(result));
        else _current = result;
    }
}
