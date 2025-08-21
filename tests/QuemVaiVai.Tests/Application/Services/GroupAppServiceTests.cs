using Moq;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Services;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Enums;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Tests.Fixtures.Application;
using Xunit;

namespace QuemVaiVai.Tests.Application.Services
{
    public class GroupAppServiceTests : IClassFixture<GroupAppServiceFixture>
    {
        private readonly GroupAppServiceFixture _fixture;
        private readonly GroupAppService _groupAppService;
        private const int ValidUserId = 1;

        public GroupAppServiceTests(GroupAppServiceFixture fixture)
        {
            _fixture = fixture;
            _groupAppService = _fixture.CreateService();
        }

        #region Success Tests

        [Fact]
        public async Task CreateGroupAsync_WithValidRequest_ShouldCreateGroupSuccessfully()
        {
            _fixture.GroupRepoMock.Invocations.Clear();
            _fixture.GroupUserRepoMock.Invocations.Clear();

            // Arrange
            var request = CreateValidCreateGroupDTO();
            var expectedGroup = CreateValidGroup();
            var expectedGroupDTO = CreateValidGroupDTO();
            var expectedGroupUser = CreateValidGroupUser();

            _fixture.MapperMock.Setup(m => m.Map<Group>(request)).Returns(expectedGroup);
            _fixture.GroupRepoMock.Setup(r => r.AddAsync(It.IsAny<Group>(), ValidUserId)).ReturnsAsync(expectedGroup);
            _fixture.MapperMock.Setup(m => m.Map<GroupDTO>(expectedGroup)).Returns(expectedGroupDTO);
            _fixture.GroupUserRepoMock.Setup(r => r.AddAsync(It.IsAny<GroupUser>(), ValidUserId)).ReturnsAsync(expectedGroupUser);

            // Act
            var result = await _groupAppService.CreateGroupAsync(request, ValidUserId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedGroupDTO.Id, result.Id);
            Assert.Equal(expectedGroupDTO.Name, result.Name);
            Assert.Equal(expectedGroupDTO.Description, result.Description);

            _fixture.GroupRepoMock.Verify(r => r.AddAsync(It.IsAny<Group>(), ValidUserId), Times.Once);
            _fixture.GroupUserRepoMock.Verify(r => r.AddAsync(
                It.Is<GroupUser>(gu => gu.GroupId == expectedGroup.Id && gu.UserId == ValidUserId && gu.Role == Role.ADMIN),
                ValidUserId), Times.Once);
        }

        [Fact]
        public async Task UpdateGroupAsync_WithValidRequest_ShouldUpdateGroupSuccessfully()
        {
            // Arrange
            var request = CreateValidUpdateGroupDTO();
            var existingGroup = CreateValidGroup();
            var expectedGroupDTO = CreateUpdatedGroupDTO();

            SetupSuccessfulGroupUpdate(request, existingGroup, expectedGroupDTO);

            // Act
            var result = await _groupAppService.UpdateGroupAsync(request, ValidUserId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedGroupDTO.Id, result.Id);
            Assert.Equal(request.Name, result.Name);
            Assert.Equal(request.Description, result.Description);

            _fixture.GroupRepoMock.Verify(r => r.UpdateAsync(It.IsAny<Group>(), ValidUserId), Times.Once);
        }

        [Fact]
        public async Task GetById_WithValidId_ShouldReturnGroupSuccessfully()
        {
            // Arrange
            var groupId = 1;
            var expectedGroup = CreateValidGroup();
            var expectedGroupDTO = CreateValidGroupDTO();

            _fixture.GroupUserDapperRepoMock.Setup(r => r.CanUserEditGroup(groupId, ValidUserId)).ReturnsAsync(true);
            _fixture.GroupDapperRepoMock.Setup(r => r.GetById(groupId)).ReturnsAsync(expectedGroup);
            _fixture.MapperMock.Setup(m => m.Map<GroupDTO>(expectedGroup)).Returns(expectedGroupDTO);

            // Act
            var result = await _groupAppService.GetById(groupId, ValidUserId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedGroupDTO.Id, result.Id);
            Assert.Equal(expectedGroupDTO.Name, result.Name);
            Assert.Equal(expectedGroupDTO.Description, result.Description);
        }

        [Fact]
        public async Task DeleteGroupAsync_WithValidId_ShouldDeleteGroupSuccessfully()
        {
            // Arrange
            var groupId = 1;

            _fixture.GroupUserDapperRepoMock.Setup(r => r.CanUserEditGroup(groupId, ValidUserId)).ReturnsAsync(true);
            _fixture.GroupRepoMock.Setup(r => r.DeleteAsync(groupId, ValidUserId)).Returns(Task.CompletedTask);

            // Act
            await _groupAppService.DeleteGroupAsync(groupId, ValidUserId);

            // Assert
            _fixture.GroupRepoMock.Verify(r => r.DeleteAsync(groupId, ValidUserId), Times.Once);
        }

        #endregion

        #region Validation Tests

        [Fact]
        public async Task CreateGroupAsync_WithNullRequest_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _groupAppService.CreateGroupAsync(null, ValidUserId));
        }

        [Fact]
        public async Task UpdateGroupAsync_WithNullRequest_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _groupAppService.UpdateGroupAsync(null, ValidUserId));
        }

        [Fact]
        public async Task GetById_WhenGroupNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            var groupId = 1;

            _fixture.GroupUserDapperRepoMock.Setup(r => r.CanUserEditGroup(groupId, ValidUserId)).ReturnsAsync(true);
            _fixture.GroupDapperRepoMock.Setup(r => r.GetById(groupId)).ReturnsAsync((Group?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _groupAppService.GetById(groupId, ValidUserId));
            Assert.Contains("Group", exception.Message);
        }

        [Fact]
        public async Task UpdateGroupAsync_WhenGroupNotFound_ShouldThrowNotFoundException()
        {
            // Arrange
            var request = CreateValidUpdateGroupDTO();

            _fixture.GroupUserDapperRepoMock.Setup(r => r.CanUserEditGroup(request.Id, ValidUserId)).ReturnsAsync(true);
            _fixture.GroupDapperRepoMock.Setup(r => r.GetById(request.Id)).ReturnsAsync((Group?)null);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<NotFoundException>(() => _groupAppService.UpdateGroupAsync(request, ValidUserId));
            Assert.Contains("Grupo", exception.Message);
        }

        #endregion

        #region Authorization Tests

        [Fact]
        public async Task GetById_WhenUserCannotEditGroup_ShouldThrowUnauthorizedException()
        {
            // Arrange
            var groupId = 1;

            _fixture.GroupUserDapperRepoMock.Setup(r => r.CanUserEditGroup(groupId, ValidUserId)).ReturnsAsync(false);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UnauthorizedException>(() => _groupAppService.GetById(groupId, ValidUserId));
            Assert.Contains("You cannot edit this group.", exception.Message);
        }

        [Fact]
        public async Task UpdateGroupAsync_WhenUserCannotEditGroup_ShouldThrowUnauthorizedException()
        {
            // Arrange
            var request = CreateValidUpdateGroupDTO();

            _fixture.GroupUserDapperRepoMock.Setup(r => r.CanUserEditGroup(request.Id, ValidUserId)).ReturnsAsync(false);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UnauthorizedException>(() => _groupAppService.UpdateGroupAsync(request, ValidUserId));
            Assert.Contains("You cannot edit this group.", exception.Message);
        }

        [Fact]
        public async Task DeleteGroupAsync_WhenUserCannotEditGroup_ShouldThrowUnauthorizedException()
        {
            // Arrange
            var groupId = 1;

            _fixture.GroupUserDapperRepoMock.Setup(r => r.CanUserEditGroup(groupId, ValidUserId)).ReturnsAsync(false);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<UnauthorizedException>(() => _groupAppService.DeleteGroupAsync(groupId, ValidUserId));
            Assert.Contains("You cannot edit this group.", exception.Message);
        }

        #endregion

        #region CreateGroupAsync - Group User Assignment Tests

        [Fact]
        public async Task CreateGroupAsync_WhenGroupCreationSucceeds_ShouldCreateGroupUserWithAdminRole()
        {
            _fixture.GroupUserRepoMock.Invocations.Clear();

            // Arrange
            var request = CreateValidCreateGroupDTO();
            var expectedGroup = CreateValidGroup();
            var expectedGroupDTO = CreateValidGroupDTO();
            var expectedGroupUser = CreateValidGroupUser();

            SetupSuccessfulGroupCreation(request, expectedGroup, expectedGroupDTO, expectedGroupUser);

            // Act
            await _groupAppService.CreateGroupAsync(request, ValidUserId);

            // Assert
            _fixture.GroupUserRepoMock.Verify(r => r.AddAsync(
                It.Is<GroupUser>(gu =>
                    gu.GroupId == expectedGroup.Id &&
                    gu.UserId == ValidUserId &&
                    gu.Role == Role.ADMIN),
                ValidUserId), Times.Once);
        }

        #endregion

        #region Integration Tests

        [Fact]
        public async Task CreateGroupAsync_ShouldCallAllDependenciesInCorrectOrder()
        {
            // Arrange
            var request = CreateValidCreateGroupDTO();
            var expectedGroup = CreateValidGroup();
            var expectedGroupDTO = CreateValidGroupDTO();
            var expectedGroupUser = CreateValidGroupUser();

            SetupSuccessfulGroupCreation(request, expectedGroup, expectedGroupDTO, expectedGroupUser);

            var callOrder = new List<string>();

            _fixture.MapperMock.Setup(m => m.Map<Group>(request))
                .Callback(() => callOrder.Add("MapToGroup"))
                .Returns(expectedGroup);

            _fixture.GroupRepoMock.Setup(r => r.AddAsync(It.IsAny<Group>(), ValidUserId))
                .Callback(() => callOrder.Add("CreateGroup"))
                .ReturnsAsync(expectedGroup);

            _fixture.GroupUserRepoMock.Setup(r => r.AddAsync(It.IsAny<GroupUser>(), ValidUserId))
                .Callback(() => callOrder.Add("CreateGroupUser"))
                .ReturnsAsync(expectedGroupUser);

            _fixture.MapperMock.Setup(m => m.Map<GroupDTO>(expectedGroup))
                .Callback(() => callOrder.Add("MapToGroupDTO"))
                .Returns(expectedGroupDTO);

            // Act
            await _groupAppService.CreateGroupAsync(request, ValidUserId);

            // Assert
            Assert.Equal(4, callOrder.Count);
            Assert.Equal("MapToGroup", callOrder[0]);
            Assert.Equal("CreateGroup", callOrder[1]);
            Assert.Equal("CreateGroupUser", callOrder[2]);
            Assert.Equal("MapToGroupDTO", callOrder[3]);
        }

        [Fact]
        public async Task UpdateGroupAsync_ShouldCallAllDependenciesInCorrectOrder()
        {
            // Arrange
            var request = CreateValidUpdateGroupDTO();
            var existingGroup = CreateValidGroup();
            var expectedGroupDTO = CreateValidGroupDTO();

            SetupSuccessfulGroupUpdate(request, existingGroup, expectedGroupDTO);

            var callOrder = new List<string>();

            _fixture.GroupUserDapperRepoMock.Setup(r => r.CanUserEditGroup(request.Id, ValidUserId))
                .Callback(() => callOrder.Add("CheckPermission"))
                .ReturnsAsync(true);

            _fixture.GroupDapperRepoMock.Setup(r => r.GetById(request.Id))
                .Callback(() => callOrder.Add("GetGroup"))
                .ReturnsAsync(existingGroup);

            _fixture.GroupRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Group>(), ValidUserId))
                .Callback(() => callOrder.Add("UpdateGroup"))
                .Returns(Task.CompletedTask);

            _fixture.MapperMock.Setup(m => m.Map<GroupDTO>(It.IsAny<Group>()))
                .Callback(() => callOrder.Add("MapToGroupDTO"))
                .Returns(expectedGroupDTO);

            // Act
            await _groupAppService.UpdateGroupAsync(request, ValidUserId);

            // Assert
            Assert.Equal(4, callOrder.Count);
            Assert.Equal("CheckPermission", callOrder[0]);
            Assert.Equal("GetGroup", callOrder[1]);
            Assert.Equal("UpdateGroup", callOrder[2]);
            Assert.Equal("MapToGroupDTO", callOrder[3]);
        }

        #endregion

        #region Error Handling Tests

        [Fact]
        public async Task CreateGroupAsync_WhenGroupCreationFails_ShouldNotCreateGroupUser()
        {
            _fixture.GroupUserRepoMock.Invocations.Clear();
            // Arrange
            var request = CreateValidCreateGroupDTO();
            var expectedGroup = CreateValidGroup();

            _fixture.MapperMock.Setup(m => m.Map<Group>(request)).Returns(expectedGroup);
            _fixture.GroupRepoMock.Setup(r => r.AddAsync(It.IsAny<Group>(), ValidUserId)).ThrowsAsync(new Exception());

            // Act
            await Assert.ThrowsAsync<Exception>(() => _groupAppService.CreateGroupAsync(request, ValidUserId));

            // Assert
            _fixture.GroupUserRepoMock.Verify(r => r.AddAsync(It.IsAny<GroupUser>(), It.IsAny<int>()), Times.Never);
        }

        #endregion

        #region Helper Methods

        private static CreateGroupDTO CreateValidCreateGroupDTO()
        {
            return new CreateGroupDTO
            {
                Name = "Test Group",
                Description = "Test Description"
            };
        }

        private static UpdateGroupDTO CreateValidUpdateGroupDTO()
        {
            return new UpdateGroupDTO
            {
                Id = 1,
                Name = "Updated Group",
                Description = "Updated Description"
            };
        }

        private static Group CreateValidGroup()
        {
            return new Group
            {
                Id = 1,
                Name = "Test Group",
                Description = "Test Description"
            };
        }

        private static GroupDTO CreateValidGroupDTO()
        {
            return new GroupDTO
            {
                Id = 1,
                Name = "Test Group",
                Description = "Test Description"
            };
        }

        private static GroupDTO CreateUpdatedGroupDTO()
        {
            return new GroupDTO
            {
                Id = 1,
                Name = "Updated Group",
                Description = "Updated Description"
            };
        }

        private static GroupUser CreateValidGroupUser()
        {
            return new GroupUser
            {
                Id = 1,
                GroupId = 1,
                UserId = ValidUserId,
                Role = Role.ADMIN
            };
        }

        private void SetupSuccessfulGroupCreation(CreateGroupDTO request, Group group, GroupDTO groupDTO, GroupUser groupUser)
        {
            _fixture.MapperMock.Setup(m => m.Map<Group>(request)).Returns(group);
            _fixture.GroupRepoMock.Setup(r => r.AddAsync(It.IsAny<Group>(), ValidUserId)).ReturnsAsync(group);
            _fixture.MapperMock.Setup(m => m.Map<GroupDTO>(group)).Returns(groupDTO);
            _fixture.GroupUserRepoMock.Setup(r => r.AddAsync(It.IsAny<GroupUser>(), ValidUserId)).ReturnsAsync(groupUser);
        }

        private void SetupSuccessfulGroupUpdate(UpdateGroupDTO request, Group group, GroupDTO groupDTO)
        {
            _fixture.GroupUserDapperRepoMock.Setup(r => r.CanUserEditGroup(request.Id, ValidUserId)).ReturnsAsync(true);
            _fixture.GroupDapperRepoMock.Setup(r => r.GetById(request.Id)).ReturnsAsync(group);
            _fixture.GroupRepoMock.Setup(r => r.UpdateAsync(It.IsAny<Group>(), ValidUserId)).Returns(Task.CompletedTask);
            _fixture.MapperMock.Setup(m => m.Map<GroupDTO>(It.IsAny<Group>())).Returns(groupDTO);
        }

        #endregion
    }
}
