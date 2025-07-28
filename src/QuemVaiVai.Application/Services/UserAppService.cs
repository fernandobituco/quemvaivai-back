using AutoMapper;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Helpers;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
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
        private readonly IPasswordHasher _passwordHasher;
        private readonly JwtTokenGenerator _jwtGenerator;
        private readonly IMapper _mapper;
        public UserAppService(
            IUserRepository repository,
            IUserDapperRepository dapperRepository,
            IPasswordHasher passwordHasher,
            JwtTokenGenerator jwtGenerator,
            IMapper mapper) : base(repository)
        {
            _dapperRepository = dapperRepository;
            _passwordHasher = passwordHasher;
            _jwtGenerator = jwtGenerator;
            _mapper = mapper;
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO request)
        {
            var exists = await _dapperRepository.ExistsByEmail(request.Email);
            if (exists)
            {
                throw new EmailAlreadyExists();
            }
            User user = _mapper.Map<User>(request);
            var response = _mapper.Map<UserDTO>(await _repository.AddAsync(user));
            return response;
        }

        public async Task<string> LoginAsync(UserLoginDTO request)
        {
            var user = await _dapperRepository.GetByEmail(request.Email);

            if (_passwordHasher.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedException();

            return _jwtGenerator.GenerateToken(user);
        }
    }
}
