using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Infrastructure.Contexts;
using System.Data;

namespace QuemVaiVai.Infrastructure.DapperRepositories
{
    public class GroupUserDapperRepository : DapperRepository<GroupUser>, IGroupUserDapperRepository
    {
        public GroupUserDapperRepository(IDbConnection connection, DapperQueryContext queryContext) : base(connection, queryContext)
        {
        }

        public async Task<bool> CanUserEditGroup(int userId, int groupId)
        {
            var sql = "SELECT EXISTS ( SELECT 1 FROM {table} WHERE user_id = @UserId AND group_id = @GroupId AND role in (1, 2) AND deleted = false);";
            var exists = await Get<bool>(sql, new { UserId = userId, GroupId = groupId });

            return exists;
        }

        public async Task<GroupUser?> GetByGroupIdAndUserId(int groupId, int userId)
        {
            var sql = GetBaseEntityValues + ", role as Role, group_id as GroupId, user_id as UserId FROM {table} WHERE user_id = @UserId AND group_id = @GroupId and deleted = false";
            var groupUser = await Get(sql, new { UserId = userId, GroupId = groupId });

            return groupUser;
        }

        public async Task<int> GetIdByGroupIdAndUserId(int groupId, int userId)
        {
            var sql = "SELECT id from {table} where user_id = @UserId AND group_id = @GroupId and deleted = false;";
            var id = await Get<int>(sql, new { UserId = userId, GroupId = groupId });

            return id;
        }

        public async Task<int> GetMemberCountByGroupId(int groupId)
        {
            var sql = @"SELECT count (t.id) from {table} t inner join tb_users tu on t.user_id = tu.id and tu.deleted = false where t.group_id = @GroupId and t.deleted = false;";
            var count = await Get<int>(sql, new { GroupId = groupId });

            return count;
        }

        public async Task<IEnumerable<UserMemberDTO>> GetAllByGroupId(int groupId)
        {
            var sql = "select u.id as Id, u.name as Name, gu.role as Role FROM {table} gu inner join tb_users u on u.id = gu.user_id and u.deleted = false where gu.group_id = @GroupId and gu.deleted = false";
            var users = await GetAll<UserMemberDTO>(sql, new { GroupId = groupId });

            return users;
        }
    }
}
