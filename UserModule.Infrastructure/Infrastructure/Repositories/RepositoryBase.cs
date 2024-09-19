using UserModule.Domain.Entities;

namespace UserModule.Infrastructure.Repositories
{
    public abstract class RepositoryBase<TEntity> where TEntity : BaseEntity
    {
        protected void SaveChanges(TEntity entity, bool isNew)
        {
            if (isNew)
            {
                entity.Id = GenerateNewId();
                entity.DateRegister = DateTime.UtcNow;
            }

            entity.DateUpdate = DateTime.UtcNow;

            UpdateSubEntities(entity, isNew);
        }

        protected void UpdateSubEntities(object entity, bool isNew)
        {
            var properties = entity.GetType().GetProperties();
            foreach (var property in properties)
            {
                if (typeof(IEnumerable<BaseEntity>).IsAssignableFrom(property.PropertyType))
                {
                    var subEntities = property.GetValue(entity) as IEnumerable<BaseEntity>;
                    if (subEntities != null)
                    {
                        foreach (var subEntity in subEntities)
                        {
                            if (isNew)
                            {
                                subEntity.Id = GenerateNewId();
                                subEntity.DateRegister = DateTime.UtcNow;
                            }
                            subEntity.DateUpdate = DateTime.UtcNow;
                        }
                    }
                }
            }
        }

        protected string GenerateNewId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
