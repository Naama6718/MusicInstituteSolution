using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace MusicInstitute.BL.Email
{
    public class SmtpEmailService : IEmailService
    {
        private readonly SmtpSettings _settings;

        public SmtpEmailService(IOptions<SmtpSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var mail = new MailMessage
            {
                From = new MailAddress(_settings.UserName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mail.To.Add(to);

            using (var smtp = new SmtpClient(_settings.Host, _settings.Port))
            {
                smtp.Credentials = new NetworkCredential(_settings.UserName, _settings.Password);
                smtp.EnableSsl = _settings.EnableSsl;

                await smtp.SendMailAsync(mail);
            }
        }
    }
}
