using AutoMapper;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserAppService(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<UserDTO> CreateUserAsync(CreateUserDTO request)
        {
            var user = _mapper.Map<User>(request);
            await _userService.CreateAsync(user);
            return _mapper.Map<UserDTO>(user);
        }

        public async Task<string> LoginAsync(UserLoginDTO request)
        {
            var user = await _userService.AuthenticateAsync(request.Email, request.Password);
            return user != null ? "TOKEN" : throw new UnauthorizedAccessException();
        }
    }
}
