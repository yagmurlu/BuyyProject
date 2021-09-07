using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class TicketTypeManger : ITicketTypeService
    {
        ITicketTypeDal _ticketTypeDal;
        public TicketTypeManger(ITicketTypeDal ticketTypeDal)
        {
            _ticketTypeDal = ticketTypeDal;
        }
        public TicketType GetById(int id)
        {
            return _ticketTypeDal.Get(x => x.TicketTypeID == id);
        }
        public List<TicketType> GetList()
        {
            return _ticketTypeDal.List();
        }
        public void TicketTypeAddBL(TicketType ticketType)
        {
            _ticketTypeDal.Insert(ticketType);
        }
        public void TicketTypeDelete(TicketType ticketType)
        {
            _ticketTypeDal.Delete(ticketType);
        }
        public void TicketTypeUpdate(TicketType ticketType)
        {
            _ticketTypeDal.Update(ticketType);
        }
    }
}
