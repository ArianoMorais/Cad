using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Domain.Entities;
using UserModule.Domain.Ports;

namespace UserModule.Infrastructure.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly IMongoCollection<TEntity> _collection;

        protected RepositoryBase(IMongoContext context, string collectionName)
        {
            _collection = context.GetCollection<TEntity>(collectionName);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var entity = await _collection.Find(_ => true).ToListAsync();
            return entity;
        }

        public async Task<TEntity?> GetByIdAsync(long id)
        {
            return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(long id, TEntity entity)
        {
            await _collection.ReplaceOneAsync(e => e.Id == id, entity);
        }

        public async Task DeleteAsync(long id)
        {
            await _collection.DeleteOneAsync(e => e.Id == id);
        }
    }
}
