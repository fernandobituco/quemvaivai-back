
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Application.Interfaces.DapperRepositories
{
    public interface IUserDapperRepository
    {
        Task<User> GetById(int id);

        Task<User> GetByEmail(string email);
        Task<bool> ExistsByEmail(string email);
    }
}
