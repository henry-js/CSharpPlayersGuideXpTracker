using Cli.Menu;
using Lib;
using LiteDB;
using LiteDB;
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

    new public class Handler(IAnsiConsole console, LiteRepository repo, ILogger<TrackCommand> logger) : ICommandHandler
    {
        public bool Interactive { get; set; }
        public int Invoke(InvocationContext context) => InvokeAsync(context).Result;

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            var challenges = repo.Query<Challenge>().ToList().GroupBy(c => c.ChapterId.Chapter);
            var menu = new MenuNavigator(new RootMenuItem("Chapters").AddChildren(
                challenges.Select(chapter =>
                    new ChallengeSubMenuItem($"Chapter {chapter.Key}", chapter.Select(c => c)))
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
                        menu.Current.Action?.Invoke();
                        continue;
                }
            }

            return await Task.FromResult(0);
        }
    }
}

internal class ChallengeMenuItem : ActionMenuItem
{
    public ChallengeMenuItem(Challenge challenge) : base(challenge.ChapterId.Number, challenge.Name)
    {
        Action = () =>
        {
            string statusColor = challenge.Status == ChallengeStatus.Pending ? "red" : challenge.Status == ChallengeStatus.Started ? "yellow" : "red";
            AnsiConsole.MarkupLineInterpolated($"[purple]Challenge: {challenge.Name}[/], Worth: [green]{challenge.Xp}xp[/], Status: [{statusColor}]{challenge.Status}[/]");
        };
    }

}
