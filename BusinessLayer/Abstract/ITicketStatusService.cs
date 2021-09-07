using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface ITicketStatusService
    {
        List<TicketStatus> GetList();
        void TicketStatusAddBL(TicketStatus ticketStatus);
        TicketStatus GetById(int id);
        void TicketStatusDelete(TicketStatus ticketStatus);
        void TicketStatusUpdate(TicketStatus ticketStatus);
    }
}
