using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuemVaiVai.Application.Interfaces.Email;
using QuemVaiVai.Infrastructure.Email;

namespace QuemVaiVai.Infrastructure.Factories
{
    public class EmailSenderFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public EmailSenderFactory(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        public IEmailSender Create()
        {
            var provider = _configuration.GetValue<string>("Email:Provider") ?? "Smtp";

            return provider.ToLower() switch
            {
                "smtp" => _serviceProvider.GetRequiredService<SmtpEmailSender>(),
                "sendgrid" => _serviceProvider.GetRequiredService<SendGridEmailSender>(),
                //"mailgun" => _serviceProvider.GetRequiredService<MailgunEmailSender>(),
                _ => throw new Exception($"Provedor de email não suportado: {provider}")
            };
        }
    }
}
