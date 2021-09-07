using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Stock
    {
        [Key]
        public int StockID { get; set; }

        public int StockQuantity { get; set; }

        public int ProductId { get; set; }

        public virtual Product Product { get; set; }
      
        public ICollection<Property> Properties { get; set; }
    }
}
