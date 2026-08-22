
using AutoMapper;
using Microsoft.Extensions.Options;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Application.Interfaces.Email;
using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Application.Interfaces.Security;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Exceptions;
using System.Linq;
using System.Threading.Tasks;

namespace QuemVaiVai.Application.Services
{
    public class PasswordResetTokenAppService : ServiceBase<PasswordResetToken>, IPasswordResetTokenAppService
    {
        private readonly IUserDapperRepository _userDapperRepository;
        private readonly IHasher _hasher;
        private readonly IEmailSender _emailSender;
        private readonly AppSettings _appSettings;
        private readonly IEmailTemplateBuilder _emailTemplateBuilder;
        private readonly IPasswordResetTokenDapperRepository _dapperRepository;
        public PasswordResetTokenAppService(
            IPasswordResetTokenRepository repository,
            IMapper mapper,
            IUserDapperRepository userDapperRepository,
            IHasher hasher,
            IEmailSender emailSender,
            IOptions<AppSettings> appSettings,
            IEmailTemplateBuilder emailTemplateBuilder,
            IPasswordResetTokenDapperRepository dapperRepository) : base(repository, mapper)
        {
            _userDapperRepository = userDapperRepository;
            _hasher = hasher;
            _emailSender = emailSender;
            _appSettings = appSettings.Value;
            _emailTemplateBuilder = emailTemplateBuilder;
            _dapperRepository = dapperRepository;
        }

        public async Task Generate(string email)
        {
            var user = await _userDapperRepository.GetByEmail(email) ?? throw new NotFoundException("Usuário");

            var token = Guid.NewGuid().ToString();
            var tokenHash = _hasher.Hash(token);

            PasswordResetToken passwordResetToken = new()
            {
                UserId = user.Id,
                TokenHash = tokenHash,
                ExpiresAt = DateTime.UtcNow.AddHours(1) // Token válido por 1 hora
            };

            await _repository.AddAsync(passwordResetToken, user.Id);

            var url = $"{_appSettings.FRONT_END_URL}/password-recovery?token={token}&userId={user.Id}";

            var body = await _emailTemplateBuilder.BuildTemplateAsync("PasswordRecovery", new Dictionary<string, string>
            {
                ["Name"] = user.Name,
                ["ConfirmationUrl"] = url,
            });

            await _emailSender.SendEmailAsync(email, "Password Recovery", body);
        }

        public async Task Validate(string token, int userId)
        {
            var passwordResetToken = await _dapperRepository.GetLastByUserId(userId) ?? throw new InvalidTokenException("PasswordResetToken");

            if (!_hasher.Verify(token, passwordResetToken.TokenHash))
                throw new InvalidTokenException();

            if (passwordResetToken.Used)
                throw new UsedTokenException();

            await Invalidate(passwordResetToken);
        }

        private async Task Invalidate(PasswordResetToken token)
        {
            token.Used = true;

            await _repository.UpdateAsync(token);
        }
    }
}
