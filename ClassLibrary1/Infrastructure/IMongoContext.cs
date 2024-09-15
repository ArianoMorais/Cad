using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserModule.Infrastructure
{
    public interface IMongoContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>(string name) where TEntity : class;
    }
}
