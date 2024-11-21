using Lib;
using Spectre.Console;

namespace Cli.Display;

public class ChapterChallengeItem : IBreakdownChartItem
{
    public required string Label { get; init; }
    public double Value { get; init; }
    public Color Color { get; init; }

    public static List<ChapterChallengeItem> ChallengeItems(IEnumerable<Challenge> challenges)
    {
        return
        [
            new ()
        {
            Label = nameof(ChallengeStatus.Pending),
            Value = challenges.Count(x => x.Status == ChallengeStatus.Pending),
            Color = Color.Red
        },
        new()
        {
            Label = nameof(ChallengeStatus.Started),
            Value = challenges.Count(x => x.Status == ChallengeStatus.Pending),
            Color = Color.Orange1,
        },
        new()
        {
            Label = nameof(ChallengeStatus.Done),
            Value = challenges.Count(x => x.Status == ChallengeStatus.Pending),
            Color = Color.Green,
        },
    ];
    }
    public static List<ChapterChallengeItem> XpItems(IEnumerable<Challenge> challenges)
    {
        return
        [
            new ()
        {
            Label = nameof(ChallengeStatus.Pending) + " xp",
            Value = challenges.Where(x => x.Status == ChallengeStatus.Pending).Sum(x => x.Xp),
            Color = Color.Red
        },
        new()
        {
            Label = nameof(ChallengeStatus.Started) + " xp",
            Value = challenges.Where(x => x.Status == ChallengeStatus.Pending).Sum(x => x.Xp),
            Color = Color.Orange1,
        },
        new()
        {
            Label = nameof(ChallengeStatus.Done) + " xp",
            Value = challenges.Where(x => x.Status == ChallengeStatus.Pending).Sum(x => x.Xp),
            Color = Color.Green,
        },
    ];
    }
}
