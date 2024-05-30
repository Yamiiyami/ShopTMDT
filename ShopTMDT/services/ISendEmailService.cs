using System.Net;
using System.Net.Mail;

namespace ShopTMDT.services
{
    public class EmaiModel
    {
        public string? ToEmail { get; set; }
        public string subject { get; set; }
        public string body { get; set; }

    }
    public interface ISendEmailService
    {
        public bool SendEmail(EmaiModel email);
    }
    public class SenEmailService : ISendEmailService
    {
        private IConfiguration _configuration;
        public SenEmailService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }
        public bool SendEmail(EmaiModel email)
        {
            try
            {
                string username = _configuration["Gmail:Username"]!;
                string password = _configuration["Gmail:Password"]!;

                var smtp = new SmtpClient()
                {
                    Host = _configuration["Gmail:Host"]!,
                    Port = int.Parse(_configuration["Gmail:Post"]!),
                    EnableSsl = bool.Parse(_configuration["Gmail:SMTP:starttls:enable"]!),
                    Credentials = new NetworkCredential(username, password)
                };

                var message = new MailMessage(username, email.ToEmail!)
                {
                    Subject = email.subject,
                    Body = email.body,
                    IsBodyHtml = true
                };

                smtp.Send(message);
                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
