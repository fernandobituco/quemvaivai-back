using Moq;
using QuemVaiVai.Application.Services;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Tests.Fixtures.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace QuemVaiVai.Tests.Application.Services
{
    public class EmailConfirmationTokenAppServiceTests : IClassFixture<EmailConfirmationTokenAppServiceFixture>
    {
        private readonly EmailConfirmationTokenAppServiceFixture _fixture;
        private readonly EmailConfirmationTokenAppService _sut;

        public EmailConfirmationTokenAppServiceTests(EmailConfirmationTokenAppServiceFixture fixture)
        {
            _fixture = fixture;
            _fixture.Reset();                 // evita “vazamento” de chamadas entre testes
            _sut = _fixture.CreateService();  // cria uma instância “fresh” por teste
        }

        [Fact]
        public async Task ConfirmAccount_ShouldThrowNotFound_WhenTokenDoesNotExist()
        {
            // Arrange
            var token = "does-not-exist";
            _fixture.TokenDapperRepoMock.Setup(r => r.GetByToken(token)).ReturnsAsync((EmailConfirmationToken?)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<NotFoundException>(() => _sut.ConfirmAccount(token));

            _fixture.TokenServiceMock.Verify(s => s.ValidateToken(It.IsAny<EmailConfirmationToken>()), Times.Never);
            _fixture.TokenRepoMock.Verify(r => r.UpdateAsync(It.IsAny<EmailConfirmationToken>(), It.IsAny<int?>()), Times.Never);
            _fixture.UserRepoMock.Verify(r => r.UpdateAsync(It.IsAny<User>(), It.IsAny<int?>()), Times.Never);
        }

        [Fact]
        public async Task ConfirmAccount_ShouldPropagateException_WhenTokenInvalid()
        {
            // Arrange
            var token = "some-token";
            var emailToken = new EmailConfirmationToken { Token = token, UserId = 1, Used = false };

            _fixture.TokenDapperRepoMock.Setup(r => r.GetByToken(token)).ReturnsAsync(emailToken);
            _fixture.TokenServiceMock
                .Setup(s => s.ValidateToken(emailToken))
                .Throws(new InvalidOperationException("Invalid token"));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => _sut.ConfirmAccount(token));
            Assert.Equal("Invalid token", ex.Message);

            _fixture.UserRepoMock.Verify(r => r.UpdateAsync(It.IsAny<User>(), It.IsAny<int?>()), Times.Never);
            _fixture.TokenRepoMock.Verify(r => r.UpdateAsync(It.IsAny<EmailConfirmationToken>(), It.IsAny<int?>()), Times.Never);
        }

        [Fact]
        public async Task ConfirmAccount_ShouldConfirmUserAndMarkTokenAsUsed_WhenValid()
        {
            // Arrange
            var token = "valid-token";
            var userId = 1;

            var emailToken = new EmailConfirmationToken
            {
                Token = token,
                UserId = userId,
                Used = false
            };

            var user = new User
            {
                Id = userId,
                Confirmed = false
            };

            _fixture.TokenDapperRepoMock.Setup(r => r.GetByToken(token)).ReturnsAsync(emailToken);
            _fixture.TokenServiceMock.Setup(s => s.ValidateToken(emailToken)); // não lança
            _fixture.UserRepoMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);

            // Act
            await _sut.ConfirmAccount(token);

            // Assert (estado)
            Assert.True(user.Confirmed);
            Assert.True(emailToken.Used);

            // Assert (interações)
            _fixture.UserRepoMock.Verify(r => r.GetByIdAsync(userId), Times.Once);
            _fixture.UserRepoMock.Verify(r => r.UpdateAsync(It.Is<User>(u => u.Confirmed), It.IsAny<int?>()), Times.Once);
            _fixture.TokenRepoMock.Verify(r => r.UpdateAsync(It.Is<EmailConfirmationToken>(t => t.Used), It.IsAny<int?>()), Times.Once);
        }

        [Fact]
        public async Task ConfirmAccount_ShouldCallDependenciesInOrder()
        {
            // Arrange
            var token = "ordered-token";
            var userId = 1;
            var emailToken = new EmailConfirmationToken { Token = token, UserId = userId, Used = false };
            var user = new User { Id = userId, Confirmed = false };

            var order = new List<string>();

            _fixture.TokenDapperRepoMock
                .Setup(r => r.GetByToken(token))
                .Callback(() => order.Add("GetToken"))
                .ReturnsAsync(emailToken);

            _fixture.TokenServiceMock
                .Setup(s => s.ValidateToken(emailToken))
                .Callback(() => order.Add("ValidateToken"));

            _fixture.UserRepoMock
                .Setup(r => r.GetByIdAsync(userId))
                .Callback(() => order.Add("GetUser"))
                .ReturnsAsync(user);

            _fixture.UserRepoMock
                .Setup(r => r.UpdateAsync(It.IsAny<User>(), It.IsAny<int?>()))
                .Callback(() => order.Add("UpdateUser"));

            _fixture.TokenRepoMock
                .Setup(r => r.UpdateAsync(It.IsAny<EmailConfirmationToken>(), It.IsAny<int?>()))
                .Callback(() => order.Add("UpdateToken"));

            // Act
            await _sut.ConfirmAccount(token);

            // Assert
            Assert.Equal(5, order.Count);
            Assert.Equal("GetToken", order[0]);
            Assert.Equal("ValidateToken", order[1]);
            Assert.Equal("GetUser", order[2]);
            Assert.Equal("UpdateUser", order[3]);
            Assert.Equal("UpdateToken", order[4]);
        }
    }
}
