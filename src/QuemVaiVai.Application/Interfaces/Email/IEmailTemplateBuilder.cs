using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Application.Interfaces.Email
{
    public interface IEmailTemplateBuilder
    {
        Task<string> BuildTemplateAsync(string templateName, Dictionary<string, string> placeholders);
    }
}
