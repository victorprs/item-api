using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioStone.Models
{
    public class DataAccess
    {

        private MongoClient client;
        private IMongoDatabase db;
        private IMongoCollection<Item> itemCollection;

        public DataAccess(String connectionString)
        {
            client = new MongoClient(connectionString);
            db = client.GetDatabase("api");
            itemCollection = db.GetCollection<Item>("item");
        }
    }
}
