using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Application.Interfaces.Services
{
    public interface IGroupUserAppService
    {
        Task ChangeUserRoleInGroup(int groupId, int userId, Role role, int responsibleUserId);
        Task JoinGroup(Guid inviteCode, int userId);
        Task RemoveUserFromGroup(int groupId, int userId, int responsibleUserId);
    }
}
