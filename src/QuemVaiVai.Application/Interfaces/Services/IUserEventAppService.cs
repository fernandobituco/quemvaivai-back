
using QuemVaiVai.Domain.Enums;

namespace QuemVaiVai.Application.Interfaces.Services
{
    public interface IUserEventAppService
    {
        Task ChangeRole(int eventId, int userId, Role role, int responsibleUserId);
        Task ChangeStatus(int eventId, int userId, Status status);
        Task JoinEvent(Guid inviteCode, int userId, Status status);
        Task RemoveUserFromEvent(int eventId, int userId, int responsibleUserId);
    }
}
