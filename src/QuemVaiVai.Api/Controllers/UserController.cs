
using Microsoft.AspNetCore.Mvc;
using QuemVaiVai.Api.Responses;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.Services;
using System.Threading.Tasks;

namespace QuemVaiVai.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : BaseController<UserController>
{
    private readonly IUserAppService _userAppService;
    public UserController(
        IHttpContextAccessor httpContextAccessor,
        ILogger<UserController> logger,
        IUserAppService userAppService) : base(httpContextAccessor, logger)
    {
        _userAppService = userAppService;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(SuccessResponse<LoginResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Login([FromBody] UserLoginDTO dto)
    {
        ModelStateValidation();

        var token = await _userAppService.LoginAsync(dto);

        if (!string.IsNullOrWhiteSpace(token))
        {
            return Success(new LoginResponse(token));
        }

        return Fail("Credenciais inválidas.", 401);
    }

    [HttpPost]
    [ProducesResponseType(typeof(SuccessResponse<CreatedUserResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO dto)
    {
        ModelStateValidation();

        var createdUser = await _userAppService.CreateUserAsync(dto);
        return Success(new CreatedUserResponse(createdUser.Id, createdUser.Name, createdUser.Email, createdUser.CreatedAt));
    }
}
