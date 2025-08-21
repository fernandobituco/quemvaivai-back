using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Domain.Responses;

namespace QuemVaiVai.Api.Controllers;

public abstract class BaseController<T> : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly ILogger<T> _logger;
    protected readonly IMapper _mapper;
    protected BaseController(IHttpContextAccessor httpContextAccessor, ILogger<T> logger, IMapper mapper)
    {
        _httpContextAccessor = httpContextAccessor;
        _logger = logger;
        _mapper = mapper;
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
    protected Result<TResponse> Success<TResponse>(TResponse result)
    {
        return Result<TResponse>.Success(result);
    }

    protected Result<TResponse> Fail<TResponse>(string errorMessage, int statusCode = 400)
    {
        return Result<TResponse>.Failure(errorMessage, statusCode);
    }

    protected void ModelStateValidation()
    {
        if (!ModelState.IsValid)
        {
            var firstError = ModelState
                .Where(kvp => kvp.Value?.Errors.Count > 0)
                .Select(kvp => new
                {
                    Campo = kvp.Key,
                    Mensagem = kvp.Value?.Errors.First().ErrorMessage
                })
                .FirstOrDefault();

            throw new InvalidModelStateException($"Campo inválido: {firstError?.Campo} - {firstError?.Mensagem}");
        }
    }

    protected string? GetRefreshTokenFromCookie()
    {
        return Request.Cookies["refreshToken"];
    }
}