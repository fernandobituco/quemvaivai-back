using QuemVaiVai.Domain.Exceptions;
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

        public void ValidatePassword(string password)
        {
            if (password == null)
            {
                throw new InvalidPasswordException("É preciso digitar uma senha");
            }
            if (password.Length < 8)
            {
                throw new InvalidPasswordException("A senha precisa ter no mínimo 8 caracteres");
            }
        }
    }
}
