using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Application.Interfaces.Contexts
{
    public interface IUserContext
    {
        int? GetCurrentUserId();
    }
}
