using QuemVaiVai.Domain.Interfaces.Services;

namespace QuemVaiVai.Domain.Services
{
    public class UserService : IUserService
    {
        public UserService()
        {
        }

        public Task<bool> ValidateEmail(string email)
        {
            throw new NotImplementedException();
        }
    }
}
