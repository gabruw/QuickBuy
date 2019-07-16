using Microsoft.Extensions.Options;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Auxiliary.Email
{
    public class SendEmail : IEmailSender
    {
        public SendEmail(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public EmailSettings _emailSettings { get; }

        /// <summary>
        /// Execute the email send without attachment
        /// </summary>
        /// <param name="email">Recipient's email address</param>
        /// <param name="subject">Subject about the email</param>
        /// <param name="message">Message in escope</param>
        /// <returns></returns>
        private async Task Execute(string email, string subject, string message)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UserEmail, "QuickBuy")
                };

                mail.To.Add(new MailAddress(email));
                mail.CC.Add(new MailAddress(_emailSettings.Cc));

                mail.Subject = "QuickBuy - " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UserEmail, _emailSettings.UserPassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Execute the email send with attachment 
        /// </summary>
        /// <param name="email">Recipient's email address</param>
        /// <param name="subject">Subject about the email</param>
        /// <param name="message">Message in escope</param>
        /// <param name="archive">Attachment in the email</param>
        /// <param name="contentType">Attachment content type</param>
        /// <returns></returns>
        private async Task Execute(string email, string subject, string message, System.IO.Stream archive, ContentType contentType)
        {
            try
            {
                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UserEmail, "QuickBuy")
                };

                mail.To.Add(new MailAddress(email));
                mail.CC.Add(new MailAddress(_emailSettings.Cc));

                mail.Subject = "QuickBuy - " + subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;
                mail.Attachments.Add(new Attachment(archive, contentType));

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UserEmail, _emailSettings.UserPassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Send email without attachment
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public Task SendEmailAsync(string email, string subject, string message)
        {
            try
            {
                Execute(email, subject, message).Wait();

                return Task.FromResult(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Send email with attachment
        /// </summary>
        /// <param name="email"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="archive"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public Task SendEmailAsync(string email, string subject, string message, System.IO.Stream archive, ContentType contentType)
        {
            try
            {
                Execute(email, subject, message, archive, contentType).Wait();

                return Task.FromResult(0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}