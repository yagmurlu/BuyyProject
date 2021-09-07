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
    public class TicketStatusManger : ITicketStatusService
    {
        ITicketStatusDal _ticketStatusDal;
        public TicketStatusManger(ITicketStatusDal ticketStatusDal)
        {
            _ticketStatusDal = ticketStatusDal;
        }
        public TicketStatus GetById(int id)
        {
            return _ticketStatusDal.Get(x => x.StatusID == id);
        }
        public List<TicketStatus> GetList()
        {
            return _ticketStatusDal.List();
        }
        public void TicketStatusAddBL(TicketStatus ticketStatus)
        {
            _ticketStatusDal.Insert(ticketStatus);
        }
        public void TicketStatusDelete(TicketStatus ticketStatus)
        {
            _ticketStatusDal.Delete(ticketStatus);
        }
        public void TicketStatusUpdate(TicketStatus ticketStatus)
        {
            _ticketStatusDal.Update(ticketStatus);
        }
    }
}
