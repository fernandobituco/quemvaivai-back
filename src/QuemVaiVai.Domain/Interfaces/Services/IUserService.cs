
namespace QuemVaiVai.Domain.Interfaces.Services
{
    public interface IUserService
    {
        bool ValidateEmail(string email);
        void ValidatePassword(string email);
    }
}
