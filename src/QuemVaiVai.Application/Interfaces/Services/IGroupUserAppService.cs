using QuemVaiVai.Domain.Enums;

namespace QuemVaiVai.Application.Interfaces.Services
{
    public interface IGroupUserAppService
    {
        Task ChangeRole(int groupId, int userId, Role role, int responsibleUserId);
        Task JoinGroup(Guid inviteCode, int userId);
        Task RemoveUserFromGroup(int groupId, int userId, int responsibleUserId);
    }
}
