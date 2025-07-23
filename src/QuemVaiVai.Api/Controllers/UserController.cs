
using Microsoft.AspNetCore.Mvc;
using QuemVaiVai.Application.DTOs;

namespace QuemVaiVai.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : BaseController<UserController>
{
    public UserController(IHttpContextAccessor httpContextAccessor, ILogger<UserController> logger) : base(httpContextAccessor, logger)
    {
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return Fail("Dados inválidos.");

            // Simulação de login (buscar no banco de verdade)
            if (dto.Email == "teste@exemplo.com" && dto.Password == "123456")
            {
                var token = "fake-jwt-token";
                return Success(new { Token = token });
            }

            return Fail("Credenciais inválidas.", 401);
        }
        catch (Exception ex)
        {
            return ExceptionResponse(ex);
        }
    }

    [HttpPost("create")]
    public IActionResult CreateUser([FromBody] CreateUserDTO dto)
    {
        try
        {
            if (!ModelState.IsValid)
                return Fail("Dados inválidos.");

            // Simulação de criação (salvar no banco real aqui)
            var createdUser = new
            {
                Id = Guid.NewGuid(),
                dto.Name,
                dto.Email
            };

            return Success(createdUser);
        }
        catch (Exception ex)
        {
            return ExceptionResponse(ex);
        }
    }
}
