using Microsoft.AspNetCore.Mvc;
using QuemVaiVai.Domain.Responses;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Application.Services;
using QuemVaiVai.Domain.Entities;
using AutoMapper;
using QuemVaiVai.Application.Interfaces.Contexts;

namespace QuemVaiVai.Api.Controllers
{
    [Route("api/email-confirmation")]
    [ApiController]
    public class EmailConfirmationTokenController : BaseController<EmailConfirmationToken>
    {
        private readonly IEmailConfirmationTokenAppService _appService;
        public EmailConfirmationTokenController(
            IHttpContextAccessor httpContextAccessor,
            ILogger<EmailConfirmationToken> logger,
            IMapper mapper,
            IUserContext userContext,
            IEmailConfirmationTokenAppService appService) : base(httpContextAccessor, logger, mapper, userContext)
        {
            _appService = appService;
        }

        [HttpGet("{token}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<bool>> AccountConfirmation(string token)
        {
            if (string.IsNullOrEmpty(token))
            {
                ModelState.AddModelError("token", "Token cannot be null or empty.");
                ModelStateValidation();
            }

            await _appService.ConfirmAccount(token);
            return Result<bool>.Success(true);
        }
    }
}