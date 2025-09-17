
using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Infrastructure.Contexts;

namespace QuemVaiVai.Infrastructure.Repositories
{
    public class TaskListRepository : RepositoryBase<TaskList>, ITaskListRepository
    {
        public TaskListRepository(AppDbContext context) : base(context)
        {
        }
    }
}
