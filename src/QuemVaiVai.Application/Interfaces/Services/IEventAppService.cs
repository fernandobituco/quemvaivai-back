using QuemVaiVai.Application.DTOs;

namespace QuemVaiVai.Application.Interfaces.Services
{
    public interface IEventAppService
    {
        Task<EventDTO> GetById(int groupId, int userId);
        Task<EventDTO> CreateEventAsync(CreateEventDTO request, int userId);
        Task<EventDTO> UpdateEventAsync(UpdateEventDTO request, int userId);
        Task DeleteEventAsync(int id, int userId);
        Task<EventDTO> GetByInviteCode(Guid inviteCode);
    }
}
