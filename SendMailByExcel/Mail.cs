using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SendMailByExcel
{
    public class Mail
    {
        private static string mailAddress = "";
        private static string account = "";
        private static string password = "";

        public string message;
        public string title;
        public string mailTo;

        public static bool SendMail(string mailObj)
        {
            string body = string.Empty;
            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "SendMailByExcel.Resources.MailLayout.html";
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                body = reader.ReadToEnd();
            }

            string title2 = "";

            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("");

                mail.From = new MailAddress(mailAddress, "displayName");
                mail.To.Add(mailObj);
                mail.Subject = title2;
                mail.IsBodyHtml = true;
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                var assembly2 = Assembly.GetExecutingAssembly();
                var resourceName2 = "SendMailByExcel.Resources.shopLogo.png";
                Stream stream = assembly2.GetManifestResourceStream(resourceName2);
                LinkedResource logo = new LinkedResource(stream, "image/jpeg");
                logo.ContentId = "companylogo";
                logo.TransferEncoding = TransferEncoding.Base64;
                htmlView.LinkedResources.Add(logo);

                mail.AlternateViews.Add(htmlView);

                

                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential(account, password);
                SmtpServer.EnableSsl = false;
                SmtpServer.Timeout = 30000;
                SmtpServer.Send(mail);
                return true;
            }
            catch(Exception ex)
            {
                string errMsg = ex.Message;
                return false;
            }
        }
    }
}
