
using AutoMapper;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Enums;
using QuemVaiVai.Domain.Exceptions;
using System.Data;
using System.Text.RegularExpressions;

namespace QuemVaiVai.Application.Services
{
    public class UserEventAppService : ServiceBase<UserEvent>, IUserEventAppService
    {
        private readonly IEventDapperRepository _eventDapperRepository;
        private readonly IUserEventDapperRepository _userEventDapperRepository;
        public UserEventAppService(
            IUserEventRepository repository,
            IMapper mapper,
            IEventDapperRepository eventDapperRepository,
            IUserEventDapperRepository userEventDapperRepository) : base(repository, mapper)
        {
            _eventDapperRepository = eventDapperRepository;
            _userEventDapperRepository = userEventDapperRepository;
        }

        public async Task ChangeRole(int eventId, int userId, Role role, int responsibleUserId)
        {
            if (role == Role.ADMIN)
                throw new RoleChangeNotAllowedException();

            await CanUserEditEvent(eventId, responsibleUserId);

            var userEvent = await _userEventDapperRepository.GetByEventIdAndUserId(eventId, userId) ?? throw new NotFoundException("UserEvent");

            if (userEvent.Role == Role.ADMIN)
                throw new RoleChangeNotAllowedException();

            userEvent.Role = role;

            await _repository.UpdateAsync(userEvent);
        }

        public async Task ChangeStatus(int eventId, int userId, Status status)
        {
            var userEvent = await _userEventDapperRepository.GetByEventIdAndUserId(eventId, userId) ?? throw new NotFoundException("UserEvent");

            userEvent.Status = status;

            await _repository.UpdateAsync(userEvent);
        }

        public async Task JoinEvent(Guid inviteCode, int userId, Status status)
        {
            var eventId = await _eventDapperRepository.GetIdByInviteCode(inviteCode);

            if (eventId == null || eventId <= 0)
            {
                throw new NotFoundException("Group");
            }

            var isUserInGroupEvent = await _userEventDapperRepository.GetIdByEventIdAndUserId((int)eventId, userId);

            if (isUserInGroupEvent > 0)
            {
                var userEvent = await _userEventDapperRepository.GetByEventIdAndUserId((int)eventId, userId) ?? throw new NotFoundException("UserEvent");

                userEvent.Status = status;

                await _repository.UpdateAsync(userEvent, userId);
            }
            else
            {
                UserEvent userEvent = new()
                {
                    EventId = (int)eventId,
                    UserId = userId,
                    Role = Role.GUEST,
                    Status = status
                };

                await _repository.AddAsync(userEvent, userId);
            }
        }

        public async Task RemoveUserFromEvent(int eventId, int userId, int responsibleUserId)
        {
            await CanUserEditEvent(eventId, responsibleUserId);

            var userEvent = await _userEventDapperRepository.GetIdByEventIdAndUserId(eventId, userId);

            if (userEvent <= 0)
                throw new NotFoundException("GroupUser");

            await _repository.DeleteAsync(userEvent, responsibleUserId);
        }

        private async Task CanUserEditEvent(int eventId, int userId)
        {
            if (!await _userEventDapperRepository.CanUserEditEvent(eventId, userId))
                throw new UnauthorizedException("You cannot edit this event.");
        }
    }
}
