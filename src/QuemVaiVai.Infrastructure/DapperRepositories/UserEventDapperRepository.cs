
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Infrastructure.Contexts;
using System.Data;
using System.Text.RegularExpressions;

namespace QuemVaiVai.Infrastructure.DapperRepositories
{
    public class UserEventDapperRepository : DapperRepository<UserEvent>, IUserEventDapperRepository
    {
        public UserEventDapperRepository(IDbConnection connection, DapperQueryContext queryContext) : base(connection, queryContext)
        {
        }

        public async Task<IEnumerable<UserMemberDTO>> GetAllByEventId(int eventId)
        {
            var sql = "select u.id as Id, u.name as Name, ue.role as Role, ue.status as Status FROM {table} ue inner join tb_users u on u.id = ue.user_id and u.deleted = false where ue.event_id = @EventId and ue.deleted = false";
            var users = await GetAll<UserMemberDTO>(sql, new { EventId = eventId });

            return users;
        }

        public async Task<int> GetIdByEventIdAndUserId(int eventId, int userId)
        {
            var sql = "SELECT id from {table} where user_id = @UserId AND event_id = @EventId and deleted = false;";
            var id = await Get<int>(sql, new { UserId = userId, EventId = eventId });

            return id;
        }

        public async Task<UserEvent?> GetByEventIdAndUserId(int eventId, int userId)
        {
            var sql = GetBaseEntityValues + ", role as Role, event_id as EventId, user_id as UserId, status as Status FROM {table} WHERE user_id = @UserId AND event_id = @EventId and deleted = false";
            var userEvent = await Get(sql, new { UserId = userId, EventId = eventId });

            return userEvent;
        }

        public async Task<bool> CanUserEditEvent(int userId, int eventId)
        {
            var sql = "SELECT EXISTS ( SELECT 1 FROM {table} WHERE user_id = @UserId AND event_id = @EventId AND role in (1, 2) AND deleted = false);";
            var exists = await Get<bool>(sql, new { UserId = userId, EventId = eventId });

            return exists;
        }

        public async Task<int> GetGoingByEventId(int eventId)
        {
            var sql = @"SELECT count (t.id) from {table} t inner join tb_users tu on t.user_id = tu.id and tu.deleted = false where t.event_id = @EventId and t.deleted = false and t.status = 1;";
            var count = await Get<int>(sql, new { EventId = eventId });

            return count;
        }

        public async Task<int> GetInterestedByEventId(int eventId)
        {
            var sql = @"SELECT count (t.id) from {table} t inner join tb_users tu on t.user_id = tu.id and tu.deleted = false where t.event_id = @EventId and t.deleted = false and t.status = 2;";
            var count = await Get<int>(sql, new { EventId = eventId });

            return count;
        }
    }
}
