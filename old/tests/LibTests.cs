using FluentAssertions;
using Lib;
using NSubstitute;
using XpTracker.Lib;

namespace XpTrackerTests;

public class LibTests
{
    private Tracker? tracker;
    public LibTests()
    {
    }

    // [Fact]
    // public void TrackerListOfChallengesShouldNotBeEmpty()
    // {
    //     var _repo = Substitute.For<ITrackerRepository>();
    //     tracker = new Tracker(_repo);
    //     tracker.Challenges.Should().NotBeEmpty();
    // }
}