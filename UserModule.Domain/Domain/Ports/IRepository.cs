using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Domain.Entities;

namespace UserModule.Domain.Ports
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity?> GetByIdAsync(long id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(long id, TEntity entity);
        Task DeleteAsync(long id);
        Task<IEnumerable<TEntity>> GetAllAsync();
    }
}
