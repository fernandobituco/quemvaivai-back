using AutoMapper;
using Microsoft.Extensions.Logging;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Enums;
using QuemVaiVai.Domain.Exceptions;

namespace QuemVaiVai.Application.Services
{
    public class EventAppService : ServiceBase<Event>, IEventAppService
    {
        private readonly IUserEventRepository _userEventRepository;
        private readonly IEventDapperRepository _eventDapperRepository;
        private readonly IUserEventDapperRepository _userEventDapperRepository;
        public EventAppService(
            IEventRepository repository,
            IMapper mapper,
            IUserEventRepository userEventRepository,
            IEventDapperRepository eventDapperRepository,
            IUserEventDapperRepository userEventDapperRepository) : base(repository, mapper)
        {
            _userEventRepository = userEventRepository;
            _eventDapperRepository = eventDapperRepository;
            _userEventDapperRepository = userEventDapperRepository;
        }

        public async Task<EventDTO> CreateEventAsync(CreateEventDTO request, int userId)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            EventDTO result = new();

            Event eventCreate = _mapper.Map<Event>(request);

            var createdEvent = await _repository.AddAsync(eventCreate, userId);

            if (createdEvent != null)
            {
                result = _mapper.Map<EventDTO>(createdEvent);
                //result.GroupName = await _grouDapperRepository.GetNameById(createdEvent.GroupId);

                UserEvent userEvent = new()
                {
                    EventId = createdEvent.Id,
                    UserId = userId,
                    Status = Status.GOING,
                    Role = Role.ADMIN,
                };
                await _userEventRepository.AddAsync(userEvent, userId);
            }
            return result;
        }

        public async Task DeleteEventAsync(int id, int userId)
        {
            await CanUserEditEvent(userId, id);

            await _repository.DeleteAsync(id, userId);
        }

        public async Task<EventDTO> GetById(int eventId, int userId)
        {
            await CanUserEditEvent(userId, eventId);

            var eventDto = _mapper.Map<EventDTO>(await _eventDapperRepository.GetById(eventId));

            eventDto.Going = await _userEventDapperRepository.GetGoingByEventId(eventId);
            eventDto.Interested = await _userEventDapperRepository.GetInterestedByEventId(eventId);

            return eventDto ?? throw new NotFoundException("Event");
        }

        public async Task<EventDTO> GetByInviteCode(Guid inviteCode)
        {
            var eventDto = _mapper.Map<EventDTO>(await _eventDapperRepository.GetByInviteCode(inviteCode));

            return eventDto ?? throw new NotFoundException("Event");
        }

        public async Task<EventDTO> UpdateEventAsync(UpdateEventDTO request, int userId)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            await CanUserEditEvent(userId, request.Id);

            var eventById = await _eventDapperRepository.GetById(request.Id) ?? throw new NotFoundException("Grupo");

            eventById.Title = request.Title;
            eventById.Description = request.Description;
            eventById.EventDate = request.EventDate;
            eventById.Location = request.Location;
            eventById.GroupId = request.GroupId;

            await _repository.UpdateAsync(eventById, userId);

            var eventDto = _mapper.Map<EventDTO>(eventById);

            eventDto.Going = await _userEventDapperRepository.GetGoingByEventId(eventById.Id);
            eventDto.Interested = await _userEventDapperRepository.GetInterestedByEventId(eventById.Id);

            return eventDto;
        }

        private async Task CanUserEditEvent(int userId, int eventId)
        {
            if (!await _userEventDapperRepository.CanUserEditEvent(userId, eventId))
                throw new UnauthorizedException("You cannot edit this event.");
        }
    }
}
