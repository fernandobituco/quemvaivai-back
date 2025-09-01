using QuemVaiVai.Application.DTOs;

namespace QuemVaiVai.Application.Interfaces.DapperRepositories
{
    public interface IUserEventDapperRepository
    {
        Task<IEnumerable<UserMemberDTO>> GetAllByEventId(int eventId);
    }
}
