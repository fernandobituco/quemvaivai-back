using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Infrastructure.Contexts;
using System.Data;
using System.Text.RegularExpressions;

namespace QuemVaiVai.Infrastructure.DapperRepositories
{
    public class PasswordResetTokenDapperRepository : DapperRepository<PasswordResetToken>, IPasswordResetTokenDapperRepository
    {
        public PasswordResetTokenDapperRepository(IDbConnection connection, DapperQueryContext queryContext) : base(connection, queryContext)
        {
        }

        public async Task<PasswordResetToken?> GetLastByUserId(int userId)
        {
            var sql = GetBaseEntityValues + ", token_hash as TokenHash, used as Used, user_id as UserId FROM {table} WHERE user_id = @UserId AND expires_at > now() and deleted = false";
            var groupUser = await Get(sql, new { UserId = userId });

            return groupUser;
        }
    }
}