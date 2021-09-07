using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public Messages GetById(int id)
        {
            return _messageDal.Get(x => x.MessagesID == id);
        }

        public List<Messages> GetList()
        {
            return _messageDal.List();
        }

        public void MessageAddBL(Messages message)
        {
            _messageDal.Insert(message);
        }

        public void MessageDelete(Messages message)
        {
            _messageDal.Delete(message);
        }

        public void MessageUpdate(Messages message)
        {
            _messageDal.Update(message);
        }

    }
}
