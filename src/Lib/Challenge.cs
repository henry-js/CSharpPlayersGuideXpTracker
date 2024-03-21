using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

namespace Lib;

public class Challenge
{
    public ChapterId ChapterId { get; set; } = new();
    public string Name { get; set; } = string.Empty;
    public int Xp { get; set; }
    public ChallengeStatus Status { get; set; }
}

public record ChapterId
{
    public int Chapter { get; set; }
    public int Id { get; set; }
}
public enum ChallengeStatus { NotStarted, InProgress, Completed }

public sealed class ChallengeMap : ClassMap<Challenge>
{
    public ChallengeMap()
    {
        Map(m => m.ChapterId.Chapter).Name("Chapter");
        Map(m => m.ChapterId.Id).Name("Id");
        Map(m => m.Name);
        Map(m => m.Xp).Default(0, true);
        Map(m => m.Status).Default(ChallengeStatus.NotStarted, true);
    }
}