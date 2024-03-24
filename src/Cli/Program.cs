using Community.Extensions.Spectre.Cli.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using CSharpPlayersGuideXpTracker.Cli.Commands;
using Spectre.Console.Cli;

var builder = Host.CreateApplicationBuilder(args);

// Only use configuration in appsettings.json
builder.Configuration.Sources.Clear();
builder.Configuration.AddJsonFile("appsettings.json", false);

//Disable logging
builder.Logging.ClearProviders();

// Bind configuration section to object
builder.Services.AddOptions<NestedSettings>()
    .Bind(builder.Configuration.GetSection(NestedSettings.Key));

builder.Services.AddCommand<CurrentStatusCommand>("status", cmd =>
{
    cmd.WithDescription("Print current status");
});

//
// The standard call save for the commands will be pre-added & configured
//
builder.UseSpectreConsole<CurrentStatusCommand>(config =>
{
    // All commands above are passed to config.AddCommand() by this point

    config.SetApplicationName("xptracker");
    config.UseBasicExceptionHandler();
});

var app = builder.Build();
await app.RunAsync();

Console.ReadLine();