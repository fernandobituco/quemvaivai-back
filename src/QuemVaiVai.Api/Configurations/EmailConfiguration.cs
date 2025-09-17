using QuemVaiVai.Infrastructure.Email;

namespace QuemVaiVai.Api.Configurations;

public static class EmailConfiguration
{
    public static void AddEmailConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
        services.Configure<SendGridSettings>(configuration.GetSection("SendGrid"));
    }
}