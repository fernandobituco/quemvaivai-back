using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Interfaces.Repositories;
using QuemVaiVai.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Services
{
    public class UserService : ServiceBase<User>, IUserService
    {
        public UserService(IRepository<User> repository) : base(repository)
        {
        }

        public Task<User?> AuthenticateAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsEmailInUseAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
