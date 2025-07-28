using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Domain.Interfaces.Services
{
    public interface IService<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Guid id);
    }
}
