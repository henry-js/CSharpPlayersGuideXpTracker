// using LiteDB;

// namespace Lib;

// public class LiteDbContext
// {
//     private const string TaskCol = "challenges";
//     private readonly LiteDatabase db;
//     private readonly ILogger<LiteDbContext> logger;
//     private readonly LiteDbOptions _options;

//     public LiteDbContext(IOptions<LiteDbOptions> options, ILogger<LiteDbContext> logger)
//     {
//         this.logger = logger;
//         _options = options.Value;
//         try
//         {
//             var db = new LiteDatabase(_options.ConnectionString);
//             this.db = db ?? throw new Exception(nameof(db));
//         }
//         catch (Exception ex)
//         {
//             throw new Exception("Can't find or create LiteDb database.", ex);
//         }
//     }
// }
