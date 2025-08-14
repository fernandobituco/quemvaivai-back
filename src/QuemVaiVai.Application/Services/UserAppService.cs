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
            if (request == null) throw new ArgumentNullException(nameof(request));
            // Validações
            await ValidateEmail(request.Email);

            ValidatePassword(request.Password, request.PasswordConfirmation);

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

        public async Task<UserDTO> UpdateUserAsync(UpdateUserDTO request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            var user = await _dapperRepository.GetCompleteForUpdateById(request.Id) ?? throw new NotFoundException("Usuário");

            // Validações
            await ValidateEmail(request.Email, request.Id);

            user.Name = request.Name;
            user.Email = request.Email;

            await _repository.UpdateAsync(user, request.Id);

            return _mapper.Map<UserDTO>(user);
        }

        public async Task DeleteUserAsync(int id)
        {
            await _repository.DeleteAsync(id, id);
        }

        private async Task ValidateEmail(string email, int? id = null)
        {
            if (!IsValidEmail(email))
                throw new ArgumentException("Email inválido", nameof(email));

            await ValidateEmailNotExistsAsync(email, id);
        }

        private void ValidatePassword(string password, string passwordConfirfmation)
        {
            ValidatePasswordMatch(password, passwordConfirfmation);
            _userService.ValidatePassword(password);
        }

        private static void ValidatePasswordMatch(string password, string passwordConfirmation)
        {
            if (!password.Equals(passwordConfirmation, StringComparison.Ordinal))
            {
                throw new InvalidPasswordException("A confirmação precisa estar igual à senha");
            }
        }

        private async Task ValidateEmailNotExistsAsync(string email, int? id = null)
        {
            if (id == null)
            {
                var exists = await _dapperRepository.ExistsByEmail(email);
                if (exists)
                {
                    throw new EmailAlreadyExistsException($"O email {email} já está em uso");
                }
            }
            else
            {
                var exists = await _dapperRepository.ExistsByEmailDiferentId(email, (int)id);
                if (exists)
                {
                    throw new EmailAlreadyExistsException($"O email {email} já está em uso");
                }
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
