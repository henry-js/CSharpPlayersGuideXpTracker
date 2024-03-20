using CsvHelper.Configuration;

namespace Lib;

public class Challenge()
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Xp { get; set; }
    public ChallengeStatus Status { get; set; }
    public int Chapter { get; set; }

    public ChallengeRecord ToRecord()
    {
        return new ChallengeRecord(Id, Name, Xp, Status, Chapter);
    }
}
public sealed class ChallengeMap : ClassMap<Challenge>
{
    public ChallengeMap()
    {
        Map(m => m.Id);
        Map(m => m.Name);
        Map(m => m.Xp).Default(0, true);
        Map(m => m.Status).Default(ChallengeStatus.NotStarted, true);
        Map(m => m.Chapter);
    }
}
public record ChallengeRecord(int Id, string Name, int Xp, ChallengeStatus Status, int Chapter)
{
    public Challenge ToChallenge()
    {
        return new Challenge { Id = Id, Chapter = Chapter, Xp = Xp, Status = Status, Name = Name };
    }
}