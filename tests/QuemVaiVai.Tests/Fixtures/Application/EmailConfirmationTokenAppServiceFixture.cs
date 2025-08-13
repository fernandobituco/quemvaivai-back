using Moq;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Application.Services;
using QuemVaiVai.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Tests.Fixtures.Application
{
    public class EmailConfirmationTokenAppServiceFixture
    {
        public Mock<IEmailConfirmationTokenRepository> TokenRepoMock { get; } = new();
        public Mock<IEmailConfirmationTokenDapperRepository> TokenDapperRepoMock { get; } = new();
        public Mock<IUserRepository> UserRepoMock { get; } = new();
        public Mock<IEmailConfirmationTokenService> TokenServiceMock { get; } = new();
        public Mock<IUserDapperRepository> UserDapperRepoMock { get; } = new();

        public EmailConfirmationTokenAppService CreateService()
        {
            return new EmailConfirmationTokenAppService(
                TokenRepoMock.Object,
                TokenDapperRepoMock.Object,
                UserRepoMock.Object,
                TokenServiceMock.Object,
                UserDapperRepoMock.Object
            );
        }

        /// <summary>
        /// Limpa setups e histórico entre testes quando a fixture é compartilhada (IClassFixture).
        /// </summary>
        public void Reset()
        {
            TokenRepoMock.Reset();
            TokenDapperRepoMock.Reset();
            UserRepoMock.Reset();
            TokenServiceMock.Reset();
            UserDapperRepoMock.Reset();
        }
    }
}
