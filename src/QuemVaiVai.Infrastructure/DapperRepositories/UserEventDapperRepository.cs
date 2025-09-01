
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Infrastructure.Contexts;
using System.Data;

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
    }
}
