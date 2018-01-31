using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;

namespace Test
{

    public interface IDbContext
    {
        IMongoDatabase GetDatabase();
        IMongoCollection<T> GetCollection<T>(
            string collectionName);

    }

    public class DbContext : IDbContext
    {

        private readonly IMongoDatabase _database;

        public DbContext(string connectionString)
        {
            var mongoClient = new MongoClient(connectionString);
            var mongoUrl = new MongoUrl(connectionString);
            _database = mongoClient.GetDatabase(mongoUrl.DatabaseName);
        }

        public IMongoDatabase GetDatabase()
        {
            return _database;
        }

        public IMongoCollection<T> GetCollection<T>(string collectionName)
        {
            return _database.GetCollection<T>(collectionName);
        }
    }
}
