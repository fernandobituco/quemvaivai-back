using QuemVaiVai.Application.DTOs;

namespace QuemVaiVai.Application.Interfaces.Services
{
    public interface ITaskItemAppService
    {
        Task<TaskItemDTO> CreateTaskListAsync(CreateTaskItemDTO request, int userId);
        Task<TaskItemDTO> UpdateTaskListAsync(UpdateTaskItemDTO request, int userId);
        Task DeleteTaskListAsync(int id, int userId);
    }
}
