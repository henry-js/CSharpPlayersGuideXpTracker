using Spectre.Console;

namespace Cli.Menu;

public interface IMenuItem
{
    string Title { get; }
    bool HasChildren { get; }
    List<IMenuItem> Children { get; set; }
    Action? Action { get; }
    SelectionPrompt<IMenuItem>? Prompt { get; }
    MenuItemType Type { get; }
}
