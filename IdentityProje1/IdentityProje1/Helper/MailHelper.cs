using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace IdentityProje1.Helper
{
    public static class MailHelper
    {
        public static void SendMail(string body, string to, string subject, bool ishtml = true)
        {

            try
            {
                SmtpClient mailcleint = new SmtpClient("smtp.gmail.com", 587);
                NetworkCredential cred = new NetworkCredential("superkahranman@gmail.com", "yourpassword");
                mailcleint.Credentials = cred;
                MailMessage mmsj = new MailMessage();
                mmsj.From = new MailAddress("superkahranman@gmail.com");
                mmsj.Subject = subject;
                mmsj.Body = $"<a href='{body}'>tiklayin</a>";
                //mailcleint.UseDefaultCredentials = false;
                mailcleint.EnableSsl = false;
                mmsj.To.Add(to);
                mailcleint.Send(mmsj);
            }
            catch (Exception)
            {
                //Console.WriteLine("hata var");
                throw;
            }


        }
    }
}
