
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Application.Interfaces.DapperRepositories
{
    public interface IUserDapperRepository
    {
        Task<User?> GetById(int id);

        Task<User?> GetByEmail(string email);
        Task<User?> GetSensitiveByEmail(string email);
        Task<bool> ExistsByEmail(string email);
        Task<bool> ExistsByEmailDiferentId(string email, int id);
        Task<User?> GetCompleteForUpdateById(int id);
    }
}
