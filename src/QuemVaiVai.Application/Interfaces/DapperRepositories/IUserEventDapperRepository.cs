using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Application.Interfaces.DapperRepositories
{
    public interface IUserEventDapperRepository
    {
        Task<IEnumerable<UserMemberDTO>> GetAllByEventId(int eventId);
        Task<int> GetIdByEventIdAndUserId(int eventId, int userId);
        Task<bool> CanUserEditEvent(int userId, int eventId);
        Task<UserEvent?> GetByEventIdAndUserId(int eventId, int userId);
        Task<int> GetGoingByEventId(int eventId);
        Task<int> GetInterestedByEventId(int eventId);
    }
}
