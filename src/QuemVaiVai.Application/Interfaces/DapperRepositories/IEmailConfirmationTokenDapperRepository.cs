using QuemVaiVai.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Application.Interfaces.DapperRepositories
{
    public interface IEmailConfirmationTokenDapperRepository
    {
        Task<EmailConfirmationToken> GetByToken(string token);
    }
}
