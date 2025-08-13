using AutoMapper;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Application.Interfaces.Email;
using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Application.Interfaces.Security;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Domain.Interfaces.Services;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Exceptions;
using Microsoft.Extensions.Options;
using QuemVaiVai.Domain.Responses;

namespace QuemVaiVai.Application.Services
{
    public class UserAppService : ServiceBase<User>, IUserAppService
    {
        private readonly IUserDapperRepository _dapperRepository;
        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IEmailTemplateBuilder _emailTemplateBuilder;
        private readonly IEmailConfirmationTokenService _emailConfirmationTokenService;
        private readonly IEmailConfirmationTokenRepository _emailConfirmationTokenRepository;
        private readonly AppSettings _appSettings;

        public UserAppService(
            IUserRepository repository,
            IUserDapperRepository dapperRepository,
            IPasswordHasher passwordHasher,
            ITokenGenerator tokenGenerator,
            IMapper mapper,
            IUserService userService,
            IEmailSender emailSender,
            IEmailTemplateBuilder emailTemplateBuilder,
            IEmailConfirmationTokenService emailConfirmationTokenService,
            IEmailConfirmationTokenRepository emailConfirmationTokenRepository,
            IOptions<AppSettings> appSettings) : base(repository)
        {
            _dapperRepository = dapperRepository;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
            _mapper = mapper;
            _userService = userService;
            _emailSender = emailSender;
            _emailTemplateBuilder = emailTemplateBuilder;
            _emailConfirmationTokenService = emailConfirmationTokenService;
            _emailConfirmationTokenRepository = emailConfirmationTokenRepository;
            _appSettings = appSettings.Value;
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO request)
        {
            // Validações
            await ValidateUserCreationAsync(request);

            User user = _mapper.Map<User>(request);

            user.PasswordHash = _passwordHasher.Hash(request.Password);

            var userCreated = await CreateAsync(user);
            var response = new UserDTO();

            if (userCreated != null)
            {
                var token = _emailConfirmationTokenService.GenerateToken(userCreated.Id);
                await _emailConfirmationTokenRepository.AddAsync(token);
                var url = $"{_appSettings.FRONT_END_URL}/account-confirmation?token={token.Token}";

                var body = await _emailTemplateBuilder.BuildTemplateAsync("AccountConfirmation", new Dictionary<string, string>
                {
                    ["Name"] = request.Name,
                    ["ConfirmationUrl"] = url,
                });

                try
                {
                    await _emailSender.SendEmailAsync(request.Email, "AccountConfirmation", body);
                }
                finally
                {
                    response = _mapper.Map<UserDTO>(userCreated);
                }
            }
            return response;
        }

        private async Task ValidateUserCreationAsync(CreateUserDTO request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (string.IsNullOrWhiteSpace(request.Email))
                throw new ArgumentException("Email é obrigatório", nameof(request.Email));

            if (!IsValidEmail(request.Email))
                throw new ArgumentException("Email inválido", nameof(request.Email));

            ValidatePasswordMatch(request.Password, request.PasswordConfirmation);
            _userService.ValidatePassword(request.Password);
            await ValidateEmailNotExistsAsync(request.Email);
        }
        private static void ValidatePasswordMatch(string password, string passwordConfirmation)
        {
            if (!password.Equals(passwordConfirmation, StringComparison.Ordinal))
            {
                throw new InvalidPasswordException("A confirmação precisa estar igual à senha");
            }
        }
        private async Task ValidateEmailNotExistsAsync(string email)
        {
            var exists = await _dapperRepository.ExistsByEmail(email);
            if (exists)
            {
                throw new EmailAlreadyExistsException($"O email {email} já está em uso");
            }
        }
        private static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
