using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserModule.Domain.Entities;
using UserModule.Domain.Ports;


namespace UserModule.Application.Services
{
    public class ServiceBase<TEntity> : IService<TEntity> where TEntity : BaseEntity
    {
        private readonly IRepository<TEntity> _repository;

        public ServiceBase(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(long id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _repository.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(long id, TEntity entity)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity != null)
            {
                await _repository.UpdateAsync(id, entity);
            }
        }

        public virtual async Task DeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
