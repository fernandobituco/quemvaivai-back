using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Infrastructure.Contexts;

namespace QuemVaiVai.Infrastructure.Repositories
{
    public class UserEventRepository : RepositoryBase<UserEvent>, IUserEventRepository
    {
        public UserEventRepository(AppDbContext context) : base(context)
        {
        }
    }
}
