
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Application.Interfaces.DapperRepositories
{
    public interface ITaskItemDapperRepository
    {
        Task<TaskItem?> GetById(int id);
        Task<List<TaskItemDTO>> GetAllByTaskListId(int taskListId);
    }
}
