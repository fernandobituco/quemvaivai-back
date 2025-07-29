using QuemVaiVai.Application.Helpers;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Application.Interfaces.Email;
using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Application.Interfaces.Security;
using QuemVaiVai.Application.Services;
using QuemVaiVai.Domain.Interfaces.Services;
using QuemVaiVai.Domain.Services;
using QuemVaiVai.Infrastructure.DapperRepositories;
using QuemVaiVai.Infrastructure.Email;
using QuemVaiVai.Infrastructure.Repositories;
using QuemVaiVai.Infrastructure.Security;

namespace QuemVaiVai.Api.Configurations;

public static class DependencyInjection
{
    public static void AddDependencyInjections(this IServiceCollection services)
    {
        // Services
        services.AddScoped<IUserAppService, UserAppService>();
        services.AddScoped<IUserService, UserService>();

        //Repositories
        services.AddScoped<IUserRepository, UserRepository>();

        //DapperRepositories
        services.AddScoped<IUserDapperRepository, UserDapperRepository>();

        //Security
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<JwtTokenGenerator>();

        //Email
        services.AddScoped<IEmailSender, SmtpEmailSender>();
        services.AddScoped<IEmailTemplateBuilder, EmailTemplateBuilder>();
    }
}