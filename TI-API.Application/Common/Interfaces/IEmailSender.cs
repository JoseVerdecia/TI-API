namespace TI_API.Application.Common.Interfaces
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string toEmail, string subject, string message);
    }
}
