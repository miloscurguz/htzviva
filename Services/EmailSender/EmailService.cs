using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services.EmailSender
{
    public class EmailSender : IEmailSender
    {
        public AuthMessageSenderOptions Options { get; } //Set with Secret Manager.

        public EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }


        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            //if (string.IsNullOrEmpty(Options.SendGridKey))
            //{
            //    throw new Exception("Null SendGridKey");
            //}
            await Execute(Options.SendGridKey, subject, message, toEmail);
        }

        public async Task Execute(string apiKey, string subject, string message, string toEmail)
        {
            apiKey = "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";
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("milos.curguz.mc@gmail.com", "Password Recovery"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(toEmail));

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);
            var response = await client.SendEmailAsync(msg);
            
        }
    }
}
