using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Infrastructure.Contexts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Infrastructure.DapperRepositories
{
    public class GroupUserDapperRepository : DapperRepository<GroupUser>, IGroupUserDapperRepository
    {
        public GroupUserDapperRepository(IDbConnection connection, DapperQueryContext queryContext) : base(connection, queryContext)
        {
        }

        public async Task<bool> CanUserEditGroup(int userId, int groupId)
        {
            var sql = "SELECT EXISTS ( SELECT 1 FROM {table} WHERE user_id = @UserId AND group_id = @GroupId AND role in (1, 2));";
            var exists = await Get<bool>(sql, new { UserId = userId, GroupId = groupId });

            return exists;
        }
    }
}
