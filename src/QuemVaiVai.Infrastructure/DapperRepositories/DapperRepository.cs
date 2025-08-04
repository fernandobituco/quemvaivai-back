using Dapper;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Infrastructure.Contexts;
using System.Data;

namespace QuemVaiVai.Infrastructure.DapperRepositories
{
    public class DapperRepository<T>
    {
        protected readonly IDbConnection _connection;
        protected readonly DapperQueryContext _queryContext;

        public DapperRepository(
            IDbConnection connection,
            DapperQueryContext queryContext)
        {
            _connection = connection;
            _queryContext = queryContext;
        }

        protected async Task<IEnumerable<T>> QueryAsync(string sql, object? parameters = null)
        {
            return await _connection.QueryAsync<T>(sql, parameters);
        }

        protected async Task<T?> QueryFirstOrDefaultAsync(string sql, object? parameters = null)
        {
            return await _connection.QueryFirstOrDefaultAsync<T>(sql, parameters);
        }

        protected async Task<int> ExecuteAsync(string sql, object? parameters = null)
        {
            return await _connection.ExecuteAsync(sql, parameters);
        }
    }
}
