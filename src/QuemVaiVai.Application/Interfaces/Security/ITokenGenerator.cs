using QuemVaiVai.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Application.Interfaces.Security
{
    public interface ITokenGenerator
    {
        string GenerateSecureToken();
        string GenerateAccessToken(User user);
    }
}
