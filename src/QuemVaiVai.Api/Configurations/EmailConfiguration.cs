using QuemVaiVai.Application.Interfaces.Email;
using QuemVaiVai.Infrastructure.Email;
using QuemVaiVai.Infrastructure.Factories;

namespace QuemVaiVai.Api.Configurations;

public static class EmailConfiguration
{
    public static void AddEmailConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<SmtpSettings>(configuration.GetSection("SmtpSettings"));
        services.Configure<SendGridSettings>(configuration.GetSection("SendGrid"));

        services.AddTransient<IEmailSender>(sp =>
        {
            var factory = sp.GetRequiredService<EmailSenderFactory>();
            return factory.Create();
        });
    }
}