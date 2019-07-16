using System.Threading.Tasks;

namespace Auxiliary.Email
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}