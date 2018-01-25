using MongoDB.Driver;

namespace DesafioStone.Models
{
    public class MongoDbContext
    {
        public static string ConnectionString { get; set; }
        public static string DatabaseName { get; set; }

        private IMongoDatabase database { get; }

        public MongoDbContext()
        {
            MongoClientSettings settings = MongoClientSettings.FromUrl(new MongoUrl(ConnectionString));
            MongoClient client = new MongoClient(settings);
            database = client.GetDatabase(DatabaseName);
        }

        public IMongoCollection<Item> Items
        {
            get
            {
                return database.GetCollection<Item>("item");
            }
        }
    }
}
