using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Infrastructure.Contexts;

namespace QuemVaiVai.Infrastructure.Repositories
{
    public class EmailConfirmationTokenRepository : RepositoryBase<EmailConfirmationToken>, IEmailConfirmationTokenRepository
    {
        public EmailConfirmationTokenRepository(AppDbContext context) : base(context)
        {
        }
    }
}
