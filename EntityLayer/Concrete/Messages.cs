using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityLayer.Concrete
{
    public class Messages
    {
        [Key]
        public int MessagesID { get; set; }
        [StringLength(250)]
        public string MessagesContext { get; set; }
        public DateTime MessagesCreated_at { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual User User { get; set; }
        
        [ForeignKey("Ticket")]
        public int TicketID { get; set; }
        public virtual Ticket Ticket { get; set; }

    }
}
