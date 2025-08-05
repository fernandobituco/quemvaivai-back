using Microsoft.EntityFrameworkCore;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Infrastructure.Contexts;

namespace QuemVaiVai.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public RepositoryBase(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> AddAsync(T entity, int? userId = null)
        {
            if (userId != null)
            {
                entity.CreatedUser = (int)userId;
            }
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity, int? userId = null)
        {
            if (userId != null)
            {
                entity.UpdatedUser = (int)userId;
            }
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, int? userId = null)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }

}
