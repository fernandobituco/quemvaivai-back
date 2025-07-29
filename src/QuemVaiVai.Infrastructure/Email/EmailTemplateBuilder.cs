using Microsoft.AspNetCore.Hosting;
using QuemVaiVai.Application.Interfaces.Email;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Infrastructure.Email
{
    public class EmailTemplateBuilder : IEmailTemplateBuilder
    {
        private readonly string _templatesPath;

        public EmailTemplateBuilder(IWebHostEnvironment webHostEnvironment)
        {
            _templatesPath = Path.Combine(webHostEnvironment.ContentRootPath, "Templates");
        }

        public async Task<string> BuildTemplateAsync(string templateName, Dictionary<string, string> placeholders)
        {
            var filePath = Path.Combine(_templatesPath, $"{templateName}.html");

            if (!File.Exists(filePath))
                throw new FileNotFoundException($"Template '{templateName}' não encontrado em {_templatesPath}");

            var template = await File.ReadAllTextAsync(filePath, Encoding.UTF8);

            foreach (var kvp in placeholders)
            {
                template = template.Replace($"{{{{{kvp.Key}}}}}", kvp.Value);
            }

            return template;
        }
    }
}
