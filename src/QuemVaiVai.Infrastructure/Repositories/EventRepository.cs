using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Infrastructure.Contexts;

namespace QuemVaiVai.Infrastructure.Repositories
{
    public class EventRepository : RepositoryBase<Event>, IEventRepository
    {
        public EventRepository(AppDbContext context) : base(context)
        {
        }
    }
}
