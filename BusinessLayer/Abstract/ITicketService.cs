using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface ITicketService
    {
        List<Ticket> GetList();
        void TicketAddBL(Ticket ticket);
        Ticket GetById(int id);
        void TicketDelete(Ticket ticket);
        void TicketUpdate(Ticket ticket);
    }
}
