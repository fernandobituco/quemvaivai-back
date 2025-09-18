using Dapper;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Infrastructure.Contexts;
using System.Data;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace QuemVaiVai.Infrastructure.DapperRepositories
{
    public class DapperRepository<T> where T : BaseEntity
    {
        protected readonly IDbConnection _connection;
        protected readonly DapperQueryContext _queryContext;
        protected readonly string TableName;
        protected readonly string GetBaseEntityValues = "select id as Id, created_at as CreatedAt, created_user as CreatedUser, updated_at as UpdatedAt, deleted as Deleted, deleted_at as DeletedAt, deleted_user as DeletedUser";

        public DapperRepository(
            IDbConnection connection,
            DapperQueryContext queryContext)
        {
            _connection = connection;
            _queryContext = queryContext;
            TableName = _queryContext.Table(ToSnakeCase(typeof(T).Name));
        }

        protected async Task<IEnumerable<T>> GetAll(string sql, object? parameters = null)
        {
            var parsedSql = ReplaceTablePlaceholder(sql);
            return await _connection.QueryAsync<T>(parsedSql, parameters);
        }

        protected async Task<T?> Get(string sql, object? parameters = null)
        {
            var parsedSql = ReplaceTablePlaceholder(sql);
            return await _connection.QueryFirstOrDefaultAsync<T>(parsedSql, parameters);
        }

        protected async Task<int> ExecuteAsync(string sql, object? parameters = null)
        {
            var parsedSql = ReplaceTablePlaceholder(sql);
            return await _connection.ExecuteAsync(parsedSql, parameters);
        }

        protected async Task<IEnumerable<TModel>> GetAll<TModel>(string sql, object? parameters = null)
        {
            var parsedSql = ReplaceTablePlaceholder(sql);
            return await _connection.QueryAsync<TModel>(parsedSql, parameters);
        }

        protected async Task<TModel?> Get<TModel>(string sql, object? parameters = null)
        {
            var parsedSql = ReplaceTablePlaceholder(sql);
            return await _connection.QueryFirstOrDefaultAsync<TModel>(parsedSql, parameters);
        }

        private static string ToSnakeCase(string input)
        {
            return Regex.Replace(input, @"([a-z0-9])([A-Z])", "$1_$2").ToLower() + "s";
        }

        private string ReplaceTablePlaceholder(string sql)
        {
            return sql.Replace("{table}", TableName);
        }
    }
}
