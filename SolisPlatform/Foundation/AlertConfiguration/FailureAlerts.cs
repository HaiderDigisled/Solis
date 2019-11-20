using Miscellaneous.Foundation;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Foundation.AlertConfiguration
{
    public class FailureAlerts
    {
        List<string> InternalRecipients;
        public FailureAlerts()
        {
            InternalRecipients = new List<string>(Settings.FailureAlerts.InternalRecipients.Split(','));
        }
        public void SendEmail(string subject, string body)
        {

            MailMessage mm = new MailMessage();

            mm.From = new MailAddress("info.rapidosolution@gmail.com");
            string password = "Rapido123";

            InternalRecipients.ForEach(x => mm.To.Add(x.Trim()));

            mm.Subject = string.Concat("Failure Alert - ", subject);
            mm.Body = body;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.EnableSsl = true;
            NetworkCredential NetworkCred = new NetworkCredential(mm.From.Address, password);
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = NetworkCred;
            smtp.Port = 587;
            smtp.Send(mm);
            Console.WriteLine("Failure Alert Sent");
        }
    }
}
