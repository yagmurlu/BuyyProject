using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMailService
    {
        int SendEmail(MailMessageDTO m);
    }

    public class MailMessageDTO
    {
        public string To;
        public string Body;
        public string Title;
        public string Summary;
    }
}
