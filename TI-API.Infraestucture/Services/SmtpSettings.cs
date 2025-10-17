namespace TI_API.Infraestucture.Services
{
    public class SmtpSettings
    {
        public string SmtpServer { get; set; }

        public int SmtpPort { get; set; }

        public string SmtpUser { get; set; }

        public string SmtpPass { get; set; }

        public string FromName { get; set; }

        public string FromAddress { get; set; }

    }
}
