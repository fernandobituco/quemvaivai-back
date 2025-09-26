using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Application.Interfaces.DapperRepositories
{
    public interface IEventDapperRepository
    {
        Task<Event?> GetById(int id);
        Task<EventCardDTO?> GetCardById(int id, int userId);
        Task<List<EventCardDTO>> GetAllByUserId(int userId, EventFiltersDto filters);
        Task<int?> GetIdByInviteCode(Guid inviteCode);
        Task<EventDTO?> GetByInviteCode(Guid inviteCode);
    }
}
