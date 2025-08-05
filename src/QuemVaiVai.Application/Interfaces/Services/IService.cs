using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Application.Interfaces.Services
{
    public interface IService<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
