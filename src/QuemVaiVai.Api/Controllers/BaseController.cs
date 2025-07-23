using Microsoft.AspNetCore.Mvc;

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
    protected IActionResult Success(object? result = null)
    {
        return Ok(new { success = true, data = result });
    }

    protected IActionResult Fail(string errorMessage, int statusCode = 400)
    {
        _logger.LogWarning("Erro na requisição: {Message}", errorMessage);
        return StatusCode(statusCode, new { success = false, error = errorMessage });
    }

    protected IActionResult ExceptionResponse(Exception ex)
    {
        _logger.LogError(ex, "Erro interno no servidor.");
        return StatusCode(500, new { success = false, error = "Erro interno no servidor." });
    }
}