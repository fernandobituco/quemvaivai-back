using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Domain.Interfaces.Services;

namespace QuemVaiVai.Domain.Services
{
    public class EmailConfirmationTokenService : IEmailConfirmationTokenService
    {
        public EmailConfirmationToken GenerateToken(int userId)
        {
            EmailConfirmationToken token = new EmailConfirmationToken()
            {
                UserId = userId,
                Token = Guid.NewGuid().ToString(),
                Expiration = DateTime.UtcNow.AddDays(1),
                Used = false,
                CreatedAt = DateTime.UtcNow,
                CreatedUser = userId,
            };

            return token;
        }

        public void ValidateToken(EmailConfirmationToken emailConfirmationToken)
        {
            if (emailConfirmationToken.Used)
            {
                throw new UsedTokenException();
            }
            if (emailConfirmationToken.UserId == 0)
            {
                throw new NotFoundException("Usuário");
            }
            if (emailConfirmationToken.Expiration <= DateTime.UtcNow)
            {
                throw new ExpiredTokenException();
            }
        }
    }
}
