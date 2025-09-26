using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.Contexts;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Application.Services;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Domain.Responses;

namespace QuemVaiVai.Api.Controllers
{
    [Route("api/tasklists")]
    [ApiController]
    public class TaskListController : BaseController<TaskListController>
    {
        private readonly ITaskListDapperRepository _dapperRepository;
        private readonly ITaskListAppService _appService;
        public TaskListController(
            IHttpContextAccessor httpContextAccessor,
            ILogger<TaskListController> logger,
            IMapper mapper,
            IUserContext userContext,
            ITaskListDapperRepository dapperRepository,
            ITaskListAppService appService) : base(httpContextAccessor, logger, mapper, userContext)
        {
            _dapperRepository = dapperRepository;
            _appService = appService;
        }

        [HttpGet("event/{eventId}")]
        [ProducesResponseType(typeof(Result<List<TaskListResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<List<TaskListResponse>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<List<TaskListResponse>>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<List<TaskListResponse>>> GetTaskListsByEvent(int eventId)
        {
            ModelStateValidation();

            var userId = _userContext.GetCurrentUserId() ?? throw new UnauthorizedException("Invalid user ID in token.");

            List<TaskListDTO> taskLists = await _dapperRepository.GetAllByEventId(eventId);

            List<TaskListResponse> response = taskLists.Select(e => new TaskListResponse(e.Id,e.Title)).ToList();

            return Result<List<TaskListResponse>>.Success(response);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Result<TaskListResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<TaskListResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<TaskListResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<TaskListResponse>> CreateTaskList([FromBody] CreateTaskListDTO dto)
        {
            ModelStateValidation();

            var userId = _userContext.GetCurrentUserId() ?? throw new UnauthorizedException("Invalid user ID in token.");

            var createdTaskList = await _appService.CreateTaskListAsync(dto, userId);

            var response = new TaskListResponse(createdTaskList.Id, createdTaskList.Title);

            return Result<TaskListResponse>.Success(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Result<TaskListResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<TaskListResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<TaskListResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<TaskListResponse>> UpdateTaskList([FromBody] UpdateTaskListDTO dto)
        {
            ModelStateValidation();

            var userId = _userContext.GetCurrentUserId() ?? throw new UnauthorizedException("Invalid user ID in token.");

            var result = await _appService.UpdateTaskListAsync(dto, userId);

            var response = new TaskListResponse(result.Id, result.Title);

            return Result<TaskListResponse>.Success(response);
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