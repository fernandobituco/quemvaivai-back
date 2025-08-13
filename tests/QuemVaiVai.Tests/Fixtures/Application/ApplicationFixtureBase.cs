using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using QuemVaiVai.Domain.Entities;

namespace QuemVaiVai.Tests.Fixtures.Application
{
    public class ApplicationFixtureBase
    {
        public Mock<IMapper> MapperMock { get; } = new();
        public Mock<ILogger<object>> LoggerMock { get; } = new();

        public AppSettings AppSettings { get; } = new AppSettings
        {
            FRONT_END_URL = "http://localhost"
        };
    }
}
