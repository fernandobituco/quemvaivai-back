using AutoMapper;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Enums;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Domain.Responses;

namespace QuemVaiVai.Application.Services
{
    public class GroupAppService : ServiceBase<Group>, IGroupAppService
    {
        private readonly IGroupDapperRepository _groupDapperRepository;
        private readonly IGroupUserRepository _groupUserRepository;
        private readonly IGroupUserDapperRepository _groupUserDapperRepository;
        public GroupAppService(IGroupRepository repository, IMapper mapper, IGroupUserRepository groupUserRepository, IGroupUserDapperRepository groupUserDapperRepository, IGroupDapperRepository groupDapperRepository) : base(repository, mapper)
        {
            _groupUserRepository = groupUserRepository;
            _groupUserDapperRepository = groupUserDapperRepository;
            _groupDapperRepository = groupDapperRepository;
        }

        public async Task<GroupDTO> CreateGroupAsync(CreateGroupDTO request, int userId)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            Group group = _mapper.Map<Group>(request);

            var createdGroup = await _repository.AddAsync(group, userId);

            if (createdGroup != null)
            {
                GroupUser groupUser = new()
                {
                    GroupId = createdGroup.Id,
                    UserId = userId,
                    Role = Role.ADMIN,
                };
                await _groupUserRepository.AddAsync(groupUser, userId);
            }

            return _mapper.Map<GroupDTO>(createdGroup);
        }

        public async Task DeleteGroupAsync(int id, int userId)
        {
            await CanUserEditGroup(id, userId);

            await _repository.DeleteAsync(id, userId);
        }

        public async Task<GroupDTO> GetById(int groupId, int userId)
        {
            await CanUserEditGroup(groupId, userId);

            var group = _mapper.Map<GroupDTO>(await _groupDapperRepository.GetById(groupId));

            return group ?? throw new NotFoundException("Group");
        }

        public async Task<GroupDTO> UpdateGroupAsync(UpdateGroupDTO request, int userId)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            await CanUserEditGroup(request.Id, userId);

            var group = await _groupDapperRepository.GetById(request.Id) ?? throw new NotFoundException("Grupo");

            group.Name = request.Name;
            group.Description = request.Description;

            await _repository.UpdateAsync(group, userId);

            return _mapper.Map<GroupDTO>(group);
        }

        private async Task CanUserEditGroup(int groupId, int userId)
        {
            if (!await _groupUserDapperRepository.CanUserEditGroup(groupId, userId))
                throw new UnauthorizedException("You cannot edit this group.");
        }
    }
}
