using QuemVaiVai.Application.DTOs;
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
    public class GroupDapperRepository : DapperRepository<Group>, IGroupDapperRepository
    {
        public GroupDapperRepository(IDbConnection connection, DapperQueryContext queryContext) : base(connection, queryContext)
        {
        }

        public async Task<List<GroupCardDTO>> GetAllByUserId(int userId)
        {
            var sql = @"select g.id as Id, g.name as Name, g.description as Description, COUNT(DISTINCT e.id) AS EventCount, COUNT(DISTINCT gu_all.user_id) AS MemberCount, 
                case 
                    when g2.role IN (1, 2) then true 
                    else false 
                end as CanEdit
                from {table} g  
                inner join group_users g2 on g2.group_id = g.id and g2.user_id = @UserId and g2.deleted = false 
                left join events e on e.group_id = g.id and e.deleted = false
                left join group_users gu_all on gu_all.group_id = g.id and gu_all.deleted = false
                where g.deleted = false
                group by g.id, g.name, g.description, g2.role ;";
            var groups = await GetAll<GroupCardDTO>(sql, new { UserId = userId });

            return groups.ToList();
        }

        public async Task<Group?> GetById(int id)
        {
            var sql = GetBaseEntityValues + ", name as Name, description as Description FROM {table} WHERE id = @Id and deleted = false";
            var group = await Get(sql, new { Id = id });

            return group;
        }
    }
}
