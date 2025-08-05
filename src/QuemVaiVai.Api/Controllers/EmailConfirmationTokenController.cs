using Microsoft.AspNetCore.Mvc;
using QuemVaiVai.Api.Responses;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Application.Services;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Api.Controllers;

[Route("api/email-confirmation")]
[ApiController]
public class EmailConfirmationTokenController : BaseController<EmailConfirmationToken>
{
    private readonly IEmailConfirmationTokenAppService _appService;
    public EmailConfirmationTokenController(
        IHttpContextAccessor httpContextAccessor,
        ILogger<EmailConfirmationToken> logger,
        IEmailConfirmationTokenAppService appService) : base(httpContextAccessor, logger)
    {
        _appService = appService;
    }

    [HttpGet("{token}")]
    [ProducesResponseType(typeof(SuccessResponse<bool>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> AccountConfirmation(string token)
    {
        if (string.IsNullOrEmpty(token))
        {
            ModelState.AddModelError("token", "Token cannot be null or empty.");
            ModelStateValidation();
        }

        await _appService.ConfirmAccount(token);
        return Success(new SuccessResponse<EmailConfirmationToken>(true, null));
    }
}