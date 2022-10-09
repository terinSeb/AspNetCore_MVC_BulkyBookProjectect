using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.AspNetCore.
namespace BulkyBook.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly SendGridOptions sendGridOptions;
        public EmailSender(IOptions<SendGridOptions> emailOptions)
        {
            sendGridOptions = emailOptions.Value;
        }
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return Execute(sendGridOptions.SendGridUser,sendGridOptions.SendGridKey, email, subject, htmlMessage);
        }
        
        private  Task Execute(string SendGridUser,string SendGridKey ,string email, string subject, string message)
        {
            try
            {
                //var apiKey = Environment.GetEnvironmentVariable(SendGridUser);
                var client = new SendGridClient("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");
                var from = new EmailAddress("terin01@gmail.com", "Bulky Books");
                var to = new EmailAddress(email, "End User");
                var msg = MailHelper.CreateSingleEmail(from, to, subject, "", message);
                return client.SendEmailAsync(msg);
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
