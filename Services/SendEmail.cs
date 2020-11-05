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

                //client.SSLConfiguration.EnabledSslProtocols = SslProtocols.Ssl3;
                client.Connect(_settings.MailServer, _settings.MailPort, true);
                client.Authenticate(_settings.Sender, _settings.Password);
                client.Send(message);
                client.Disconnect(true);

            }
        }
    //    bool ServerCertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
    //    {
    //        if (sslPolicyErrors == SslPolicyErrors.None)
    //            return true;

    //        // Note: The following code casts to an X509Certificate2 because it's easier to get the
    //        // values for comparison, but it's possible to get them from an X509Certificate as well.
    //        if (certificate is X509Certificate2 certificate2)
    //        {
    //            var cn = certificate2.GetNameInfo(X509NameType.SimpleName, false);
    //            var fingerprint = certificate2.Thumbprint;
    //            var serial = certificate2.SerialNumber;
    //            var issuer = certificate2.Issuer;

    //            return cn == "imap.gmail.com" && issuer == "CN=GTS CA 1O1, O=Google Trust Services, C=US" &&
    //            serial == "0096768414983DDE9C0800000000320A68" &&
    //            fingerprint == "A53BA86C137D828618540738014F7C3D52F699C7";
    //        }

    //        return false;
    //    }
    }
}
