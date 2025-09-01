using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Infrastructure.Contexts;
using System.Data;

namespace QuemVaiVai.Infrastructure.DapperRepositories
{
    public class EventDapperRepository : DapperRepository<Event>, IEventDapperRepository
    {
        public EventDapperRepository(IDbConnection connection, DapperQueryContext queryContext) : base(connection, queryContext)
        {
        }

        public Task<List<EventCardDTO>> GetAllByGroupId(int groupId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<EventCardDTO>> GetAllByUserId(int userId)
        {
            var sql = @"select t.id as Id, t.title as Title, t.location as location, t.description as Description, t.event_date as EventDate, t.group_id as GroupId, t.invite_code as InviteCode, 
                tg.name as GroupName, 
                COUNT(DISTINCT te_going.id) AS Going, COUNT(DISTINCT te_interested.id) AS Interested, 
                case
	                when te.role in (1,2) then true
	                else false
                end as CanEdit
                from tb_events t 
                left join tb_groups tg on tg.id = t.group_id and tg.deleted = false 
                inner join tb_user_events te on te.user_id = @UserId and te.deleted = false 
                left join tb_user_events te_going on te_going.event_id = t.id and te_going.deleted = false and te.status = 1 
                left join tb_user_events te_interested on te_interested.event_id = t.id and te_interested.deleted = false and te.status = 2 
                where t.deleted = false
                group by t.id, t.title, t.location, t.description, t.event_date, t.group_id, t.invite_code, tg.name, te.role ;";
            var groups = await GetAll<EventCardDTO>(sql, new { UserId = userId });

            return groups.ToList();
        }

        public Task<Event?> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
