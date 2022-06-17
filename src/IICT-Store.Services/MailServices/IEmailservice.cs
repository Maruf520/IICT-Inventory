using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IICT_Store.Services.MailServices
{
    public interface IEmailservice
    {
        Task SendEmailAsync(Message message);
    }
}
