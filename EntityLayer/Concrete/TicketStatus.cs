using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class TicketStatus
    {
        [Key]
        public int StatusID { get; set; }
        public string StatusTitle { get; set; }
        public string StatusDescription { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
