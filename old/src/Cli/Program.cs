using Lib;
using XpTracker.Lib;

var repository = new TrackerRepository();
var tracker = new Tracker(repository);


var chapter1 = tracker.GetChapter(1);
foreach (var challenge in chapter1)
{
    Console.WriteLine(challenge);
}
var startedChallenge = tracker.Start(chapter1[0]);
var completedChallenge = tracker.Complete(chapter1[0]);

// repository.CreateCsv();