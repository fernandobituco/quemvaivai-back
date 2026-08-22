using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Infrastructure.Contexts;

namespace QuemVaiVai.Infrastructure.Repositories
{
    public class PasswordResetTokenRepository : RepositoryBase<PasswordResetToken>, IPasswordResetTokenRepository
    {
        public PasswordResetTokenRepository(AppDbContext context) : base(context)
        {
        }
    }
}
