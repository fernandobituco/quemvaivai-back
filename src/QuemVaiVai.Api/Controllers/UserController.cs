
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Domain.Responses;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace QuemVaiVai.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : BaseController<UserController>
{
    private readonly IUserAppService _userAppService;
    private readonly IMapper _mapper;
    public UserController(
        IHttpContextAccessor httpContextAccessor,
        ILogger<UserController> logger,
        IUserAppService userAppService,
        IMapper mapper) : base(httpContextAccessor, logger)
    {
        _userAppService = userAppService;
        _mapper = mapper;
    }

    [HttpPost]
    [ProducesResponseType(typeof(Result<CreatedUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<CreatedUserResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<CreatedUserResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<Result<CreatedUserResponse>> CreateUser([FromBody] CreateUserDTO dto)
    {
        ModelStateValidation();

        var createdUser = await _userAppService.CreateUserAsync(dto);
        var response = _mapper.Map<CreatedUserResponse>(createdUser);
        return Result<CreatedUserResponse>.Success(response);
    }

    [HttpGet("profile")]
    [ProducesResponseType(typeof(Result<UserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<UserResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<UserResponse>), StatusCodes.Status500InternalServerError)]
    public Result<UserResponse> GetUserProfile()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int userIdInt))
            throw new UnauthorizedException("Invalid user ID in token.");
        var userEmail = User.FindFirstValue(ClaimTypes.Email);
        if (string.IsNullOrEmpty(userEmail))
            throw new UnauthorizedException("Invalid email in token.");
        var userName = User.FindFirstValue(ClaimTypes.Name);
        if (string.IsNullOrEmpty(userName))
            throw new UnauthorizedException("Invalid name in token.");
        //var tokenId = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

        UserResponse response = new(userIdInt, userName, userEmail);
        return Result<UserResponse>.Success(response);
    }
}
