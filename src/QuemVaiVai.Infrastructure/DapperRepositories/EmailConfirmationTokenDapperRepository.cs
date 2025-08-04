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
            var table = _queryContext.Table("email_confirmation_tokens");
            var sql = $"SELECT * FROM {table} WHERE token = @Token";
            var user = await QueryFirstOrDefaultAsync(sql, new { Token = token });

            return user;
        }
    }
}
