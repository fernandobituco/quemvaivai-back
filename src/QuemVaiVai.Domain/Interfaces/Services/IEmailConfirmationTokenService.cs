using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Domain.Interfaces.Services
{
    public interface IEmailConfirmationTokenService
    {
        EmailConfirmationToken GenerateToken(int userId);
        void ValidateToken(EmailConfirmationToken emailConfirmationToken);
    }
}
