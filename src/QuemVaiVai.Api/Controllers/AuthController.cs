using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Domain.Responses;

namespace QuemVaiVai.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : BaseController<AuthController>
{
    private readonly IAuthService _authService;

    public AuthController(
        IHttpContextAccessor httpContextAccessor,
        ILogger<AuthController> logger,
        IAuthService authService,
        IMapper mapper) : base(httpContextAccessor, logger, mapper)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(Result<LoginResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<LoginResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<LoginResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<Result<LoginResponse>> Login([FromBody] UserLoginDTO request)
    {
        var response = await _authService.LoginAsync(request.Email, request.Password);
        SetRefreshTokenCookie(response.RefreshToken, response.RefreshTokenExpiry);
        return Result<LoginResponse>.Success(new LoginResponse(response.AccessToken, response.AccessTokenExpiry));
    }

    [HttpPost("refresh")]
    public async Task<Result<LoginResponse>> RefreshToken([FromBody] string request)
    {
        var refreshToken = request ?? GetRefreshTokenFromCookie();
        if (string.IsNullOrEmpty(refreshToken))
            throw new InvalidTokenException();

        var response = await _authService.RefreshTokenAsync(refreshToken) ?? throw new InvalidTokenException();

        SetRefreshTokenCookie(response.RefreshToken, response.RefreshTokenExpiry);

        return Result<LoginResponse>.Success(new LoginResponse(response.AccessToken, response.AccessTokenExpiry));
    }

    [HttpPost("force-refresh")]
    public async Task<Result<LoginResponse>> ForceRefreshToken()
    {
        var refreshToken = GetRefreshTokenFromCookie();
        if (string.IsNullOrEmpty(refreshToken))
            throw new InvalidTokenException();

        var response = await _authService.RefreshTokenAsync(refreshToken) ?? throw new InvalidTokenException();
        SetRefreshTokenCookie(response.RefreshToken, response.RefreshTokenExpiry);
        return Result<LoginResponse>.Success(new LoginResponse(response.AccessToken, response.AccessTokenExpiry));
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<Result<bool>> Logout()
    {
        var refreshToken = GetRefreshTokenFromCookie();
        if (!string.IsNullOrEmpty(refreshToken))
        {
            await _authService.RevokeTokenAsync(refreshToken);
        }

        Response.Cookies.Delete("refreshToken");
        return Result<bool>.Success(true);
    }

    [HttpGet("test")]
    [HttpHead("test")]
    [AllowAnonymous]
    public ActionResult<bool> Test()
    {
        return new ActionResult<bool>(true);
    }

    private void SetRefreshTokenCookie(string refreshToken, DateTime expiry)
    {
        Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.None,
            Expires = expiry,
            Path = "/",
        });
    }
}