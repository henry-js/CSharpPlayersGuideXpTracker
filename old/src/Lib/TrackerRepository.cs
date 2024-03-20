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

    private readonly CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture) { BadDataFound = null };
    public TrackerRepository()
    {
        var fileName = "all_challenges.csv";
        if (Path.IsPathFullyQualified(fileName))
            _challengeFile = fileName;
        else
            _challengeFile = Path.Combine(Directory.GetParent(Assembly.GetExecutingAssembly().Location)!.FullName, fileName);
    }

    private readonly string _challengeFile;

    public IEnumerable<ChallengeRecord> GetChallenges()
    {
        using var reader = new StreamReader(_challengeFile);
        using var csv = new CsvReader(reader, csvConfig);
        csv.Context.RegisterClassMap<ChallengeMap>();
        var records = csv.GetRecords<Challenge>().ToList();
        return records.Select(x => x.ToRecord());
    }

    public void SaveChallenges(IEnumerable<ChallengeRecord> challengeRecords)
    {
        using var writer = new StreamWriter(_challengeFile);
        using var csv = new CsvWriter(writer, CultureInfo.InvariantCulture);
        var challenges = challengeRecords.Select(c => c.ToChallenge());
        csv.WriteRecords(challenges);
    }
}

public interface ITrackerRepository
{
    IEnumerable<ChallengeRecord> GetChallenges();

    // void SaveChallenge(ChallengeRecord challenge);
    void SaveChallenges(IEnumerable<ChallengeRecord> challenges);
}