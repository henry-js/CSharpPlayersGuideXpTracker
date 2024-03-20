using System.Collections;
using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Globalization;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using XpTracker.Lib;

namespace Lib;

public class Tracker
{
    private readonly Dictionary<int, ChallengeRecord> _challenges = [];

    private readonly ITrackerRepository _repo;

    public Tracker(ITrackerRepository repository)
    {
        _repo = repository;
        _challenges = _repo.GetChallenges().ToDictionary(x => x.Id);

    }

    public int CompletedCount =>
        _challenges.Values.Count(x => x.Status == ChallengeStatus.Completed);

    public int TotalXp =>
        _challenges.Values.Select(c => c.Xp)
        .Sum();

    public int CurrentXp =>
        _challenges.Values.Where(c => c.Status == ChallengeStatus.Completed)
        .Sum(x => x.Xp);


    public List<ChallengeRecord> GetChapter(int chapter = 1)
    {
        if (chapter < 1 || chapter > 4)
            chapter = 1;
        return _challenges.Values.Where(x => x.Chapter == chapter).ToList();
    }
    public ChallengeRecord Complete(ChallengeRecord challengeRecord)
    {
        _challenges[challengeRecord.Id] = challengeRecord with { Status = ChallengeStatus.Completed };
        _repo.SaveChallenges(_challenges.Values);
        return _challenges[challengeRecord.Id];
    }

    public ChallengeRecord GetCompleted()
    {
        throw new NotImplementedException();
    }

    public ChallengeRecord GetUnCompleted()
    {
        throw new NotImplementedException();
    }

    public ChallengeRecord Start(ChallengeRecord challengeRecord)
    {
        _challenges[challengeRecord.Id] = challengeRecord with { Status = ChallengeStatus.InProgress };
        _repo.SaveChallenges(_challenges.Values);
        return _challenges[challengeRecord.Id];
    }
}



public enum ChallengeStatus { NotStarted, InProgress, Completed }