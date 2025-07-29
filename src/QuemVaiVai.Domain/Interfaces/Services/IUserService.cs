using QuemVaiVai.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<bool> ValidateEmail(string email);
        void ValidatePassword(string email);
    }
}
