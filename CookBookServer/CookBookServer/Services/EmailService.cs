using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace CookBookServer.Services
{
    public static class EmailService
    {
        private static readonly string _smtpDisplayName = "Cookbook";
        /*private static readonly string _smtpPassword = "diqyminwmvduqaly";*/
        private static readonly string _smtpPassword = "01071997";
        private static readonly string _smtpEmail = "cookbooc@yandex.ru";
        private static readonly string _smtpHost = "smtp.yandex.ru";
        private static readonly int _smtpPort = 465;

        public static void Send(Email email)
        {
            using (var message = new MailMessage())
            {
                message.From = new MailAddress(_smtpEmail, _smtpDisplayName);
                foreach (var mail in email.Recipients)
                    message.To.Add(mail);

                foreach (var mail in email.ReplyToRecipients)
                    message.ReplyToList.Add(mail);

                message.Subject = email.Subject;
                message.Body = email.Body;
                message.IsBodyHtml = email.IsBodyHtml;

                using (var smtp = new SmtpClient(_smtpHost, _smtpPort))
                {                    
                    smtp.Credentials = new NetworkCredential(_smtpEmail, _smtpPassword);
                    smtp.EnableSsl = true;
                    smtp.Send(message);
                }
            }
        }
    }


    public class Email
    {
        public Email()
        {
            Recipients = new List<string>();
            ReplyToRecipients = new List<string>();
        }

        public string Subject { get; set; }

        public string Body { get; set; }

        public List<string> Recipients { get; set; }

        public List<string> ReplyToRecipients { get; set; }

        public bool IsBodyHtml { get; set; }
    }
}

