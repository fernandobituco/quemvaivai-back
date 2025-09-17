using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Application.Interfaces.DapperRepositories
{
    public interface IGroupDapperRepository
    {
        Task<Group?> GetById(int id);
        Task<List<GroupCardDTO>> GetAllByUserId(int userId);
        Task<int?> GetIdByInviteCode(Guid inviteCode);
        Task<GroupDTO?> GetByInviteCode(Guid inviteCode);
    }
}
