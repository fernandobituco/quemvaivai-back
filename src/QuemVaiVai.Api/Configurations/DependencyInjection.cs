using QuemVaiVai.Application.Interfaces.Contexts;
using QuemVaiVai.Application.Interfaces.DapperRepositories;
using QuemVaiVai.Application.Interfaces.Email;
using QuemVaiVai.Application.Interfaces.Repositories;
using QuemVaiVai.Application.Interfaces.Security;
using QuemVaiVai.Application.Interfaces.Services;
using QuemVaiVai.Application.Services;
using QuemVaiVai.Domain.Interfaces.Services;
using QuemVaiVai.Domain.Services;
using QuemVaiVai.Infrastructure.Contexts;
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
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IEmailConfirmationTokenService, EmailConfirmationTokenService>();

        // AppServices
        services.AddScoped<IEmailConfirmationTokenAppService, EmailConfirmationTokenAppService>();
        services.AddScoped<IGroupAppService, GroupAppService>();
        services.AddScoped<IGroupUserAppService, GroupUserAppService>();

        //Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEmailConfirmationTokenRepository, EmailConfirmationTokenRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<IGroupRepository, GroupRepository>();
        services.AddScoped<IGroupUserRepository, GroupUserRepository>();

        //DapperRepositories
        services.AddScoped<IUserDapperRepository, UserDapperRepository>();
        services.AddScoped<IEmailConfirmationTokenDapperRepository, EmailConfirmationTokenDapperRepository>();
        services.AddScoped<IGroupDapperRepository, GroupDapperRepository>();
        services.AddScoped<IGroupUserDapperRepository, GroupUserDapperRepository>();

        //Security
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ITokenGenerator, JwtTokenGenerator>();

        //Email
        services.AddScoped<IEmailSender, SmtpEmailSender>();
        services.AddScoped<IEmailTemplateBuilder, EmailTemplateBuilder>();

        //Contexts
        services.AddScoped<IUserContext, UserContext>();
    }
}