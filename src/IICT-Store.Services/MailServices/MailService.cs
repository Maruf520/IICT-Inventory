using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace IICT_Store.Services.MailServices
{
    public class MailService : IMailService
    {
        private readonly IConfiguration configuration;

        public MailService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<string> SendEmail(string to, string subject, string body, string userName)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("IICT Inventory", "md.maruf5201@gmail.com"));
            message.To.Add(new MailboxAddress(userName, to));
            message.Subject = subject;
            message.Body = new TextPart("plain")
            {
                Text = body,
            };
            using var client = new SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync("iict.sust.edu@gmail.com", "MahiKeya0124");

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
            return "Sent!";
        }
    }
}