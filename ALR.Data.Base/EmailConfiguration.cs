namespace ALR.Data.Base
{
    public class EmailConfiguration
    {
        public string FromMail { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SmtpServer { get; set; }
        public int Port { get; set; }
    }

    public class SendEmailMessageDto
    {
        public string ToMail { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }

    public class ConfigSocket
    {
        public string IpAddress { get; set; }
        public int Port { get; set; }
    }
}
