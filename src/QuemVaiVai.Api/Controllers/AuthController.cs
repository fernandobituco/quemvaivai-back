using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Domain.Responses;

namespace QuemVaiVai.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : BaseController<AuthController>
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;

    public AuthController(
        IHttpContextAccessor httpContextAccessor,
        ILogger<AuthController> logger,
        IAuthService authService,
        IMapper mapper) : base(httpContextAccessor, logger)
    {
        _authService = authService;
        _mapper = mapper;
    }

    [HttpPost("login")]
    [ProducesResponseType(typeof(Result<LoginResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<LoginResponse>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Result<LoginResponse>), StatusCodes.Status500InternalServerError)]
    public async Task<Result<LoginResponse>> Login([FromBody] UserLoginDTO request)
    {
        try
        {
            var response = await _authService.LoginAsync(request.Email, request.Password);
            SetRefreshTokenCookie(response.RefreshToken, response.RefreshTokenExpiry);
            return Result<LoginResponse>.Success(new LoginResponse(response.AccessToken, response.AccessTokenExpiry));
        }
        catch (UnauthorizedAccessException)
        {
            return Fail<LoginResponse>("Credenciais inválidas.", 401);
        }
    }

    [HttpPost("refresh")]
    public async Task<Result<LoginResponse>> RefreshToken([FromBody] string request)
    {
        var refreshToken = request ?? GetRefreshTokenFromCookie();
        if (string.IsNullOrEmpty(refreshToken))
            return Fail<LoginResponse>("Refresh token is required", 401);

        var response = await _authService.RefreshTokenAsync(refreshToken);
        if (response == null)
            return Fail<LoginResponse>("Invalid or expired refresh token", 401);

        SetRefreshTokenCookie(response.RefreshToken, response.RefreshTokenExpiry);
        return Result<LoginResponse>.Success(new LoginResponse(response.AccessToken, response.AccessTokenExpiry));
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout([FromBody] string request)
    {
        var refreshToken = request ?? GetRefreshTokenFromCookie();
        if (!string.IsNullOrEmpty(refreshToken))
        {
            await _authService.RevokeTokenAsync(refreshToken);
        }

        Response.Cookies.Delete("refreshToken");
        return Ok(new { message = "Logged out successfully" });
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

    private string? GetRefreshTokenFromCookie()
    {
        return Request.Cookies["refreshToken"];
    }
}