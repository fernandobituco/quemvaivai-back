using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Infrastructure.DapperRepositories
{
    public class EmailConfirmationTokenDapperRepository : DapperRepository<EmailConfirmationToken>, IEmailConfirmationTokenDapperRepository
    {
        public EmailConfirmationTokenDapperRepository(IDbConnection connection, DapperQueryContext queryContext) : base(connection, queryContext)
        {
        }

        public async Task<EmailConfirmationToken> GetByToken(string token)
        {
            var sql = GetBaseEntityValues + ", user_id as UserId, used as Used, expiration as Expiration, token as Token FROM {table} WHERE token = @Token";
            var user = await Get(sql, new { Token = token });

            return user;
        }
    }
}
