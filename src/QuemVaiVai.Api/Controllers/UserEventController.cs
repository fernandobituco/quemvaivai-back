using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuemVaiVai.Application.Interfaces.Contexts;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Domain.Responses;
using QuemVaiVai.Infrastructure.DapperRepositories;

namespace QuemVaiVai.Api.Controllers
{
    [Route("api/userevents")]
    [ApiController]
    public class UserEventController : BaseController<UserEvent>
    {
        private readonly IUserEventDapperRepository _userEventDapperRepository;
        public UserEventController(
            IHttpContextAccessor httpContextAccessor,
            ILogger<UserEvent> logger,
            IMapper mapper,
            IUserContext userContext,
            IUserEventDapperRepository userEventDapperRepository) : base(httpContextAccessor, logger, mapper, userContext)
        {
            _userEventDapperRepository = userEventDapperRepository;
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
    }
}