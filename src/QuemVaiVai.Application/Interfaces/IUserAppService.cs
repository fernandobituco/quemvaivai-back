using QuemVaiVai.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Application.Interfaces
{
    public interface IUserAppService
    {
        Task<UserDTO> CreateUserAsync(CreateUserDTO request);
        Task<string> LoginAsync(UserLoginDTO request);
    }

}
