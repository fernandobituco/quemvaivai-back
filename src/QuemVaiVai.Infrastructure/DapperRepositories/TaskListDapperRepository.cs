
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

        public async Task<IEnumerable<TaskList>> GetAllByEventId(int eventId)
        {
            var sql = "select id as Id, title as Title FROM {table} WHERE eventId = @EventId and deleted = false";
            return await GetAll(sql, new { EventId = eventId });
        }

        public async Task<TaskList?> GetById(int id)
        {
            var sql = GetBaseEntityValues + ", title as Title , eventId as EventIt FROM {table} WHERE id = @Id and deleted = false";
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
