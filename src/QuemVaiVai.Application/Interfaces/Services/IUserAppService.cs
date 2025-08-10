using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Responses;

namespace QuemVaiVai.Application.Interfaces.Services
{
    public interface IUserAppService : IService<User>
    {
        Task<UserDTO> CreateUserAsync(CreateUserDTO request);
    }
}
