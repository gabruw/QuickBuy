using System.Threading.Tasks;

namespace Auxiliary.Email
{
    public class EmailMessage
    {
        public async Task RecoveryPasswordAsync(string email, IEmailSender emailSender)
        {
            var subject = "QuickBuy - Password Recovery";
            var message = string.Format("Hello {0}, \nAccess this link to change your password: {1}");

            await emailSender.SendEmailAsync(email, subject, message);
        }

        public async Task ConfirmEmailAsync(string email, IEmailSender emailSender)
        {
            var subject = "QuickBuy - Confirm Email";
            var message = string.Format("Hello {0}, \nAccess this link to confirm your register: {1}");

            await emailSender.SendEmailAsync(email, subject, message);
        }
    }
}