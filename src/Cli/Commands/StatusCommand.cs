using Microsoft.Extensions.Logging;
using Spectre.Console;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace CSharpPlayersGuideXpTracker.Cli.Commands;

internal sealed class StatusCommand : Command
{
    public StatusCommand() : base("status", "Print current status")
    {
        AddOptions(this);
    }

    public static void AddOptions(Command command) { }

    new public sealed class Handler(IAnsiConsole console, ILogger<StatusCommand> logger) : ICommandHandler
    {
        public int Invoke(InvocationContext context) => InvokeAsync(context).Result;

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            // Create the layout
            var layout = new Layout("Root")
                        .SplitRows(
                            new Layout("Top").Ratio(5),
                            new Layout("Bottom").Ratio(Console.WindowHeight - 5)
                        );

            // Update the left column
            layout["Top"].Update(
                new Panel(
                    Align.Center(
                        new BreakdownChart()
                            .ShowPercentage()
                            .FullSize()
                            .AddItem("SCSS", 80, Color.Red)
                            .AddItem("HTML", 28.3, Color.Blue)
                            .AddItem("C#", 22.6, Color.Green)
                            .AddItem("JavaScript", 6, Color.Yellow)
                            .AddItem("Ruby", 6, Color.LightGreen),
                        VerticalAlignment.Middle))
                    .Expand());

            // Render the layout
            AnsiConsole.Write(layout);
            Console.ReadLine();
            return 0;
        }
    }
}
