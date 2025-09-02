using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.Contexts;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Application.Services;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Domain.Responses;
using System.Security.Claims;

namespace QuemVaiVai.Api.Controllers
{
    [Route("api/events")]
    [ApiController]
    public class EventController : BaseController<EventController>
    {
        private readonly IEventAppService _eventAppService;
        private readonly IEventDapperRepository _eventDapperRepository;
        public EventController(
            IHttpContextAccessor httpContextAccessor,
            ILogger<EventController> logger,
            IMapper mapper,
            IUserContext userContext,
            IEventAppService eventAppService,
            IEventDapperRepository eventDapperRepository) : base(httpContextAccessor, logger, mapper, userContext)
        {
            _eventAppService = eventAppService;
            _eventDapperRepository = eventDapperRepository;
        }


        [HttpPost]
        [ProducesResponseType(typeof(Result<EventCardResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<EventCardResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<EventCardResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<EventCardResponse>> CreateEvent([FromBody] CreateEventDTO dto)
        {
            ModelStateValidation();

            var userId = _userContext.GetCurrentUserId() ?? throw new UnauthorizedException("Invalid user ID in token.");

            var createdEvent = await _eventAppService.CreateEventAsync(dto, userId);
            var response = new EventCardResponse(
                createdEvent.Id, 
                createdEvent.Title, 
                createdEvent.Description, 
                createdEvent.Location,
                createdEvent.EventDate, 
                createdEvent.InviteCode,
                createdEvent.GroupName, 
                createdEvent.GroupId,
                0, 
                1,
                true
            );
            return Result<EventCardResponse>.Success(response);
        }

        [HttpGet("user")]
        [ProducesResponseType(typeof(Result<List<EventCardResponse>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<List<EventCardResponse>>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<List<EventCardResponse>>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<List<EventCardResponse>>> GetEventsByUser()
        {
            ModelStateValidation();

            var userId = _userContext.GetCurrentUserId() ?? throw new UnauthorizedException("Invalid user ID in token.");

            List<EventCardDTO> events = await _eventDapperRepository.GetAllByUserId(userId);

            List<EventCardResponse> response = events.Select(e => new EventCardResponse(
                e.Id,
                e.Title,
                e.Description,
                e.Location,
                e.EventDate,
                e.InviteCode,
                e.GroupName,
                e.GroupId,
                e.Interested,
                e.Going,
                e.CanEdit,
                e.ActiveVote,
                false
            ))
            .ToList();

            return Result<List<EventCardResponse>>.Success(response);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Result<EventCardResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<EventCardResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<EventCardResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<EventCardResponse>> GetGroupById(int id)
        {
            var userId = _userContext.GetCurrentUserId() ?? throw new UnauthorizedException("Invalid user ID in token.");

            var result = await _eventAppService.GetById(id, userId);

            EventCardResponse response = new(
                result.Id,
                result.Title,
                result.Description,
                result.Location,
                result.EventDate,
                result.InviteCode,
                result.GroupName,
                result.GroupId,
                result.Interested,
                result.Going,
                false,
                result.ActiveVote
            );

            return Result<EventCardResponse>.Success(response);
        }

        [HttpGet("invitecode/{inviteCode}")]
        [ProducesResponseType(typeof(Result<EventCardResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<EventCardResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<EventCardResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<EventCardResponse>> GetGroupByInviteCode(Guid inviteCode)
        {
            var userId = _userContext.GetCurrentUserId() ?? throw new UnauthorizedException("Invalid user ID in token.");

            var result = await _eventAppService.GetByInviteCode(inviteCode);

            EventCardResponse response = new(
                result.Id,
                result.Title,
                result.Description,
                result.Location,
                result.EventDate,
                result.InviteCode,
                result.GroupName,
                result.GroupId,
                result.Interested,
                result.Going,
                false,
                result.ActiveVote
            );

            return Result<EventCardResponse>.Success(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Result<EventCardResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<EventCardResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<EventCardResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<EventCardResponse>> UpdateGroup([FromBody] UpdateEventDTO dto)
        {
            ModelStateValidation();

            var userId = _userContext.GetCurrentUserId() ?? throw new UnauthorizedException("Invalid user ID in token.");

            var result = await _eventAppService.UpdateEventAsync(dto, userId);

            EventCardResponse response = new(
                result.Id,
                result.Title,
                result.Description,
                result.Location,
                result.EventDate,
                result.InviteCode,
                result.GroupName,
                result.GroupId,
                result.Interested,
                result.Going,
                true,
                result.ActiveVote
            );
            return Result<EventCardResponse>.Success(response);
        }
    }
}