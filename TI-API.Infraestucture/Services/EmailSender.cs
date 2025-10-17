using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using TI_API.Application.Common.Interfaces;

namespace TI_API.Infraestucture.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpSettings _smtpSettings;

        public EmailSender(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string message)
        {
            var mail = new MailMessage
            {
                From = new MailAddress(_smtpSettings.FromAddress, _smtpSettings.FromName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true,

            };
            mail.To.Add(toEmail);

            using var smtpClient = new SmtpClient(_smtpSettings.SmtpServer, _smtpSettings.SmtpPort)
            {
                Credentials = new NetworkCredential(_smtpSettings.SmtpUser, _smtpSettings.SmtpPass),
                EnableSsl = true
            };

            await smtpClient.SendMailAsync(mail);
        }
    }
}
