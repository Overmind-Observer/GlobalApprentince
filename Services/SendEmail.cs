using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Global_Intern.Models;
using Global_Intern.Models.Filters;
using Global_Intern.Util;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace Global_Intern.Services
{
    public class SendEmail
    {
        private EmailSettings _settings;



        
        public SendEmail(EmailSettings settings)
        {
            _settings = settings;
        }

       

        public void SendEmailtoUser(string fullname, string email, string subject, string body)
        {
            
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_settings.SenderName, _settings.Sender));
            message.To.Add(new MailboxAddress(fullname, email));

            message.Subject = subject;
            // html can be added
            message.Body = new TextPart("plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                //client.SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;
                

                client.Connect(_settings.MailServer, _settings.MailPort, true);
                client.Authenticate(_settings.Sender, _settings.Password);
                client.Send(message);
                client.Disconnect(true);
                
            }
        }
    }
}
