using MongoDB.Driver;
using UserModule.Domain.Entities;
using UserModule.Domain.Ports;
using UserModule.Infrastructure.Infrastructure.Configuration;

namespace UserModule.Infrastructure.Repositories
{
    public class Repository<T> : RepositoryBase<T>, IRepository<T>
        where T : BaseEntity, new()
    {
        protected readonly IMongoCollection<T> _collection;
        protected readonly IMongoCollection<ChangeLog> _collectionLog;

        public Repository(IMongoContext context, string collectionName)
        {
            _collection = context.GetCollection<T>(collectionName);
            _collectionLog = context.GetCollection<ChangeLog>("ChangeLogs");
        }

        public async Task SaveAsync(T entity)
        {
            SaveChanges(entity, isNew: true);
            await _collection.InsertOneAsync(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var entity = await _collection.Find(_ => true).ToListAsync();
            return entity;
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            var oldEntity = await _collection.Find(e => e.Id == entity.Id).FirstOrDefaultAsync();

            await LogChangesAsync(oldEntity, entity, entity.UserId);

            SaveChanges(entity, isNew: false);
            await _collection.ReplaceOneAsync(e => e.Id == entity.Id, entity);
        }

        public async Task LogChangesAsync<T>(T oldEntity, T newEntity, long? userId)
        {
            var fields = typeof(T).GetProperties()
                .Where(prop => prop.CanRead)
                .Select(prop => new
                {
                    Name = prop.Name,
                    OldValue = prop.GetValue(oldEntity)?.ToString(),
                    NewValue = prop.GetValue(newEntity)?.ToString()
                })
                .Where(field => field.OldValue != field.NewValue)
                .ToList();

            if (fields.Any())
            {
                var changeLog = new ChangeLog
                {
                    UserId = userId,
                    Changes = fields.Select(field => new ChangeDetail
                    {
                        Id = GenerateNewId(),
                        DateRegister = DateTime.UtcNow,
                        FieldName = field.Name,
                        OldValue = field.OldValue,
                        NewValue = field.NewValue
                    }).ToList(),
                    Id = GenerateNewId(),
                    DateRegister = DateTime.UtcNow
                };
                await _collectionLog.InsertOneAsync(changeLog);
            }
        }

        public async Task DeleteAsync(string id)
        {
            await _collection.DeleteOneAsync(e => e.Id == id);
        }
    }
}
