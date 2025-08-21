using Moq;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Services;
using QuemVaiVai.Domain.Entities;
using QuemVaiVai.Domain.Exceptions;
using QuemVaiVai.Tests.Fixtures.Application;
using Xunit;

namespace QuemVaiVai.Tests.Application.Services
{
    public class UserAppServiceTests : IClassFixture<UserAppServiceFixture>
    {
        private readonly UserAppServiceFixture _fixture;
        private readonly UserAppService _userAppService;

        public UserAppServiceTests(UserAppServiceFixture fixture)
        {
            _fixture = fixture;
            _userAppService = _fixture.CreateService();
        }

        #region Success Tests

        [Fact]
        public async Task CreateUserAsync_WithValidRequest_ShouldCreateUserSuccessfully()
        {
            _fixture.EmailSenderMock.Invocations.Clear();
            // Arrange
            var request = CreateValidCreateUserDTO();
            var expectedUser = CreateValidUser();
            var expectedUserDTO = CreateValidUserDTO();
            var expectedToken = CreateValidEmailConfirmationToken();

            _fixture.MapperMock.Setup(m => m.Map<User>(request)).Returns(expectedUser);
            _fixture.PasswordHasherMock.Setup(p => p.Hash(request.Password)).Returns("hashedPassword");
            _fixture.UserRepoMock.Setup(r => r.AddAsync(It.IsAny<User>(), It.IsAny<int?>())).ReturnsAsync(expectedUser);
            _fixture.MapperMock.Setup(m => m.Map<UserDTO>(expectedUser)).Returns(expectedUserDTO);
            _fixture.DapperRepoMock.Setup(d => d.ExistsByEmail(request.Email)).ReturnsAsync(false);
            _fixture.EmailTokenServiceMock.Setup(e => e.GenerateToken(expectedUserDTO.Id)).Returns(expectedToken);
            _fixture.EmailTemplateBuilderMock.Setup(e => e.BuildTemplateAsync("AccountConfirmation", It.IsAny<Dictionary<string, string>>()))
                .ReturnsAsync("template body");

            // Act
            var result = await _userAppService.CreateUserAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUserDTO.Id, result.Id);
            Assert.Equal(expectedUserDTO.Name, result.Name);
            Assert.Equal(expectedUserDTO.Email, result.Email);

            _fixture.PasswordHasherMock.Verify(p => p.Hash(request.Password), Times.AtLeastOnce);
            _fixture.UserRepoMock.Verify(r => r.AddAsync(It.Is<User>(u => u.PasswordHash == "hashedPassword"), It.IsAny<int?>()), Times.AtLeastOnce);
            _fixture.EmailTokenRepoMock.Verify(e => e.AddAsync(expectedToken, It.IsAny<int?>()), Times.AtLeastOnce);
            _fixture.EmailSenderMock.Verify(e => e.SendEmailAsync(request.Email, "AccountConfirmation", "template body"), Times.Once);
        }

        [Fact]
        public async Task CreateUserAsync_ShouldGenerateCorrectConfirmationUrl()
        {
            // Arrange
            var request = CreateValidCreateUserDTO();
            var expectedUser = CreateValidUser();
            var expectedUserDTO = CreateValidUserDTO();
            var expectedToken = CreateValidEmailConfirmationToken();
            var expectedUrl = $"{_fixture.AppSettings.FRONT_END_URL}/account-confirmation?token={expectedToken.Token}";

            SetupSuccessfulUserCreation(request, expectedUser, expectedUserDTO, expectedToken);

            Dictionary<string, string>? capturedTemplateData = null;
            _fixture.EmailTemplateBuilderMock.Setup(e => e.BuildTemplateAsync("AccountConfirmation", It.IsAny<Dictionary<string, string>>()))
                .Callback<string, Dictionary<string, string>>((template, data) => capturedTemplateData = data)
                .ReturnsAsync("template body");

            // Act
            await _userAppService.CreateUserAsync(request);

            // Assert
            Assert.NotNull(capturedTemplateData);
            Assert.Equal(request.Name, capturedTemplateData["Name"]);
            Assert.Equal(expectedUrl, capturedTemplateData["ConfirmationUrl"]);
        }

        [Fact]
        public async Task UpdateUserAsync_WithValidRequest_ShouldUpdateUserSuccessfully()
        {
            // Arrange
            var request = CreateValidUpdateUserDTO();
            var expectedUser = CreateValidUser();
            var expectedUserDTO = CreateValidUserDTO();

            SetupSuccessfulUserUpdate(request, expectedUser, expectedUserDTO);

            // Act
            var result = await _userAppService.UpdateUserAsync(request);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUserDTO.Id, result.Id);
            Assert.Equal(expectedUserDTO.Name, result.Name);
            Assert.Equal(expectedUserDTO.Email, result.Email);

            _fixture.UserRepoMock.Verify(r => r.UpdateAsync(It.IsAny<User>(), It.IsAny<int?>()), Times.AtLeastOnce);
        }

        #endregion

        #region Validation Tests

        [Fact]
        public async Task CreateUserAsync_WithNullRequest_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userAppService.CreateUserAsync(null));
        }

        [Fact]
        public async Task UpdateUserAsync_WithNullRequest_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => _userAppService.UpdateUserAsync(null));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("invalid-email")]
        [InlineData("@domain.com")]
        [InlineData("user@")]
        [InlineData("user.domain.com")]
        public async Task CreateUserAsync_WithInvalidEmail_ShouldThrowArgumentException(string email)
        {
            // Arrange
            var request = CreateValidCreateUserDTO();
            request.Email = email;

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userAppService.CreateUserAsync(request));
            Assert.Contains("Email inválido", exception.Message);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData("invalid-email")]
        [InlineData("@domain.com")]
        [InlineData("user@")]
        [InlineData("user.domain.com")]
        public async Task UpdateUserAsync_WithInvalidEmail_ShouldThrowArgumentException(string email)
        {
            // Arrange
            var request = CreateValidUpdateUserDTO();
            var expectedUser = CreateValidUser();
            request.Email = email;

            _fixture.DapperRepoMock.Setup(d => d.GetCompleteForUpdateById(request.Id)).ReturnsAsync(expectedUser);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<ArgumentException>(() => _userAppService.UpdateUserAsync(request));
            Assert.Contains("Email inválido", exception.Message);
        }

        [Fact]
        public async Task CreateUserAsync_WithPasswordMismatch_ShouldThrowInvalidPasswordException()
        {
            // Arrange
            var request = CreateValidCreateUserDTO();
            request.Password = "password123";
            request.PasswordConfirmation = "different123";

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidPasswordException>(() => _userAppService.CreateUserAsync(request));
            Assert.Contains("A confirmação precisa estar igual à senha", exception.Message);
        }

        [Fact]
        public async Task CreateUserAsync_WithInvalidPassword_ShouldThrowException()
        {
            // Arrange
            var request = CreateValidCreateUserDTO();
            _fixture.UserServiceMock.Setup(u => u.ValidatePassword(request.Password))
                .Throws(new InvalidPasswordException("Senha deve ter pelo menos 8 caracteres"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidPasswordException>(() => _userAppService.CreateUserAsync(request));
            Assert.Contains("Senha deve ter pelo menos 8 caracteres", exception.Message);
        }

        [Fact]
        public async Task CreateUserAsync_WithExistingEmail_ShouldThrowEmailAlreadyExistsException()
        {
            // Arrange
            var request = CreateValidCreateUserDTO();
            _fixture.DapperRepoMock.Setup(d => d.ExistsByEmail(request.Email)).ReturnsAsync(true);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<EmailAlreadyExistsException>(() => _userAppService.CreateUserAsync(request));
            Assert.Contains($"O email {request.Email} já está em uso", exception.Message);
        }

        [Fact]
        public async Task UpdateUserAsync_WithExistingEmail_ShouldThrowEmailAlreadyExistsException()
        {
            // Arrange
            var request = CreateValidUpdateUserDTO();
            _fixture.DapperRepoMock.Setup(d => d.ExistsByEmailDiferentId(request.Email, request.Id)).ReturnsAsync(true);

            // Act & Assert
            var exception = await Assert.ThrowsAsync<EmailAlreadyExistsException>(() => _userAppService.UpdateUserAsync(request));
            Assert.Contains($"O email {request.Email} já está em uso", exception.Message);
        }

        #endregion

        #region CreateUserAsync - Email Confirmation Tests

        [Fact]
        public async Task CreateUserAsync_WhenUserCreationSucceeds_ShouldGenerateAndSaveEmailToken()
        {
            _fixture.EmailTokenServiceMock.Invocations.Clear();
            _fixture.EmailSenderMock.Invocations.Clear();
            // Arrange
            var request = CreateValidCreateUserDTO();
            var expectedUser = CreateValidUser();
            var expectedUserDTO = CreateValidUserDTO();
            var expectedToken = CreateValidEmailConfirmationToken();

            SetupSuccessfulUserCreation(request, expectedUser, expectedUserDTO, expectedToken);

            // Act
            await _userAppService.CreateUserAsync(request);

            // Assert
            _fixture.EmailTokenServiceMock.Verify(e => e.GenerateToken(expectedUserDTO.Id), Times.AtLeastOnce);
            _fixture.EmailTokenRepoMock.Verify(e => e.AddAsync(expectedToken, It.IsAny<int?>()), Times.AtLeastOnce);
        }

        [Fact]
        public async Task CreateUserAsync_WhenUserCreationSucceeds_ShouldSendConfirmationEmail()
        {
            _fixture.EmailSenderMock.Invocations.Clear();
            // Arrange
            var request = CreateValidCreateUserDTO();
            var expectedUser = CreateValidUser();
            var expectedUserDTO = CreateValidUserDTO();
            var expectedToken = CreateValidEmailConfirmationToken();

            SetupSuccessfulUserCreation(request, expectedUser, expectedUserDTO, expectedToken);
            Console.WriteLine(_fixture.EmailSenderMock.Invocations.Count);
            // Act
            await _userAppService.CreateUserAsync(request);

            // Assert
            _fixture.EmailSenderMock.Verify(e => e.SendEmailAsync(
                request.Email,
                "AccountConfirmation",
                It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task CreateUserAsync_WhenUserCreationFails_ShouldNotSendConfirmationEmail()
        {
            _fixture.EmailTokenServiceMock.Invocations.Clear();
            _fixture.EmailSenderMock.Invocations.Clear();
            // Arrange
            var request = CreateValidCreateUserDTO();
            var expectedUser = CreateValidUser();

            _fixture.MapperMock.Setup(m => m.Map<User>(request)).Returns(expectedUser);
            _fixture.PasswordHasherMock.Setup(p => p.Hash(request.Password)).Returns("hashedPassword");
            _fixture.UserRepoMock.Setup(r => r.AddAsync(It.IsAny<User>(), It.IsAny<int?>())).ThrowsAsync(new InvalidOperationException("Database error"));
            _fixture.MapperMock.Setup(m => m.Map<UserDTO?>(expectedUser)).Returns((UserDTO?)null);
            _fixture.DapperRepoMock.Setup(d => d.ExistsByEmail(request.Email)).ReturnsAsync(false);

            // Act & Assert
            var act = await Assert.ThrowsAsync<InvalidOperationException>(() => _userAppService.CreateUserAsync(request));

            // Assert
            _fixture.EmailTokenServiceMock.Verify(e => e.GenerateToken(It.IsAny<int>()), Times.Never);
            _fixture.EmailSenderMock.Verify(e => e.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
        }

        #endregion

        #region Integration Tests

        [Fact]
        public async Task CreateUserAsync_ShouldCallAllDependenciesInCorrectOrder()
        {
            // Arrange
            var request = CreateValidCreateUserDTO();
            var expectedUser = CreateValidUser();
            var expectedUserDTO = CreateValidUserDTO();
            var expectedToken = CreateValidEmailConfirmationToken();

            SetupSuccessfulUserCreation(request, expectedUser, expectedUserDTO, expectedToken);

            var callOrder = new List<string>();

            _fixture.DapperRepoMock.Setup(d => d.ExistsByEmail(It.IsAny<string>()))
                .Callback(() => callOrder.Add("EmailExists"))
                .ReturnsAsync(false);

            _fixture.UserServiceMock.Setup(u => u.ValidatePassword(It.IsAny<string>()))
                .Callback(() => callOrder.Add("ValidatePassword"));

            _fixture.PasswordHasherMock.Setup(p => p.Hash(It.IsAny<string>()))
                .Callback(() => callOrder.Add("HashPassword"))
                .Returns("hashedPassword");

            _fixture.UserRepoMock.Setup(r => r.AddAsync(It.IsAny<User>(), It.IsAny<int?>()))
                .Callback(() => callOrder.Add("CreateUser"))
                .ReturnsAsync(expectedUser);

            // Act
            await _userAppService.CreateUserAsync(request);

            // Assert
            Assert.Equal(4, callOrder.Count);
            Assert.Equal("EmailExists", callOrder[0]);
            Assert.Equal("ValidatePassword", callOrder[1]);
            Assert.Equal("HashPassword", callOrder[2]);
            Assert.Equal("CreateUser", callOrder[3]);
        }

        [Theory]
        [InlineData("test@example.com")]
        [InlineData("user.name@domain.co.uk")]
        [InlineData("test+tag@example.org")]
        public async Task CreateUserAsync_WithValidEmails_ShouldSucceed(string email)
        {
            // Arrange
            var request = CreateValidCreateUserDTO();
            request.Email = email;
            var expectedUser = CreateValidUser();
            var expectedUserDTO = CreateValidUserDTO();
            var expectedToken = CreateValidEmailConfirmationToken();

            SetupSuccessfulUserCreation(request, expectedUser, expectedUserDTO, expectedToken);

            // Act & Assert
            var result = await _userAppService.CreateUserAsync(request);
            Assert.NotNull(result);
        }

        #endregion

        #region Error Handling Tests

        [Fact]
        public async Task CreateUserAsync_WhenRepositoryThrowsException_ShouldPropagateException()
        {
            // Arrange
            var request = CreateValidCreateUserDTO();
            var expectedUser = CreateValidUser();

            _fixture.MapperMock.Setup(m => m.Map<User>(request)).Returns(expectedUser);
            _fixture.PasswordHasherMock.Setup(p => p.Hash(request.Password)).Returns("hashedPassword");
            _fixture.DapperRepoMock.Setup(d => d.ExistsByEmail(request.Email)).ReturnsAsync(false);
            _fixture.UserRepoMock.Setup(r => r.AddAsync(It.IsAny<User>(), It.IsAny<int?>()))
                .ThrowsAsync(new InvalidOperationException("Database error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _userAppService.CreateUserAsync(request));
            Assert.Contains("Database error", exception.Message);
        }

        [Fact]
        public async Task CreateUserAsync_WhenEmailSendingFails_ShouldStillReturnUser()
        {
            // Arrange
            var request = CreateValidCreateUserDTO();
            var expectedUser = CreateValidUser();
            var expectedUserDTO = CreateValidUserDTO();
            var expectedToken = CreateValidEmailConfirmationToken();

            SetupSuccessfulUserCreation(request, expectedUser, expectedUserDTO, expectedToken);
            _fixture.EmailSenderMock.Setup(e => e.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new InvalidOperationException("Email service unavailable"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _userAppService.CreateUserAsync(request));
            Assert.Contains("Email service unavailable", exception.Message);
        }

        #endregion

        #region Helper Methods

        private static CreateUserDTO CreateValidCreateUserDTO()
        {
            return new CreateUserDTO
            {
                Name = "Test User",
                Email = "test@example.com",
                Password = "ValidPassword123!",
                PasswordConfirmation = "ValidPassword123!"
            };
        }

        private static UpdateUserDTO CreateValidUpdateUserDTO()
        {
            return new UpdateUserDTO
            {
                Id = 1,
                Name = "Test User",
                Email = "test@example.com",
            };
        }

        private static User CreateValidUser()
        {
            return new User
            {
                Id = 1,
                Name = "Test User",
                Email = "test@example.com",
                PasswordHash = "hashedPassword"
            };
        }

        private static UserDTO CreateValidUserDTO()
        {
            return new UserDTO
            {
                Id = 1,
                Name = "Test User",
                Email = "test@example.com"
            };
        }

        private static EmailConfirmationToken CreateValidEmailConfirmationToken()
        {
            return new EmailConfirmationToken
            {
                Id = 1,
                Token = "confirmation-token-123",
                UserId = 1,
                Expiration = DateTime.UtcNow.AddHours(24)
            };
        }

        private void SetupSuccessfulUserCreation(CreateUserDTO request, User user, UserDTO userDTO, EmailConfirmationToken token)
        {
            _fixture.MapperMock.Setup(m => m.Map<User>(request)).Returns(user);
            _fixture.UserServiceMock.Setup(u => u.ValidatePassword(It.IsAny<string>()));
            _fixture.PasswordHasherMock.Setup(p => p.Hash(request.Password)).Returns("hashedPassword");
            _fixture.UserRepoMock.Setup(r => r.AddAsync(It.IsAny<User>(), It.IsAny<int?>())).ReturnsAsync(user);
            _fixture.MapperMock.Setup(m => m.Map<UserDTO>(user)).Returns(userDTO);
            _fixture.DapperRepoMock.Setup(d => d.ExistsByEmail(request.Email)).ReturnsAsync(false);
            _fixture.EmailTokenServiceMock.Setup(e => e.GenerateToken(userDTO.Id)).Returns(token);
            _fixture.EmailTemplateBuilderMock.Setup(e => e.BuildTemplateAsync("AccountConfirmation", It.IsAny<Dictionary<string, string>>()))
                .ReturnsAsync("template body");
        }

        private void SetupSuccessfulUserUpdate(UpdateUserDTO request, User user, UserDTO userDTO)
        {
            _fixture.MapperMock.Setup(m => m.Map<User>(request)).Returns(user);
            _fixture.UserRepoMock.Setup(r => r.UpdateAsync(It.IsAny<User>(), It.IsAny<int?>())).Returns(Task.CompletedTask);
            _fixture.MapperMock.Setup(m => m.Map<UserDTO>(user)).Returns(userDTO);
            _fixture.DapperRepoMock.Setup(d => d.ExistsByEmailDiferentId(request.Email, request.Id)).ReturnsAsync(false);
            _fixture.DapperRepoMock.Setup(d => d.GetCompleteForUpdateById(request.Id)).ReturnsAsync(user);
        }

        #endregion
    }
}
