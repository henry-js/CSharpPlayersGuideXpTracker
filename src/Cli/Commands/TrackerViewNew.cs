using Cli.Display;
using Lib;
using Spectre.Console;

public class TrackerViewNew
{
    public TrackerViewNew(IEnumerable<Challenge> challenges)
    {
        Layout = new Layout("Root")
                    .SplitRows(
                        new Layout("Top").Ratio(5),
                        new Layout("Bottom").Ratio(Console.WindowHeight - 5)
                    );
        Layout["Top"].Update(new BreakdownChart().AddItems(ChapterChallengeItem.ChallengeItems(challenges)));
    }

    public Layout Layout { get; }
}

// var layout = new Layout("Root")
//             .SplitRows(
//                 new Layout("Top").Ratio(5),
//                 new Layout("Bottom").Ratio(Console.WindowHeight - 5)
//             );

// // Update the left column
// layout["Top"].Update(
//     new Panel(
//         Align.Center(
//             new BreakdownChart()
//                 .ShowPercentage()
//                 .FullSize()
//                 .AddItem("SCSS", 80, Color.Red)
//                 .AddItem("HTML", 28.3, Color.Blue)
//                 .AddItem("C#", 22.6, Color.Green)
//                 .AddItem("JavaScript", 6, Color.Yellow)
//                 .AddItem("Ruby", 6, Color.LightGreen),
//             VerticalAlignment.Middle))
//         .Expand());

// // Render the layout
// AnsiConsole.Write(layout);
