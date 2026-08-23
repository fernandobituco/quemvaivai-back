using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using QuemVaiVai.Application.Interfaces.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Infrastructure.Email
{
    public class BrevoEmailSender : IEmailSender
    {
        private readonly BrevoSettings _settings;
        private readonly IHostEnvironment _env;
        private readonly HttpClient _httpClient;

        public BrevoEmailSender(IOptions<BrevoSettings> settings, IHostEnvironment env, HttpClient httpClient)
        {
            _settings = settings.Value;
            _env = env;
            _httpClient = httpClient;
        }
        public async Task SendEmailAsync(string toEmail, string subject, string bodyHtml)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                "https://api.brevo.com/v3/smtp/email"
            );

            request.Headers.Add("api-key", _settings.ApiKey);

            var payload = new
            {
                sender = new
                {
                    name = _settings.FromName,
                    email = _settings.FromEmail
                },
                to = new[]
                {
            new
            {
                email = toEmail
            }
        },
                subject,
                htmlContent = bodyHtml
            };

            request.Content = JsonContent.Create(payload);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                throw new Exception(
                    $"Erro ao enviar email pelo Brevo: {error}"
                );
            }
        }
    }
}
