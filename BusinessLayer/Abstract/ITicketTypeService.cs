using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface ITicketTypeService
    {
        List<TicketType> GetList();
        void TicketTypeAddBL(TicketType ticketType);
        TicketType GetById(int id);
        void TicketTypeDelete(TicketType ticketType);
        void TicketTypeUpdate(TicketType ticketType);
    }
}
