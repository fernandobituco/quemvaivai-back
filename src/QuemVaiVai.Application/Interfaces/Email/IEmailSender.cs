using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuemVaiVai.Application.Interfaces.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toEmail, string subject, string bodyHtml);
    }
}
