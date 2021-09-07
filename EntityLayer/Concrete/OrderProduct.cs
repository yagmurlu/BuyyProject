using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class OrderProduct
    {
        [Key]
        public int Id { get; set; }

        public int OrderId { get; set; }

        [Required]
        public virtual Order Order { get; set; }

        public int ProductId { get; set; }

        public int Count { get; set; }

        [Required]
        public virtual Product Product { get; set; }
        public double Price { get; set; }
    }
}
