using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Interfaces.Services
{
    public interface IUserAppService : IService<User>
    {
        Task<UserDTO> CreateUserAsync(CreateUserDTO request);
        Task<string> LoginAsync(UserLoginDTO request);
    }
}
