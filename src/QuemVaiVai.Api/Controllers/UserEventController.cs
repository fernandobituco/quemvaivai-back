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
    [Route("api/userevents")]
    [ApiController]
    public class UserEventController : BaseController<UserEvent>
    {
        private readonly IUserEventDapperRepository _userEventDapperRepository;
        private readonly IUserEventAppService _userEventAppService;
        public UserEventController(
            IHttpContextAccessor httpContextAccessor,
            ILogger<UserEvent> logger,
            IMapper mapper,
            IUserContext userContext,
            IUserEventDapperRepository userEventDapperRepository,
            IUserEventAppService userEventAppService) : base(httpContextAccessor, logger, mapper, userContext)
        {
            _userEventDapperRepository = userEventDapperRepository;
            _userEventAppService = userEventAppService;
        }

        [HttpGet("event/{eventId}")]
        [ProducesResponseType(typeof(Result<List<UserMemberResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<List<UserMemberResponse>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<List<UserMemberResponse>>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<List<UserMemberResponse>>> GetAllByUserId(int eventId)
        {
            var userId = _userContext.GetCurrentUserId() ?? throw new UnauthorizedException("Invalid user ID in token.");

            var result = await _userEventDapperRepository.GetAllByEventId(eventId);

            List<UserMemberResponse> response = result.Select(u => new UserMemberResponse(
                u.Id,
                u.Name,
                u.Role,
                u.Status
            ))
            .ToList();

            return Result<List<UserMemberResponse>>.Success(response);
        }

        [HttpPost("invite")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<bool>> JoinGroup([FromBody] JoinEventDTO dto)
        {
            ModelStateValidation();

            var userId = _userContext.GetCurrentUserId() ?? throw new UnauthorizedException("Invalid user ID in token.");

            await _userEventAppService.JoinEvent(dto.InviteCode, userId, dto.Status);
            return Result<bool>.Success(true);
        }

        [HttpDelete("{eventId}/{userId}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<bool>> RemoveUser(int eventId, int userId)
        {
            ModelStateValidation();

            var responsibleUserId = _userContext.GetCurrentUserId() ?? throw new UnauthorizedException("Invalid user ID in token.");

            await _userEventAppService.RemoveUserFromEvent(eventId, userId, responsibleUserId);
            return Result<bool>.Success(true);
        }

        [HttpPut("change-role")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<bool>> ChangeRole([FromBody] UserEventDTO dto)
        {
            ModelStateValidation();

            var responsibleUserId = _userContext.GetCurrentUserId() ?? throw new UnauthorizedException("Invalid user ID in token.");

            await _userEventAppService.ChangeRole(dto.EventId, dto.UserId, dto.Role, responsibleUserId);
            return Result<bool>.Success(true);
        }

        [HttpPut("change-status")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<bool>> ChangeStatus([FromBody] UserEvent dto)
        {
            ModelStateValidation();

            var userId = _userContext.GetCurrentUserId() ?? throw new UnauthorizedException("Invalid user ID in token.");

            if (userId != dto.UserId)
            {
                throw new UnauthorizedException();
            }

            await _userEventAppService.ChangeStatus(dto.EventId, dto.UserId, dto.Status);
            return Result<bool>.Success(true);
        }
    }
}