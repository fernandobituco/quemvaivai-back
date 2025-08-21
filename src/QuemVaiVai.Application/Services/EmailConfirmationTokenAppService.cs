using AutoMapper;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Domain.Interfaces.Services;

namespace QuemVaiVai.Application.Services
{
    public class EmailConfirmationTokenAppService : ServiceBase<EmailConfirmationToken>, IEmailConfirmationTokenAppService
    {
        private readonly IEmailConfirmationTokenDapperRepository _dapperRepository;
        private readonly IEmailConfirmationTokenService _service;
        private readonly IUserRepository _userRepository;
        public EmailConfirmationTokenAppService(
            IEmailConfirmationTokenRepository repository,
            IMapper mapper,
            IEmailConfirmationTokenDapperRepository dapperRepository,
            IUserRepository userRepository,
            IEmailConfirmationTokenService service) : base(repository, mapper)
        {
            _dapperRepository = dapperRepository;
            _userRepository = userRepository;
            _service = service;
        }

        public async Task ConfirmAccount(string token)
        {
            var emailConfirmationToken = await _dapperRepository.GetByToken(token) ?? throw new NotFoundException("Token");
            
            _service.ValidateToken(emailConfirmationToken);

            await ConfirmUserAccount(emailConfirmationToken);

            emailConfirmationToken.Used = true;

            await _repository.UpdateAsync(emailConfirmationToken);
        }

        private async Task ConfirmUserAccount(EmailConfirmationToken emailConfirmationToken)
        {
            var user = await _userRepository.GetByIdAsync(emailConfirmationToken.UserId) ?? throw new NotFoundException("Usuário");
            user.Confirmed = true;
            await _userRepository.UpdateAsync(user);
        }
    }
}
