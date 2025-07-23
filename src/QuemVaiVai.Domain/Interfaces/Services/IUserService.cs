using QuemVaiVai.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Interfaces.Services
{
    public interface IUserService : IService<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<bool> IsEmailInUseAsync(string email);
        Task<User?> AuthenticateAsync(string email, string password);
        //Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
    }
}
