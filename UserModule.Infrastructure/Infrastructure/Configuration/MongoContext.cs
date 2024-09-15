using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using UserModule.Infrastructure.Configuration;
using UserModule.Domain.Entities;

namespace UserModule.Infrastructure.Infrastructure.Configuration
{
    public class MongoContext : IMongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(MongoDbSettings mongoSettings)
        {
            var client = new MongoClient(mongoSettings.ConnectionString);
            _database = client.GetDatabase(mongoSettings.DatabaseName);
        }

        public IMongoCollection<TEntity> GetCollection<TEntity>(string name) where TEntity : BaseEntity
        {
            return _database.GetCollection<TEntity>(name);
        }
    }
}
