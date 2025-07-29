using AutoMapper;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Helpers;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Application.Interfaces.Email;
using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Application.Interfaces.Security;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Domain.Interfaces.Services;

namespace QuemVaiVai.Application.Services
{
    public class UserAppService : ServiceBase<User>, IUserAppService
    {
        private readonly IUserDapperRepository _dapperRepository;
        private readonly IUserService _userService;
        private readonly IPasswordHasher _passwordHasher;
        private readonly JwtTokenGenerator _jwtGenerator;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IEmailTemplateBuilder _emailTemplateBuilder;

        public UserAppService(
            IUserRepository repository,
            IUserDapperRepository dapperRepository,
            IPasswordHasher passwordHasher,
            JwtTokenGenerator jwtGenerator,
            IMapper mapper,
            IUserService userService,
            IEmailSender emailSender,
            IEmailTemplateBuilder emailTemplateBuilder) : base(repository)
        {
            _dapperRepository = dapperRepository;
            _passwordHasher = passwordHasher;
            _jwtGenerator = jwtGenerator;
            _mapper = mapper;
            _userService = userService;
            _emailSender = emailSender;
            _emailTemplateBuilder = emailTemplateBuilder;
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO request)
        {
            // Validações
            await ValidateUserCreationAsync(request);

            User user = _mapper.Map<User>(request);

            user.PasswordHash = _passwordHasher.Hash(request.Password);

            var response = _mapper.Map<UserDTO>(await _repository.AddAsync(user));

            if (response != null)
            {
                var body = await _emailTemplateBuilder.BuildTemplateAsync("AccountConfirmation", new Dictionary<string, string>
                {
                    ["Name"] = request.Name,
                });

                await _emailSender.SendEmailAsync(request.Email, "AccountConfirmation", body);
            }
            return response;
        }

        public async Task<string> LoginAsync(UserLoginDTO request)
        {
            var user = await _dapperRepository.GetByEmail(request.Email);

            if (_passwordHasher.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedException();

            return _jwtGenerator.GenerateToken(user);
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
