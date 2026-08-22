using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.Contexts;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Domain.Responses;

namespace QuemVaiVai.Api.Controllers
{
    [Route("api/passwordresettoken")]
    [ApiController]
    public class PasswordResetTokenController : BaseController<PasswordResetTokenController>
    {
        private readonly IPasswordResetTokenDapperRepository _dapperRepository;
        private readonly IPasswordResetTokenAppService _appService;
        private readonly IUserAppService _userAppService;
        public PasswordResetTokenController(
            IHttpContextAccessor httpContextAccessor,
            ILogger<PasswordResetTokenController> logger,
            IMapper mapper,
            IUserContext userContext,
            IPasswordResetTokenDapperRepository dapperRepository,
            IPasswordResetTokenAppService appService,
            IUserAppService userAppService) : base(httpContextAccessor, logger, mapper, userContext)
        {
            _dapperRepository = dapperRepository;
            _appService = appService;
            _userAppService = userAppService;
        }

        [HttpGet("{email}")]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Result<bool>), StatusCodes.Status500InternalServerError)]
        public async Task<Result<bool>> GenerateToken(string email)
        {
            ModelStateValidation();

            await _appService.Generate(email);

            return Result<bool>.Success(true);
        }

        [HttpPost]
        public async Task<Result<bool>> ResetPassword([FromBody] PasswordResetDTO dto)
        {
            ModelStateValidation();

            await _appService.Validate(dto.PasswordResetToken, dto.UserId);

            await _userAppService.UpdatePassword(dto.UserId, dto.Password, dto.PasswordResetToken);

            return Result<bool>.Success(true);
        }
    }
}