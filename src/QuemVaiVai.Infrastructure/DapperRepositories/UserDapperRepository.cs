using Dapper;
using Microsoft.EntityFrameworkCore.Query;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Infrastructure.Contexts;
using System.Data;

namespace QuemVaiVai.Infrastructure.DapperRepositories
{
    public class UserDapperRepository : DapperRepository<User>, IUserDapperRepository
    {
        public UserDapperRepository(
            IDbConnection connection,
            DapperQueryContext queryContext) : base(connection, queryContext)
        {
        }

        public async Task<bool> ExistsByEmail(string email)
        {
            var table = _queryContext.Table("users");
            var sql = @$"SELECT 1 FROM {table} WHERE ""Email"" = @Email LIMIT 1;";
            var exists = await _connection.ExecuteScalarAsync<int?>(sql, new { Email = email });

            bool userExists = exists.HasValue;

            return userExists;
        }

        public async Task<User> GetByEmail(string email)
        {
            var table = _queryContext.Table("users");
            var sql = $"SELECT * FROM {table} WHERE email = @Email";
            var user = await QueryFirstOrDefaultAsync(sql, new { Email = email });

            if (user == null)
                throw new NotFoundException("Usuário");

            return user;
        }

        public async Task<User> GetById(int id)
        {
            var table = _queryContext.Table("users");
            var sql = $"SELECT * FROM {table} WHERE id = @Id";
            var user = await QueryFirstOrDefaultAsync(sql, new { Id = id.ToString() });

            if (user == null)
                throw new NotFoundException("Usuário");

            return user;
        }
    }
}
