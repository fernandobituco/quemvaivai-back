using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using QuemVaiVai.Application.Interfaces.Email;
using QuemVaiVai.Infrastructure.Email;
using SendGrid;
using SendGrid.Helpers.Mail;

public class SendGridEmailSender : IEmailSender
{
    private readonly SendGridSettings _settings;

    public SendGridEmailSender(IOptions<SendGridSettings> settings)
    {
        _settings = settings.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string bodyHtml)
    {
        var client = new SendGridClient(_settings.ApiKey);
        var from = new EmailAddress(_settings.FromEmail, _settings.FromName);
        var toAddress = new EmailAddress(to);
        var msg = MailHelper.CreateSingleEmail(from, toAddress, subject, null, bodyHtml);

        var response = await client.SendEmailAsync(msg);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Body.ReadAsStringAsync();
            throw new Exception($"Erro ao enviar email pelo SendGrid: {error}");
        }
    }
}
