using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Application.Interfaces.Services
{
    public interface IUserAppService : IService<User>
    {
        Task<UserDTO> CreateUserAsync(CreateUserDTO request);
        Task<string> LoginAsync(UserLoginDTO request);
    }
}
