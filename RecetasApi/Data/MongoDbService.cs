using MongoDB.Driver;

namespace RecetasApi.Data
{
    public class MongoDbService
    {
        private readonly IMongoDatabase? _database;

        public MongoDbService(IConfiguration configuration)
        {
            var connectingString = Environment.GetEnvironmentVariable("DB_CONNECTION") ?? configuration.GetConnectionString("DbConnection");
            var mongoUrl = MongoUrl.Create(connectingString);
            var mongoClient = new MongoClient(mongoUrl);
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName ?? "RecipesDB");
        }

        public IMongoDatabase? Database => _database;
    }
}
