
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Application.Interfaces.DapperRepositories
{
    public interface ITaskListDapperRepository
    {
        Task<TaskList?> GetById(int id);
        Task<List<TaskListDTO>> GetAllByEventId(int eventId);
        Task<int> GetEventIdById(int id);
    }
}
