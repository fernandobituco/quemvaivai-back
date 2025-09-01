using AutoMapper;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Enums;

namespace QuemVaiVai.Application.Services
{
    public class EventAppService : ServiceBase<Event>, IEventAppService
    {
        private readonly IUserEventRepository _userEventRepository;
        public EventAppService(
            IEventRepository repository,
            IMapper mapper,
            IUserEventRepository userEventRepository) : base(repository, mapper)
        {
            _userEventRepository = userEventRepository;
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

        public Task DeleteGroupAsync(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<EventDTO> GetById(int groupId, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<EventDTO> UpdateEventAsync(UpdateGroupDTO request, int userId)
        {
            throw new NotImplementedException();
        }
    }
}
