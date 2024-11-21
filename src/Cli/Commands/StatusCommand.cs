using Microsoft.Extensions.Logging;
using Spectre.Console;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Text.Json;
using XpTracker.Lib;

namespace CSharpPlayersGuideXpTracker.Cli.Commands;

internal sealed class StatusCommand : Command
{
    public StatusCommand() : base("status", "Print current status")
    {
        AddOptions(this);
    }

    public static void AddOptions(Command command) { }

    new public sealed class Handler(IAnsiConsole console, ILogger<StatusCommand> logger, ITrackerRepository repository) : ICommandHandler
    {
        public int Invoke(InvocationContext context) => InvokeAsync(context).Result;

        public async Task<int> InvokeAsync(InvocationContext context)
        {
            // Create the layout
            var challenges = await repository.GetChallenges();
            foreach (var item in challenges)
            {
                Console.WriteLine(JsonSerializer.Serialize(item));
            }
            Console.ReadLine();
            return 0;
        }
    }
}
