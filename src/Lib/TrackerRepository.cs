using System.Globalization;
using System.Reflection;
using CsvHelper;
using CsvHelper.Configuration;
using Lib;
using LiteDB;

namespace XpTracker.Lib;

// public class TrackerRepository : ITrackerRepository
// {
//     private readonly CsvConfiguration csvConfig = new(CultureInfo.InvariantCulture) { BadDataFound = null };
//     private readonly string _challengeFile;

//     public TrackerRepository()
//     {
//         var fileName = "all_challenges.csv";
//         _challengeFile = Path.Combine(Directory.GetParent(Assembly.GetExecutingAssembly().Location)!.FullName, "challenges", fileName);
//     }

//     public IEnumerable<Challenge> GetChallenges()
//     {
//         using var reader = new StreamReader(_challengeFile);
//         using var csv = new CsvReader(reader, csvConfig);
//         csv.Context.RegisterClassMap<ChallengeMap>();
//         var records = csv.GetRecords<Challenge>().OrderBy(c => c.ChapterId.Chapter).ThenBy(c => c.ChapterId.Number);
//         return records.ToList();
//     }

//     public async Task SaveChallenges(IEnumerable<Challenge> challenges)
//     {
//         using var writer = new StreamWriter(_challengeFile);
//         using var csv = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
//         await csv.WriteRecordsAsync(challenges);
//     }
// }

public class LiteTrackerRepository : ITrackerRepository
{
    public LiteTrackerRepository(LiteDatabase db)
    {
        db.GetCollection<Challenge>();
        repo = new LiteRepository(db);
        this.db = db;
    }

    private readonly LiteRepository repo = default!;
    private readonly LiteDatabase db;

    public IEnumerable<Challenge> GetChallenges()
    {
        return db.GetCollection<Challenge>().FindAll().ToList();
    }

    public async Task SaveChallenges(IEnumerable<Challenge> challenges)
    {
        foreach (var challenge in challenges)
        {
            await Task.Run(() => repo.Insert(challenge));
        }
    }
}

public interface ITrackerRepository
{
    IEnumerable<Challenge> GetChallenges();

    Task SaveChallenges(IEnumerable<Challenge> challenges);
}
