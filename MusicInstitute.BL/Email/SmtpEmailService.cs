using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MusicInstitute.BL.Email
{
    public class SmtpEmailService : IEmailService
    {
        private readonly string _smtpHost = "smtp.yourprovider.com";
        private readonly int _smtpPort = 587;
        private readonly string _smtpUser = "naama6718@gmail.com";
        private readonly string _smtpPass = "lerploujqwgnctql";

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var mail = new MailMessage();
            mail.From = new MailAddress(_smtpUser);
            mail.To.Add(to);
            mail.Subject = subject;
            mail.Body = body;

            using (var smtp = new SmtpClient(_smtpHost, _smtpPort))
            {
                smtp.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                smtp.EnableSsl = true;

                await smtp.SendMailAsync(mail);
            }
        }
    }
}
