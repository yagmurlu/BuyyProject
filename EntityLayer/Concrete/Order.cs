using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        public int Number { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public double TotalOrderPrice { get; set; }

        public ICollection<Payment> Payments { get; set; }

        public int UserId { get; set; }//Profile-order ilişkisi

        public virtual User User { get; set; }

        public int AddressId { get; set; }

        public virtual Address Address { get; set; }

        public ICollection<OrderStatus> OrderStatuses { get; set; }

        public virtual ICollection<OrderProduct> OrderProducts {get; set;}
    }
}
