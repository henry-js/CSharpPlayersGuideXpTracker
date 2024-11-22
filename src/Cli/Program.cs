using System.CommandLine;
using System.CommandLine.Builder;
using System.CommandLine.Hosting;
using System.CommandLine.Parsing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Spectre.Console;
using CSharpPlayersGuideXpTracker.Cli.Commands;
using CSharpPlayersGuideXpTracker.Cli.Extensions;
using XpTracker.Lib;
using Lib;
using LiteDB;

var loggerConfiguration = new LoggerConfiguration()
    .MinimumLevel.Debug()
    // .WriteTo.File("logs/startup_.log",
    // outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u}] {SourceContext}: {Message:lj}{NewLine}{Exception}",
    // rollingInterval: RollingInterval.Day
    // )
    // .Enrich.WithProperty("Application Name", "<APP NAME>");
    .WriteTo.Console();
Log.Logger = loggerConfiguration.CreateBootstrapLogger();

var rootCommand = new RootCommand("root");
rootCommand.AddCommand(new StatusCommand());
rootCommand.AddCommand(new TrackCommand());

var cmdLine = new CommandLineBuilder(rootCommand)
    .UseHost(_ => Host.CreateDefaultBuilder(args), builder =>
    {
        builder.ConfigureAppConfiguration(config =>
        {
        })
            .ConfigureServices((context, services) =>
            {
                services.AddSingleton(_ => AnsiConsole.Console);
                // services.AddSingleton<ITrackerRepository, TrackerRepository>();
                services.AddSingleton((s) =>
                {
                    var dll = new FileInfo(typeof(Program).Assembly.Location);
                    var fileName = Path.Combine(dll.DirectoryName, "challenges.db");
                    return new LiteDatabase(@$"Filename={fileName};Connection=direct");
                });
                services.AddSingleton((sp) => new LiteRepository(sp.GetRequiredService<LiteDatabase>()));
                services.AddSingleton<ITrackerRepository, LiteTrackerRepository>();
            })
            .UseProjectCommandHandlers()
            .UseSerilog((context, services, configuration) =>
                configuration.ReadFrom.Configuration(context.Configuration));
    })
    .UseDefaults()
    .Build();

int result = await cmdLine.InvokeAsync(args);

return result;
