
using Microsoft.AspNetCore.Mvc;
using QuemVaiVai.Domain.Responses;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.Services;
using System.Threading.Tasks;
using AutoMapper;

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
}
