using System.Collections;
using System.Collections.Frozen;
using System.Globalization;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using Lib;

namespace XpTracker.Lib;

public class TrackerRepository : ITrackerRepository
{
    private readonly CsvConfiguration csvConfig = new(CultureInfo.InvariantCulture) { BadDataFound = null };
    private readonly string _challengeFile;

    public TrackerRepository()
    {
        var fileName = "all_challenges.csv";
        _challengeFile = Path.Combine(Directory.GetParent(Assembly.GetExecutingAssembly().Location)!.FullName, "challenges", fileName);
    }

    public IEnumerable<Challenge> GetChallenges()
    {
        using var reader = new StreamReader(_challengeFile);
        using var csv = new CsvReader(reader, csvConfig);
        csv.Context.RegisterClassMap<ChallengeMap>();
        var records = csv.GetRecords<Challenge>().OrderBy(c => c.ChapterId.Chapter).ThenBy(c => c.ChapterId.Id);
        return records.ToList();
    }

    public void SaveChallenges(IEnumerable<Challenge> challengeRecords)
    {
        var challenges = challengeRecords;
        using var writer = new StreamWriter(_challengeFile);
        using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
        csv.WriteRecords(challenges);
    }
}

public interface ITrackerRepository
{
    IEnumerable<Challenge> GetChallenges();

    void SaveChallenges(IEnumerable<Challenge> challenges);
}