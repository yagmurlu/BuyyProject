using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{

    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }
        public DateTime? TicketClosed_at { get; set; }
        public DateTime? TicketCreated_at { get; set; }
        [ForeignKey("TicketStatus")]
        public int TicketStatusID { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }
        [ForeignKey("TicketType")]
        public int TicketTypeID { get; set; }
        public virtual TicketType TicketType { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Messages> Messages { get; set; }
    }
}
