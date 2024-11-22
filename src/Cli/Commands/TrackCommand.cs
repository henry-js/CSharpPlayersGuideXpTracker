using Cli.Menu;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace CSharpPlayersGuideXpTracker.Cli.Commands;

internal sealed class TrackCommand : Command
{
    public TrackCommand() : base("track", "Manage challenges")
    {
        AddOptions(this);
    }

    public static void AddOptions(Command command)
    {
        var interactive = new Option<bool>(["-i", "--interactive"]);
    }

    new public class Handler(IAnsiConsole console, ILogger<TrackCommand> logger) : ICommandHandler
    {
        public bool Interactive { get; set; }
        public int Invoke(InvocationContext context) => InvokeAsync(context).Result;

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            var menu = new MenuNavigator(new RootMenuItem("Root").AddChildren(
                [
                    new SubMenuItem("1. Sample Item",
                        [
                            new SubMenuItem("a. SubMenu Item", []),
                            new SubMenuItem("b. SubMenu Item", []),
                            new SubMenuItem("c. SubMenu Item", []),
                        ]
                    ),
                    new SubMenuItem("2. Sample Item", []),
                    new SubMenuItem("3. Sample Item", []),
                    new SubMenuItem("4. Sample Item", []),
                ]
            ));
            bool exit = false;
            while (!exit)
            {
                switch (menu.Current.Type)
                {
                    case MenuItemType.Root:
                    case MenuItemType.SubMenu:
                        menu.Display(console);
                        continue;
                    case MenuItemType.Back:
                        if (menu.CanMovePrevious) menu.MovePrevious();
                        continue;
                    case MenuItemType.Exit:
                        exit = true;
                        continue;
                    case MenuItemType.Action:
                        console.MarkupLine("[blue]ACTION HIT[/]");
                        continue;
                }
            }

            return await Task.FromResult(0);
        }
    }
}
