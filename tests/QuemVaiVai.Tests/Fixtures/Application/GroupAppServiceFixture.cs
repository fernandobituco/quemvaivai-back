using Microsoft.Extensions.Options;
using Moq;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Application.Interfaces.Email;
using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Application.Interfaces.Security;
using QuemVaiVai.Application.Services;
using QuemVaiVai.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Tests.Fixtures.Application
{
    public class GroupAppServiceFixture : ApplicationFixtureBase
    {
        public Mock<IGroupRepository> GroupRepoMock { get; } = new();
        public Mock<IGroupDapperRepository> GroupDapperRepoMock { get; } = new();
        public Mock<IGroupUserRepository> GroupUserRepoMock { get; } = new();
        public Mock<IGroupUserDapperRepository> GroupUserDapperRepoMock { get; } = new();

        public GroupAppService CreateService()
        {
            return new GroupAppService(
                GroupRepoMock.Object,
                MapperMock.Object,
                GroupUserRepoMock.Object,
                GroupUserDapperRepoMock.Object,
                GroupDapperRepoMock.Object
            );
        }
    }
}
