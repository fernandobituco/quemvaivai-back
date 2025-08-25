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

namespace QuemVaiVai.Api.Controllers;

[Route("api/groups")]
[ApiController]
public class GroupController : BaseController<GroupController>
{
    private readonly IGroupAppService _groupAppService;
    private readonly IGroupDapperRepository _dapperRepository;
    private readonly IGroupUserDapperRepository _groupUserDapperRepository;
    public GroupController(
        IHttpContextAccessor httpContextAccessor,
        ILogger<GroupController> logger,
        IMapper mapper,
        IUserContext userContext,
        IGroupAppService groupAppService,
        IGroupDapperRepository dapperRepository,
        IGroupUserDapperRepository groupUserDapperRepository) : base(httpContextAccessor, logger, mapper, userContext)
    {
        _groupAppService = groupAppService;
        _dapperRepository = dapperRepository;
        _groupUserDapperRepository = groupUserDapperRepository;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<GroupResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<GroupResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<GroupResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<Result<GroupResponse>> CreateGroup([FromBody] CreateGroupDTO dto)
    {
        ModelStateValidation();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int userIdInt))
            throw new UnauthorizedException("Invalid user ID in token.");

        var createdGroup = await _groupAppService.CreateGroupAsync(dto, userIdInt);
        var response = new GroupResponse(createdGroup.Id, createdGroup.Name, createdGroup.Description, createdGroup.InviteCode);
        return Result<GroupResponse>.Success(response);
    }

    [HttpGet("user")]
    [ProducesResponseType(typeof(Result<List<GroupCardResponse>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<List<GroupCardResponse>>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<List<GroupCardResponse>>), StatusCodes.Status500InternalServerError)]
    public async Task<Result<List<GroupCardResponse>>> GetAllByUserId()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int userIdInt))
            throw new UnauthorizedException("Invalid user ID in token.");

        var result = await _dapperRepository.GetAllByUserId(userIdInt);

        List<GroupCardResponse> response = result.Select(g => new GroupCardResponse(
            g.Id,
            g.Name,
            g.Description,
            g.MemberCount,
            g.EventCount,
            g.NextEvent,
            g.CanEdit,
            g.InviteCode
        ))
        .ToList();


        return Result<List<GroupCardResponse>>.Success(response);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Result<GroupResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<GroupResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<GroupResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<Result<GroupResponse>> GetGroupById(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int userIdInt))
            throw new UnauthorizedException("Invalid user ID in token.");

        var result = await _groupAppService.GetById(id, userIdInt);

        GroupResponse response = new(
            result.Id,
            result.Name,
            result.Description
        );


        return Result<GroupResponse>.Success(response);
    }

    [HttpPut]
    [ProducesResponseType(typeof(Result<GroupResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<GroupResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<GroupResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<Result<GroupResponse>> UpdateGroup([FromBody] UpdateGroupDTO dto)
    {
        ModelStateValidation();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int userIdInt))
            throw new UnauthorizedException("Invalid user ID in token.");

        var group = await _groupAppService.UpdateGroupAsync(dto, userIdInt);
        var response = new GroupResponse(group.Id, group.Name, group.Description);
        return Result<GroupResponse>.Success(response);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<Result<bool>> DeleteGroup(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int userIdInt))
            throw new UnauthorizedException("Invalid user ID in token.");

        await _groupAppService.DeleteGroupAsync(id, userIdInt);

        return Result<bool>.Success(true);
    }
}