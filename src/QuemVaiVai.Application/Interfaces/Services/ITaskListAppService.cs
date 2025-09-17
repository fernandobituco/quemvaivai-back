
using QuemVaiVai.Application.DTOs;

namespace QuemVaiVai.Application.Interfaces.Services
{
    public interface ITaskListAppService
    {
        Task<TaskListDTO> CreateTaskListAsync(CreateTaskListDTO request, int userId);
        Task<TaskListDTO> UpdateTaskListAsync(UpdateTaskListDTO request, int userId);
        Task DeleteTaskListAsync(int id, int userId);
    }
}
