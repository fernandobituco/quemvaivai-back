using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> AddAsync(T entity, int? userId = null);
        Task UpdateAsync(T entity, int? userId = null);
        Task DeleteAsync(int id, int? userId = null);
    }
}
