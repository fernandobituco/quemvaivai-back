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
    public class UserAppServiceFixture : ApplicationFixtureBase
    {
        public Mock<IUserRepository> UserRepoMock { get; } = new();
        public Mock<IUserDapperRepository> DapperRepoMock { get; } = new();
        public Mock<IPasswordHasher> PasswordHasherMock { get; } = new();
        public Mock<IEmailSender> EmailSenderMock { get; } = new();
        public Mock<IEmailTemplateBuilder> EmailTemplateBuilderMock { get; } = new();
        public Mock<IEmailConfirmationTokenService> EmailTokenServiceMock { get; } = new();
        public Mock<IEmailConfirmationTokenRepository> EmailTokenRepoMock { get; } = new();
        public Mock<IUserService> UserServiceMock { get; } = new();
        public Mock<ITokenGenerator> TokenGeneratorMock { get; } = new();

        public UserAppService CreateService()
        {
            return new UserAppService(
                UserRepoMock.Object,
                DapperRepoMock.Object,
                PasswordHasherMock.Object,
                TokenGeneratorMock.Object,
                MapperMock.Object,
                UserServiceMock.Object,
                EmailSenderMock.Object,
                EmailTemplateBuilderMock.Object,
                EmailTokenServiceMock.Object,
                EmailTokenRepoMock.Object,
                Options.Create(AppSettings)
            );
        }
    }
}
