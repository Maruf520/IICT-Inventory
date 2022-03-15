using System.Threading.Tasks;

namespace IICT_Store.Services.MailServices
{
    public interface IMailService
    {
        Task<string> SendEmail(string to, string subject, string body, string userName);
    }
}