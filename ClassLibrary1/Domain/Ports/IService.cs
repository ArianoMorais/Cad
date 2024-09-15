using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Domain.Entities;

namespace UserModule.Domain.Ports
{
    public interface IService<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(long id);
        Task AddAsync(TEntity entity);
        Task UpdateAsync(long id, TEntity entity);
        Task DeleteAsync(long id);
    }
}
