using QuemVaiVai.Application.Interfaces;
using QuemVaiVai.Application.Services;
using QuemVaiVai.Domain.Interfaces.Services;
using QuemVaiVai.Domain.Services;

namespace QuemVaiVai.Api.Configurations;

public static class DependencyInjection
{
    public static void AddDependencyInjections(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IUserAppService, UserAppService>();
        services.AddScoped<IUserService, UserService>();
    }
}