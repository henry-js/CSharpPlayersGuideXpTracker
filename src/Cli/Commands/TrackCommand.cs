using Microsoft.Extensions.Logging;
using Spectre.Console;
using System.CommandLine;
using System.CommandLine.Invocation;

namespace MyProjectNamespace;

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
            while (Interactive)
            {
            }
            return 0;
        }
    }
}
