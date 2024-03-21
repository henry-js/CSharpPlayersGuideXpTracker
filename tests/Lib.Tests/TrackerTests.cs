using FluentAssertions;
using XpTracker.Lib;

namespace Lib.Tests;

public class TrackerTests
{
    private readonly Tracker _tracker;

    public TrackerTests()
    {
        var repository = new TrackerRepository();
        _tracker = new Tracker(repository);
    }

    [Fact]
    public void LoadsChallenges()
    {
        _tracker.Challenges.Should().NotBeNull();
        _tracker.TotalXp.Should().BeGreaterThan(0);
    }

    [Fact]
    public async Task CanStartAChallengeAsync()
    {
        var challengPos = new ChapterId { Chapter = 2, Id = 3 };
        var challenge = _tracker.GetChallenge(challengPos);

        challenge.Should().NotBeNull();
        challenge = await _tracker.StartAsync(challenge!.ChapterId);
        challenge.Status.Should().Be(ChallengeStatus.InProgress);
    }

    [Fact]
    public async Task CanCompleteAChallengeAsync()
    {
        var challengePos = new ChapterId { Chapter = 4, Id = 1 };
        var challenge = await _tracker.CompleteAsync(challengePos);

        challenge.Status.Should().Be(ChallengeStatus.Completed);
    }
}