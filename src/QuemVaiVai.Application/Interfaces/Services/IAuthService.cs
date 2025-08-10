using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Domain.Responses;

namespace QuemVaiVai.Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> LoginAsync(string email, string password);
        Task<LoginResponseDTO?> RefreshTokenAsync(string refreshToken);
        Task RevokeTokenAsync(string refreshToken);
        Task RevokeAllUserTokensAsync(int userId);
    }
}
