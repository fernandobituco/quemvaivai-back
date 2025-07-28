
namespace QuemVaiVai.Application.Interfaces.DapperRepositories
{
    public interface IDapperRepository<T>
    {
        Task<IEnumerable<T>> QueryAsync(string sql, object? parameters = null);
        Task<T?> QueryFirstOrDefaultAsync(string sql, object? parameters = null);
        Task<int> ExecuteAsync(string sql, object? parameters = null);
    }

}
