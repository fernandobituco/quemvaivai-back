
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Application.Interfaces.DapperRepositories
{
    public interface ITaskListDapperRepository
    {
        Task<TaskList?> GetById(int id);
        Task<IEnumerable<TaskList>> GetAllByEventId(int eventId);
        Task<int> GetEventIdById(int id);
    }
}
