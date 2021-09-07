using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using BusinessLayer.Abstract;

namespace BusinessLayer.Concrete
{
    public class MailManager : IMailService
    {
        MailAddress From;
        SmtpClient SmtpClient;

        public MailManager()
        {
            this.From = new MailAddress("no-reply@buyy.com", "BUYY.com LTD. ŞTİ. E-Ticaretke");
            this.SmtpClient = new SmtpClient("10.10.0.11", 25);
            this.SmtpClient.EnableSsl = true;
            this.SmtpClient.UseDefaultCredentials = false;
            this.SmtpClient.Credentials = new System.Net.NetworkCredential("no-reply", "Buyy2021!");
        }

        public int SendEmail(MailMessageDTO m)
        {

            MailAddress to = new MailAddress(m.To);
            MailMessage _toMessage = new MailMessage(this.From, to);
            _toMessage.Body = m.Body;
            _toMessage.Subject = m.Title;
            _toMessage.IsBodyHtml = true;
            try
            {
                this.SmtpClient.Send(_toMessage);
            }
            catch (Exception e)
            {
                return 0x0004;
            }

            return 0x0000;
        }
    }
}
