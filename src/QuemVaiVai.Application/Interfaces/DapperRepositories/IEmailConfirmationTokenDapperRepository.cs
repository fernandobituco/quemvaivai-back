using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Application.Interfaces.DapperRepositories
{
    public interface IEmailConfirmationTokenDapperRepository
    {
        Task<EmailConfirmationToken?> GetByToken(string token);
    }
}
