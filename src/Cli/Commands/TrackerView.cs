using Lib;
using Spectre.Console;

namespace CSharpPlayersGuideXpTracker.Cli.Commands;

public class TrackerView
{
    private const string TopLeft = "TopLeft";
    private const string TopRight = "TopRight";
    private const string BottomLeft = "BottomLeft";
    private const string BottomRight = "BottomRight";
    private readonly Tracker _tracker;

    public TrackerView(Tracker tracker)
    {
        _tracker = tracker;
        Layout.Name = $"Current Level: {_tracker.CurrentLevel}";
        FillLayout();
    }

    public Layout Layout { get; internal set; } = new Layout(
    nameof(TrackerView))
        .SplitRows(
            new Layout("").SplitColumns(new Layout(TopLeft), new Layout(TopRight)),
            new Layout("").SplitColumns(new Layout(BottomLeft), new Layout(BottomRight))
        );

    private void FillLayout()
    {
        var panel = new Panel(new BarChart()
            .Width(100)
            .AddItem("NotStarted", _tracker.GetNotStarted().Count(), Color.Red)
            .AddItem("InProgress", _tracker.GetStarted().Count(), Color.Blue)
            .AddItem("Completed", _tracker.GetCompleted().Count(), Color.Green)
        ).Border(BoxBorder.Heavy).Expand();
        panel.Header = new PanelHeader("Challenges");

        Layout[TopRight].Update(panel);

        var panel2 = new Panel(new BreakdownChart()
            .Width(100)
            .AddItem("Current XP", _tracker.CurrentXp, Color.Green)
            .AddItem("Remaining XP", _tracker.TotalXp - _tracker.CurrentXp, Color.Red)
        ).Border(BoxBorder.Rounded).Expand();
        panel2.Header = new PanelHeader($"[bold magenta]Level:[/] {_tracker.CurrentLevel}");
        Layout[TopLeft].Update(panel2);

        // var panel3 = new Panel(new Calendar(DateTime.Now).AddCalendarEvent(_tracker.LastInProgress));
    }
}