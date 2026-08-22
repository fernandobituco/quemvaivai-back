using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Domain.Interfaces.Services;

namespace QuemVaiVai.Domain.Services
{
    public class UserService : IUserService
    {
        public UserService()
        {
        }

        public bool ValidateEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
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
