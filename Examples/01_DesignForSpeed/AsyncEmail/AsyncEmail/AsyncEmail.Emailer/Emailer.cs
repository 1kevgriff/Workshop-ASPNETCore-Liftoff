using System.Net;
using System.Net.Mail;

namespace AsyncEmail.Emailer
{
    public class Emailer
    {
        public async Task SendEmail(string subject,string body, string toEmailAddress, string fromEmailAddress)
        {
            // Using Mailhog 
            var smtpServer = "localhost";
            var smtpPort = 1025;
            var smtpUsername = "";
            var smtpPassword = "";

            using (var smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);

                var email = new MailMessage(fromEmailAddress, toEmailAddress)
                {
                    Subject = subject,
                    Body = body,
                };

                await smtpClient.SendMailAsync(email, CancellationToken.None);
            }
        }
    }
}