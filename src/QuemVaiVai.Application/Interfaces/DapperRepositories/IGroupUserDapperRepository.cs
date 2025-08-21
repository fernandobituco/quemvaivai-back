using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Application.Interfaces.DapperRepositories
{
    public interface IGroupUserDapperRepository
    {
        Task<bool> CanUserEditGroup(int userId, int groupId);
    }
}
