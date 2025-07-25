using Microsoft.AspNetCore.Mvc;
using QuemVaiVai.Api.Responses;

namespace QuemVaiVai.Api.Controllers;

public abstract class BaseController<T> : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILogger<T> _logger;
    protected BaseController(IHttpContextAccessor httpContextAccessor, ILogger<T> logger)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
    }

    protected Guid GetCurrentUserId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);
        if (userIdClaim == null)
        {
            throw new System.Exception("User is not authenticated.");
        }
        
        return Guid.Parse(userIdClaim.Value);
    }
    protected IActionResult Success<TResponse>(TResponse result)
    {
        return Ok(new SuccessResponse<TResponse>(true, result ));
    }

    protected IActionResult Fail(string errorMessage, int statusCode = 400)
    {
        _logger.LogWarning("Erro na requisição: {Message}", errorMessage);
        return StatusCode(statusCode, new ErrorResponse(false, errorMessage));
    }

    protected IActionResult ExceptionResponse(Exception ex)
    {
        _logger.LogError(ex, "Erro interno no servidor.");
        return StatusCode(500, new ErrorResponse(false, ex.Message));
    }
}