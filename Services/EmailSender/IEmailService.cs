using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.EmailSender
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
