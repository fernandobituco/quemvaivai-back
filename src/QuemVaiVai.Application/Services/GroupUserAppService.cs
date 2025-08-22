using AutoMapper;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Enums;
using QuemVaiVai.Domain.Exceptions;

namespace QuemVaiVai.Application.Services
{
    public class GroupUserAppService : ServiceBase<GroupUser>, IGroupUserAppService
    {
        private readonly IGroupUserDapperRepository _groupUserDapperRepository;
        private readonly IGroupDapperRepository _groupDapperRepository;
        public GroupUserAppService(
            IGroupUserRepository repository,
            IMapper mapper,
            IGroupUserDapperRepository groupUserDapperRepository,
            IGroupDapperRepository groupDapperRepository) : base(repository, mapper)
        {
            _groupUserDapperRepository = groupUserDapperRepository;
            _groupDapperRepository = groupDapperRepository;
        }

        public async Task JoinGroup(Guid inviteCode, int userId)
        {
            var groupId = await _groupDapperRepository.GetIdByInviteCode(inviteCode);

            if (groupId == null || groupId <= 0)
            {
                throw new NotFoundException("Group");
            }

            var isUserInGroup = await _groupUserDapperRepository.GetIdByGroupIdAndUserId((int)groupId, userId);

            if (isUserInGroup > 0 )
                throw new UserAlreadyPartOfGroupException();

            GroupUser groupUser = new()
            {
                GroupId = (int)groupId,
                UserId = userId,
            };

            await _repository.AddAsync(groupUser, userId);
        }

        public async Task ChangeUserRoleInGroup(int groupId, int userId, Role role, int responsibleUserId)
        {
            await CanUserEditGroup(groupId, responsibleUserId);

            var groupUser = await _groupUserDapperRepository.GetByGroupIdAndUserId(groupId, userId) ?? throw new NotFoundException("GroupUser");

            groupUser.Role = role;

            await _repository.UpdateAsync(groupUser);
        }

        public async Task RemoveUserFromGroup(int groupId, int userId, int responsibleUserId)
        {
            await CanUserEditGroup(groupId, responsibleUserId);

            var groupUser = await _groupUserDapperRepository.GetIdByGroupIdAndUserId(groupId, userId);

            if (groupUser <= 0)
                throw new NotFoundException("GroupUser");

            await _repository.DeleteAsync(groupUser, responsibleUserId);

            throw new NotImplementedException();
        }

        private async Task CanUserEditGroup(int groupId, int userId)
        {
            if (!await _groupUserDapperRepository.CanUserEditGroup(groupId, userId))
                throw new UnauthorizedException("You cannot edit this group.");
        }
    }
}
