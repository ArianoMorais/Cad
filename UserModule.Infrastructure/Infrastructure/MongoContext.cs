using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UserModule.Infrastructure.Configuration;
using UserModule.Domain.Entities;

namespace UserModule.Infrastructure
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>(string name) where TEntity : BaseEntity
        {
            return _database.GetCollection<TEntity>(name);
        }
    }
}
