using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Application.Interfaces.DapperRepositories
{
    public interface IEventDapperRepository
    {
        Task<Event?> GetById(int id);
        Task<List<EventCardDTO>> GetAllByUserId(int userId);
        Task<List<EventCardDTO>> GetAllByGroupId(int groupId);
        Task<int?> GetIdByInviteCode(Guid inviteCode);
        Task<EventDTO?> GetByInviteCode(Guid inviteCode);
    }
}
