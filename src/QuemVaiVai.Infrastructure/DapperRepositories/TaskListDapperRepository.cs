
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Infrastructure.Contexts;
using System.Data;
using System.Text.RegularExpressions;

namespace QuemVaiVai.Infrastructure.DapperRepositories
{
    public class TaskListDapperRepository : DapperRepository<TaskList>, ITaskListDapperRepository
    {
        public TaskListDapperRepository(IDbConnection connection, DapperQueryContext queryContext) : base(connection, queryContext)
        {
        }

        public async Task<List<TaskListDTO>> GetAllByEventId(int eventId)
        {
            var sql = "select id as Id, title as Title FROM {table} WHERE event_id = @EventId and deleted = false";
            var result = await GetAll<TaskListDTO>(sql, new { EventId = eventId });
            return result.ToList();
        }

        public async Task<TaskList?> GetById(int id)
        {
            var sql = GetBaseEntityValues + ", title as Title , eventId as EventId FROM {table} WHERE id = @Id and deleted = false";
            var taskList = await Get(sql, new { Id = id });

            return taskList;
        }

        public async Task<int> GetEventIdById(int id)
        {
            var sql = "SELECT event_id from {table} where id = @Id and deleted = false;";
            var eventId = await Get<int>(sql, new { Id = id });

            return eventId;
        }
    }
}
