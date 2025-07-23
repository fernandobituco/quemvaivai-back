using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Interfaces.Repositories;
using QuemVaiVai.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Services
{
    public class ServiceBase<T> : IService<T> where T : BaseEntity
    {
        protected readonly IRepository<T> _repository;

        public ServiceBase(IRepository<T> repository)
        {
            _repository = repository;
        }

        public virtual async Task<T?> GetByIdAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public virtual async Task CreateAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public virtual async Task DeleteAsync(Guid id)
        {
            await _repository.DeleteAsync(id);
        }
    }

}
