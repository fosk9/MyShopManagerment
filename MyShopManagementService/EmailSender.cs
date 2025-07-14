using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;

namespace MyShopManagementService
{
    public class EmailSender : IEmailSender
    {
        public bool SendEmail(string toEmail, string subject, string mess)
        {
            var fromEmail = "linhtkhe173474@fpt.edu.vn"; 
            var pass = "hqvx uylj pzjx ytjc";
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(fromEmail));
            email.To.Add(MailboxAddress.Parse(toEmail));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = mess };

            using var smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(fromEmail, pass);

            try
            {
                smtp.Send(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
            finally
            {
                smtp.Disconnect(true);
            }
        }

        public string GetOTP()
        {
            Random random = new Random();
            return random.Next(100000, 999999).ToString();
        }
    }
}




