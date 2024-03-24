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
        // var figlet = new FigletText("C# Players Guide XP Tracker");
        // console.Write(figlet);

        var trackerView = new TrackerView(_tracker);

        console.Write(trackerView.Layout);

        return await Task.FromResult(0);
    }

    public class Settings : CommandSettings { }
}
