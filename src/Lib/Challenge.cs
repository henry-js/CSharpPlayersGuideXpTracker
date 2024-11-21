using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace Lib;

public class Challenge
{
    public int Id { get; set; }
    public ChapterId ChapterId { get; set; } = new();
    public string Name { get; set; } = string.Empty;
    public int Xp { get; set; }
    public ChallengeStatus Status { get; set; }
}

public record ChapterId
{
    public int Chapter { get; set; }
    public int Number { get; set; }
}
public enum ChallengeStatus { Pending, Started, Done }

public sealed class ChallengeMap : ClassMap<Challenge>
{
    public ChallengeMap()
    {
        Map(m => m.ChapterId.Chapter).Name("Chapter");
        Map(m => m.ChapterId.Number).Name("Number");
        Map(m => m.Name);
        Map(m => m.Xp).Default(0, true);
        Map(m => m.Status).Default(ChallengeStatus.Pending, true);
    }
}
