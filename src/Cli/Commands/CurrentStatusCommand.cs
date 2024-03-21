using Lib;
using Spectre.Console;
using Spectre.Console.Cli;
using XpTracker.Lib;

namespace CSharpPlayersGuideXpTracker.Cli.Commands;

public class CurrentStatusCommand(IAnsiConsole console) : AsyncCommand<CurrentStatusCommand.Settings>
{
    private readonly IAnsiConsole console = console;
    Tracker _tracker = new Tracker(new TrackerRepository());

    public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
    {
        var figlet = new FigletText("C# Players Guide XP Tracker");
        // console.Write(figlet);
        console.Write(new BarChart()
            .Width(100)
            .AddItem("NotStarted", _tracker.GetNotStarted().Count(), Color.Red)
            .AddItem("InProgress", _tracker.GetStarted().Count(), Color.Blue)
            .AddItem("Completed", _tracker.GetCompleted().Count(), Color.Green)
        );

        console.Write(new BreakdownChart()
            .Width(100)
            .AddItem("Current XP", _tracker.CurrentXp, Color.Green)
            .AddItem("Remaining XP", _tracker.TotalXp - _tracker.CurrentXp, Color.Red)
        );

        return 0;
    }

    public class Settings : CommandSettings { }
}
