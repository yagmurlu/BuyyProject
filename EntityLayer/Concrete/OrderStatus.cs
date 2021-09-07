using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class OrderStatus
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }

        /**
         * Creation time for entity
         */
        public DateTime CreatedAt { get; set; }

        /**
         * Update time for entity
         * */
        public DateTime UpdatedAt { get; set; }

        /**
         * Time information for user
         */
        public DateTime Time { get; set; }

        public string Context { get; set; }

        public int UpdatedById { get; set; }

        public User UpdatedBy { get; set; }
    }
}
