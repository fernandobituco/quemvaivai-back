using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.Contexts;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Domain.Responses;
using QuemVaiVai.Infrastructure.DapperRepositories;

namespace QuemVaiVai.Api.Controllers
{
    [Route("api/taskitems")]
    [ApiController]
    public class TaskItemController : BaseController<TaskItemController>
    {
        private readonly ITaskItemAppService _appService;
        private readonly ITaskItemDapperRepository _dapperRepository;
        public TaskItemController(
            IHttpContextAccessor httpContextAccessor,
            ILogger<TaskItemController> logger,
            IMapper mapper,
            IUserContext userContext,
            ITaskItemAppService appService,
            ITaskItemDapperRepository dapperRepository) : base(httpContextAccessor, logger, mapper, userContext)
        {
            _appService = appService;
            _dapperRepository = dapperRepository;
        }

        [HttpGet("event/{eventId}")]
        [ProducesResponseType(typeof(Result<List<TaskItemResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<List<TaskItemResponse>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<List<TaskItemResponse>>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<List<TaskItemResponse>>> GetTaskListsByEvent(int eventId)
        {
            ModelStateValidation();

            var userId = _userContext.GetCurrentUserId() ?? throw new UnauthorizedException("Invalid user ID in token.");

            List<TaskItemDTO> taskLists = await _dapperRepository.GetAllByTaskListId(eventId);

            List<TaskItemResponse> response = taskLists.Select(ti => new TaskItemResponse(ti.Id, ti.Descritption, ti.AssignedTo, ti.IsDone)).ToList();

            return Result<List<TaskItemResponse>>.Success(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Result<TaskItemResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<TaskItemResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<TaskItemResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<TaskItemResponse>> CreateTaskList([FromBody] CreateTaskItemDTO dto)
        {
            ModelStateValidation();

            var userId = _userContext.GetCurrentUserId() ?? throw new UnauthorizedException("Invalid user ID in token.");

            var createdTaskItem = await _appService.CreateTaskListAsync(dto, userId);

            var response = new TaskItemResponse(createdTaskItem.Id, createdTaskItem.Descritption, null, false);

            return Result<TaskItemResponse>.Success(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Result<TaskItemResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<TaskItemResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<TaskItemResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<TaskItemResponse>> UpdateTaskList([FromBody] UpdateTaskItemDTO dto)
        {
            ModelStateValidation();

            var userId = _userContext.GetCurrentUserId() ?? throw new UnauthorizedException("Invalid user ID in token.");

            var result = await _appService.UpdateTaskListAsync(dto, userId);

            var response = new TaskItemResponse(result.Id, result.Descritption, result.AssignedTo, result.IsDone);

            return Result<TaskItemResponse>.Success(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<bool>> DeleteTaskList(int id)
        {
            var userId = _userContext.GetCurrentUserId() ?? throw new UnauthorizedException("Invalid user ID in token.");

            await _appService.DeleteTaskListAsync(id, userId);

            return Result<bool>.Success(true);
        }
    }
}