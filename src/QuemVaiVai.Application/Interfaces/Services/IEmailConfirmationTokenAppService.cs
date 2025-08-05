using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Application.Interfaces.Services
{
    public interface IEmailConfirmationTokenAppService : IService<EmailConfirmationToken>
    {
        Task ConfirmAccount(string token);
    }
}
