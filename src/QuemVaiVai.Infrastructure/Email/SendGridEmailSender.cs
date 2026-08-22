using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using QuemVaiVai.Application.Interfaces.Email;
using QuemVaiVai.Infrastructure.Email;
using SendGrid;
using SendGrid.Helpers.Mail;

public class SendGridEmailSender : IEmailSender
{
    private readonly SendGridSettings _settings;
    private readonly IHostEnvironment _env;

    public SendGridEmailSender(IOptions<SendGridSettings> settings, IHostEnvironment env)
    {
        _settings = settings.Value;
        _env = env;
    }

    public async Task SendEmailAsync(string to, string subject, string bodyHtml)
    {
        var client = new SendGridClient(_settings.ApiKey);
        var from = new EmailAddress(_settings.FromEmail, _settings.FromName);
        var toAddress = new EmailAddress(to);
        var msg = MailHelper.CreateSingleEmail(from, toAddress, subject, null, bodyHtml);

        if (_env.IsDevelopment())
        {
            msg.SetClickTracking(false, false);
        }
        var response = await client.SendEmailAsync(msg);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Body.ReadAsStringAsync();
            throw new Exception($"Erro ao enviar email pelo SendGrid: {error}");
        }
    }
}
