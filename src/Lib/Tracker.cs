using System.Collections;
using XpTracker.Lib;

namespace Lib;

public class Tracker
{
    private readonly Dictionary<ChapterId, Challenge> _challenges = [];

    private readonly ITrackerRepository _repo;

    public Tracker(ITrackerRepository repository)
    {
        _repo = repository;
        _challenges = _repo.GetChallenges().Result.ToDictionary(c => c.ChapterId);
    }

    public int CompletedCount =>
        _challenges.Values.Count(x => x.Status == ChallengeStatus.Done);

    public int TotalXp =>
        _challenges.Values.Sum(c => c.Xp);

    public int CurrentXp =>
        _challenges.Values.Where(c => c.Status == ChallengeStatus.Done)
            .Sum(x => x.Xp);

    public int CurrentLevel => CurrentXp switch
    {
        < 200 => 1,
        < 400 => 2,
        _ => 0
    };
    public IEnumerable<Challenge> Challenges => _challenges.Values;
    public List<Challenge> GetChapter(int chapter = 1)
    {
        if (chapter < 1 || chapter > 4)
            chapter = 1;
        return _challenges.Values.Where(x => x.ChapterId.Chapter == chapter).ToList();
    }
    public async Task<Challenge> CompleteAsync(ChapterId pos)
    {
        _challenges[pos].Status = ChallengeStatus.Done; // = challengeRecord with { Status = ChallengeStatus.Done };
        await _repo.SaveChallenges(_challenges.Values);
        return _challenges[pos];
    }

    public IEnumerable<Challenge> GetCompleted() => _challenges.Values.Where(c => c.Status == ChallengeStatus.Done);
    public IEnumerable<Challenge> GetNotStarted() => _challenges.Values.Where(c => c.Status == ChallengeStatus.Pending);
    public IEnumerable<Challenge> GetStarted() => _challenges.Values.Where(c => c.Status == ChallengeStatus.Started);

    public async Task<Challenge> StartAsync(ChapterId challengePos)
    {
        var saveTask = _repo.SaveChallenges(_challenges.Values);
        var challenge = _challenges[challengePos];
        _challenges[challenge.ChapterId].Status = ChallengeStatus.Started;

        await saveTask;

        return _challenges[challenge.ChapterId];
    }

    public Challenge? GetChallenge(ChapterId challengePos)
    {
        if (_challenges.ContainsKey(challengePos))
            return _challenges[challengePos];

        return null;
    }


}
