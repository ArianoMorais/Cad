using UserModule.Domain.Entities;

namespace UserModule.Domain.Ports
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetByIdAsync(string id);
        Task SaveAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(string id);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
