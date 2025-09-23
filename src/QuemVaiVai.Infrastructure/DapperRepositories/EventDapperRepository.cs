using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Infrastructure.Contexts;
using System.Data;
using System.Text;

namespace QuemVaiVai.Infrastructure.DapperRepositories
{
    public class EventDapperRepository : DapperRepository<Event>, IEventDapperRepository
    {
        public EventDapperRepository(IDbConnection connection, DapperQueryContext queryContext) : base(connection, queryContext)
        {
        }

        public async Task<List<EventCardDTO>> GetAllByUserId(int userId, EventFiltersDto filters)
        {
            var sql = new StringBuilder(@"
                select 
                    t.id as Id, 
                    t.title as Title, 
                    t.location as Location, 
                    t.description as Description, 
                    t.event_date as EventDate, 
                    t.group_id as GroupId, 
                    t.invite_code as InviteCode, 
                    tg.name as GroupName, 
                    tue.status as Status, 
                    COUNT(DISTINCT tue_going.id) AS Going, 
                    COUNT(DISTINCT tue_interested.id) AS Interested, 
                    case when tue.role in (1,2) then true else false end as CanEdit, 
                    case when exists (
                        select 1 
                        from tb_task_lists tl 
                        where tl.event_id = t.id 
                          and tl.deleted = false
                    ) then true else false end as ActiveTaskList
                from tb_events t 
                left join tb_groups tg 
                    on tg.id = t.group_id 
                   and tg.deleted = false 
                left join tb_user_events tue 
                    on tue.user_id = @UserId 
                   and tue.deleted = false 
                   and tue.event_id = t.id 
                left join tb_user_events tue_going 
                    on tue_going.event_id = t.id 
                   and tue_going.deleted = false 
                   and tue_going.status = 1 
                left join tb_user_events tue_interested 
                    on tue_interested.event_id = t.id 
                   and tue_interested.deleted = false 
                   and tue_interested.status = 2 
                left join tb_group_users tgu
                    on tgu.group_id = t.group_id
                   and tgu.user_id = @UserId
                   and tgu.deleted = false
                where 
                    t.deleted = false
                    and (tue.id is not null or tgu.id is not null)
                group by 
                    t.id, t.title, t.location, t.description, t.event_date, 
                    t.group_id, t.invite_code, tg.name, tue.status, tue.role
            ");

            // 🔹 GroupId
            if (filters.GroupId.HasValue)
            {
                if (filters.GroupId == 0)
                    sql.Append(" and t.group_id is null");
                else
                    sql.Append(" and t.group_id = @GroupId");
            }

            // 🔹 SelfStatus
            if (filters.Situation.HasValue)
            {
                sql.Append(" and tue.status = @Situation");
            }

            // 🔹 Status (Upcoming ou Past)
            if (filters.Status.HasValue)
            {
                if (filters.Status == EventStatusFilter.Upcoming)
                {
                    sql.Append(" and t.event_date >= NOW()");
                }
                else if (filters.Status == EventStatusFilter.Past)
                {
                    sql.Append(" and t.event_date < NOW()");
                }
            }

            sql.Append(@"
                group by 
                    t.id, t.title, t.location, t.description, 
                    t.event_date, t.group_id, t.invite_code, 
                    tg.name, tue.role, tue.status
            ");

            var events = await GetAll<EventCardDTO>(sql.ToString(), new
            {
                UserId = userId,
                filters.GroupId,
                filters.Status,
                filters.Situation
            });

            return events.ToList();
        }


        public async Task<Event?> GetById(int id)
        {
            var sql = GetBaseEntityValues + ", title as Title, description as Description, location as location, event_date as EventDate, group_id as GroupId FROM {table} WHERE id = @Id and deleted = false";
            var result = await Get(sql, new { Id = id });

            return result;
        }

        public async Task<EventDTO?> GetByInviteCode(Guid inviteCode)
        {
            var sql = @"select t.id as Id, t.title as Title, t.location as location, t.description as Description, t.event_date as EventDate, t.group_id as GroupId, t.invite_code as InviteCode, 
                tg.name as GroupName, tue.status as Status, 
                COUNT(DISTINCT tue_going.id) AS Going, COUNT(DISTINCT tue_interested.id) AS Interested, 
                case
	                when tue.role in (1,2) then true
	                else false
                end as CanEdit
                from tb_events t 
                left join tb_groups tg on tg.id = t.group_id and tg.deleted = false 
                inner join tb_user_events tue on tue.event_id = t.id and tue.deleted = false and tue.event_id = t.id 
                left join tb_user_events tue_going on tue_going.event_id = t.id and tue_going.deleted = false and tue_going.status = 1 
                left join tb_user_events tue_interested on tue_interested.event_id = t.id and tue_interested.deleted = false and tue_interested.status = 2 
                WHERE t.invite_code = @InviteCode and t.deleted = false 
                group by t.id, t.title, t.description, tg.name, tue.role, tue.status ";
            var eventDto = await Get<EventDTO>(sql, new { InviteCode = inviteCode });

            return eventDto;
        }

        public async Task<int?> GetIdByInviteCode(Guid inviteCode)
        {
            var sql = "SELECT id from {table} where invite_code = @InviteCode and deleted = false;";
            var id = await Get<int>(sql, new { InviteCode = inviteCode });

            return id;
        }
    }
}
