﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class TicketType
    {
        [Key]
        public int TicketTypeID { get; set; }
        public string TicketTitle { get; set; }
        public string TicketDescription { get; set; }
        public ICollection<Ticket> Tickets { get; set; }

    }
}
