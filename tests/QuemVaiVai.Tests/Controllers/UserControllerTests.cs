using Azure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Newtonsoft.Json;
using QuemVaiVai.Api.Controllers;
using QuemVaiVai.Application.DTOs;
using QuemVaiVai.Application.Interfaces;
using QuemVaiVai.Tests.Responses;
using QuemVaiVai.Tests.Responses.UserResponses;

namespace QuemVaiVai.Tests.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
        private readonly Mock<ILogger<UserController>> _loggerMock;
        private readonly Mock<IUserAppService> _userAppServiceMock;
        private readonly UserController _controller;

        public UserControllerTests()
        {
            _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
            _loggerMock = new Mock<ILogger<UserController>>();
            _userAppServiceMock = new Mock<IUserAppService>();

            _controller = new UserController(_httpContextAccessorMock.Object, _loggerMock.Object, _userAppServiceMock.Object);
        }

        [Fact]
        public async Task Login_WithValidCredentials_ReturnsSuccess()
        {
            // Arrange
            var dto = new UserLoginDTO
            {
                Email = "teste@exemplo.com",
                Password = "123456"
            };

            _userAppServiceMock
                .Setup(s => s.LoginAsync(dto))
                .ReturnsAsync("fake-jwt-token");

            // Act
            var result = await _controller.Login(dto) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Value);
            Assert.Equal(200, result.StatusCode);

            var response = JsonConvert.DeserializeObject<ApiResponse<LoginResponse>>(
                JsonConvert.SerializeObject(result.Value)
            );

            Assert.NotNull(response);
            Assert.True(response!.Success);
            Assert.NotNull(response.Data);

            Assert.Equal("fake-jwt-token", response.Data.Token);
        }

        [Fact]
        public async Task CreateUser_WithValidData_ReturnsSuccess()
        {
            // Arrange
            var dto = new CreateUserDTO
            {
                Name = "João da Silva",
                Email = "joao@exemplo.com",
                Password = "senha123"
            };

            var expectedUser = new UserDTO
            {
                Id = 1,
                Name = dto.Name,
                Email = dto.Email
            };

            _userAppServiceMock
                .Setup(s => s.CreateUserAsync(dto))
                .ReturnsAsync(expectedUser);

            // Act
            var result = await _controller.CreateUser(dto) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            var response = JsonConvert.DeserializeObject<ApiResponse<CreatedUserResponse>>(
                JsonConvert.SerializeObject(result.Value)
            );

            Assert.NotNull(response);
            Assert.True(response!.Success);
            Assert.NotNull(response.Data);

            Assert.Equal(expectedUser.Name, response.Data.Name);
            Assert.Equal(expectedUser.Email, response.Data.Email);
        }
    }
}
