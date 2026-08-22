
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Application.Interfaces.DapperRepositories
{
    public interface IPasswordResetTokenDapperRepository
    {
        Task<PasswordResetToken?> GetLastByUserId(int userId);
    }
}
