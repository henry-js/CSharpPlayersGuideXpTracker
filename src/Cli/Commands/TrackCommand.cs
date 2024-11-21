using Cli.Commands;
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
            var menu = new MenuNavigator(new MenuItem("Root", MenuItemType.Root).AddChildren(
                [
                    new MenuItem("1. Sample Item", MenuItemType.SubMenu).AddChildren(
                        [
                            new MenuItem("a. SubMenu Item", MenuItemType.SubMenu),
                            new MenuItem("b. SubMenu Item", MenuItemType.SubMenu),
                            new MenuItem("c. SubMenu Item", MenuItemType.SubMenu),
                        ]
                    ),
                    new MenuItem("2. Sample Item", MenuItemType.Action),
                    new MenuItem("3. Sample Item", MenuItemType.SubMenu),
                    new MenuItem("4. Sample Item", MenuItemType.SubMenu),
                ]
            ));

            while (true)
            {
                DisplayCurrentMenu(menu);
                switch (menu.Current.Type)
                {
                }
            }
            var selected = menu.Display(console);
            return 0;
        }

        private void DisplayCurrentMenu(MenuNavigator menu)
        {
            throw new NotImplementedException();
        }
    }
}
