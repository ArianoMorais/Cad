using MongoDB.Driver;
using UserModule.Domain.Entities;

namespace UserModule.Infrastructure.Infrastructure.Configuration
{
    public interface IMongoContext
    {
        IMongoCollection<TEntity> GetCollection<TEntity>(string name) where TEntity : BaseEntity;
    }
}
