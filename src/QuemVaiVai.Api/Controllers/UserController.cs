
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.Contexts;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Domain.Responses;
using System.Security.Claims;

namespace QuemVaiVai.Api.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : BaseController<UserController>
    {
        private readonly IUserAppService _userAppService;
        private readonly IAuthService _authService;
        private readonly IUserDapperRepository _dapperRepository;
        public UserController(
            IHttpContextAccessor httpContextAccessor,
            ILogger<UserController> logger,
            IUserAppService userAppService,
            IMapper mapper,
            IUserContext userContext,
            IAuthService authService,
            IUserDapperRepository dapperRepository) : base(httpContextAccessor, logger, mapper, userContext)
        {
            _userAppService = userAppService;
            _authService = authService;
            _dapperRepository = dapperRepository;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Result<CreatedUserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<CreatedUserResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<CreatedUserResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<CreatedUserResponse>> CreateUser([FromBody] CreateUserDTO dto)
        {
            ModelStateValidation();

            var createdUser = await _userAppService.CreateUserAsync(dto);
            CreatedUserResponse response = new(createdUser.Id, createdUser.Name, createdUser.Email);
            return Result<CreatedUserResponse>.Success(response);
        }

        [HttpGet("profile")]
        [ProducesResponseType(typeof(Result<UserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<UserResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<UserResponse>), StatusCodes.Status500InternalServerError)]
        public Result<UserResponse> GetUserProfile()
        {
            var userId = _userContext.GetCurrentUserId() ?? throw new UnauthorizedException("Invalid user ID in token.");

            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(userEmail))
                throw new UnauthorizedException("Invalid email in token.");
            var userName = User.FindFirstValue(ClaimTypes.Name);
            if (string.IsNullOrEmpty(userName))
                throw new UnauthorizedException("Invalid name in token.");
            //var tokenId = User.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;

            UserResponse response = new(userId, userName, userEmail);
            return Result<UserResponse>.Success(response);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Result<CreatedUserResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<CreatedUserResponse>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<CreatedUserResponse>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<CreatedUserResponse>> UpdateUser([FromBody] UpdateUserDTO dto)
        {
            ModelStateValidation();

            var userId = _userContext.GetCurrentUserId() ?? throw new UnauthorizedException("Invalid user ID in token.");

            if (userId != dto.Id)
            {
                throw new UnauthorizedException("Um usuário só pode alterar a própria conta");
            }

            var user = await _userAppService.UpdateUserAsync(dto);
            CreatedUserResponse response = new(user.Id, user.Name, user.Email);
            return Result<CreatedUserResponse>.Success(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<bool>> DeleteUser(int id)
        {
            var userId = _userContext.GetCurrentUserId() ?? throw new UnauthorizedException("Invalid user ID in token.");

            if (userId != id)
            {
                throw new UnauthorizedException("Um usuário só pode alterar a própria conta");
            }

            await _userAppService.DeleteUserAsync(id);

            var refreshToken = GetRefreshTokenFromCookie();
            if (!string.IsNullOrEmpty(refreshToken))
            {
                await _authService.RevokeTokenAsync(refreshToken);
            }

            Response.Cookies.Delete("refreshToken");

            return Result<bool>.Success(true);
        }
    }
}
