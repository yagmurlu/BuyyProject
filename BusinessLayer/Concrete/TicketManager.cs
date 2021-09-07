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
    public class TicketManager : ITicketService
    {
        ITicketDal _ticketDal;
        public TicketManager(ITicketDal ticketDal)
        {
            _ticketDal = ticketDal;
        }
        public Ticket GetById(int id)
        {
            return _ticketDal.Get(x => x.TicketID == id);
        }
        public List<Ticket> GetList()
        {
            return _ticketDal.List();
        }
        public void TicketAddBL(Ticket ticket)
        {
            _ticketDal.Insert(ticket);
        }
        public void TicketDelete(Ticket ticket)
        {
            _ticketDal.Delete(ticket);
        }
        public void TicketUpdate(Ticket ticket)
        {
            _ticketDal.Update(ticket);
        }
    }
}
