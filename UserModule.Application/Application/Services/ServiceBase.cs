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

        public virtual async Task<TEntity?> GetByIdAsync(string id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await _repository.SaveAsync(entity);
        }

        public virtual async Task UpdateAsync(TEntity entity)
        {

            var existingEntity = await _repository.GetByIdAsync(entity.Id);
            if (existingEntity != null)
            {
                await _repository.UpdateAsync(entity);
            }
        }

        public virtual async Task DeleteAsync(string id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
