using QuemVaiVai.Application.DTOs;

namespace QuemVaiVai.Application.Interfaces.Services
{
    public interface IGroupAppService
    {
        Task<GroupDTO> GetById(int groupId, int userId);
        Task<GroupDTO> CreateGroupAsync(CreateGroupDTO request, int userId);
        Task<GroupDTO> UpdateGroupAsync(UpdateGroupDTO request, int userId);
        Task DeleteGroupAsync(int id, int userId);
        Task<GroupDTO> GetByInviteCode(Guid inviteCode);
    }
}
