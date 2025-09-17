
using AutoMapper;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Exceptions;

namespace QuemVaiVai.Application.Services
{
    public class TaskListAppService : ServiceBase<TaskList>, ITaskListAppService
    {
        private readonly ITaskListDapperRepository _taskListDapperRepository;
        private readonly IUserEventDapperRepository _userEventDapperRepository;
        public TaskListAppService(
            ITaskListRepository repository,
            IMapper mapper,
            ITaskListDapperRepository taskListDapperRepository,
            IUserEventDapperRepository userEventDapperRepository) : base(repository, mapper)
        {
            _taskListDapperRepository = taskListDapperRepository;
            _userEventDapperRepository = userEventDapperRepository;
        }

        public async Task<TaskListDTO> CreateTaskListAsync(CreateTaskListDTO request, int userId)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            await CanUserEditEvent(userId, request.EventId);

            var taskList = _mapper.Map<TaskList>(request);

            return _mapper.Map<TaskListDTO>(await _repository.AddAsync(taskList, userId));
        }

        public async Task DeleteTaskListAsync(int id, int userId)
        {
            var enventId = await _taskListDapperRepository.GetEventIdById(id);

            await CanUserEditEvent(userId, enventId);

            throw new NotImplementedException();
        }

        public async Task<TaskListDTO> UpdateTaskListAsync(UpdateTaskListDTO request, int userId)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            await CanUserEditEvent(userId, request.EventId);

            var taskList = await _taskListDapperRepository.GetById(request.Id) ?? throw new NotFoundException("TaskList");

            taskList.Title = request.Title;

            await _repository.UpdateAsync(taskList, userId);

            return _mapper.Map<TaskListDTO>(taskList);
        }

        private async Task CanUserEditEvent(int userId, int eventId)
        {
            if (!await _userEventDapperRepository.CanUserEditEvent(userId, eventId))
                throw new UnauthorizedException("You cannot edit this event.");
        }
    }
}
