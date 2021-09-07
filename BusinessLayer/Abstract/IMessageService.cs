using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMessageService
    {
        List<Messages> GetList();
        void MessageAddBL(Messages messages);
        Messages GetById(int id);
        void MessageDelete(Messages messages);
        void MessageUpdate(Messages messages);
    }
}
