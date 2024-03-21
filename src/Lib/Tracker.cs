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
    private readonly Dictionary<ChapterId, Challenge> _challenges = [];

    private readonly ITrackerRepository _repo;

    public Tracker(ITrackerRepository repository)
    {
        _repo = repository;
        _challenges = _repo.GetChallenges().ToDictionary(c => c.ChapterId);
    }

    public int CompletedCount =>
        _challenges.Values.Count(x => x.Status == ChallengeStatus.Completed);

    public int TotalXp =>
        _challenges.Values.Sum(c => c.Xp);

    public int CurrentXp =>
        _challenges.Values.Where(c => c.Status == ChallengeStatus.Completed)
            .Sum(x => x.Xp);

    public IEnumerable<Challenge> Challenges => _challenges.Values;
    public List<Challenge> GetChapter(int chapter = 1)
    {
        if (chapter < 1 || chapter > 4)
            chapter = 1;
        return _challenges.Values.Where(x => x.ChapterId.Chapter == chapter).ToList();
    }
    public Challenge Complete(ChapterId pos)
    {
        _challenges[pos].Status = ChallengeStatus.Completed; // = challengeRecord with { Status = ChallengeStatus.Completed };
        _repo.SaveChallenges(_challenges.Values);
        return _challenges[pos];
    }

    public Challenge GetCompleted()
    {
        throw new NotImplementedException();
    }

    public Challenge GetUnCompleted()
    {
        throw new NotImplementedException();
    }
    public IEnumerable<Challenge> GetStarted() => _challenges.Values.Where(c => c.Status == ChallengeStatus.InProgress);

    public Challenge Start(ChapterId challengePos)
    {
        var challenge = _challenges[challengePos];
        _challenges[challenge.ChapterId].Status = ChallengeStatus.InProgress;
        _repo.SaveChallenges(_challenges.Values);
        return _challenges[challenge.ChapterId];
    }

    public Challenge? GetChallenge(ChapterId challengePos)
    {
        if (_challenges.ContainsKey(challengePos))
            return _challenges[challengePos];

        return null;
    }
}