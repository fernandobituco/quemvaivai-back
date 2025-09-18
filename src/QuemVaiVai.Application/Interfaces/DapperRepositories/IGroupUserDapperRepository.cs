using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Application.Interfaces.DapperRepositories
{
    public interface IGroupUserDapperRepository
    {
        Task<bool> CanUserEditGroup(int groupId, int userId);
        Task<int> GetIdByGroupIdAndUserId(int groupId, int userId);
        Task<GroupUser?> GetByGroupIdAndUserId(int groupId, int userId);
        Task<int> GetMemberCountByGroupId(int groupId);
        Task<IEnumerable<UserMemberDTO>> GetAllByGroupId(int groupId);
    }
}
