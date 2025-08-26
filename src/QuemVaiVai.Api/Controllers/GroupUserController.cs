using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.Contexts;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Domain.Responses;

namespace QuemVaiVai.Api.Controllers;

[Route("api/groupusers")]
[ApiController]
public class GroupUserController : BaseController<GroupUserController>
{
    private readonly IGroupUserAppService _groupUserAppService;
    public GroupUserController(
        IHttpContextAccessor httpContextAccessor,
        ILogger<GroupUserController> logger,
        IMapper mapper,
        IUserContext userContext,
        IGroupUserAppService groupUserAppService) : base(httpContextAccessor, logger, mapper, userContext)
    {
        _groupUserAppService = groupUserAppService;
    }


    [HttpPost("invite")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<Result<bool>> JoinGroup([FromBody] JoinGroupDTO dto)
    {
        ModelStateValidation();

        var userId = _userContext.GetCurrentUserId();

        if (userId == null || userId <= 0)
        {
            throw new UnauthorizedException("Invalid user ID in token.");
        }

        await _groupUserAppService.JoinGroup(dto.InviteCode, (int)userId);
        return Result<bool>.Success(true);
    }


    [HttpPut]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<Result<bool>> ChangeRole([FromBody] GroupUserDTO dto)
    {
        ModelStateValidation();

        var responsibleUserId = _userContext.GetCurrentUserId();

        if (responsibleUserId == null || responsibleUserId <= 0)
        {
            throw new UnauthorizedException("Invalid user ID in token.");
        }

        await _groupUserAppService.ChangeRole(dto.GroupId, dto.UserId, dto.Role, (int)responsibleUserId);
        return Result<bool>.Success(true);
    }


    [HttpDelete("{groupId}/{userId}")]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status500InternalServerError)]
    public async Task<Result<bool>> RemoveUser(int groupId, int userId)
    {
        ModelStateValidation();

        var responsibleUserId = _userContext.GetCurrentUserId();

        if (responsibleUserId == null || responsibleUserId <= 0)
        {
            throw new UnauthorizedException("Invalid user ID in token.");
        }

        await _groupUserAppService.RemoveUserFromGroup(groupId, userId, (int)responsibleUserId);
        return Result<bool>.Success(true);
    }
}