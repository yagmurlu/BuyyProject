using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Image> Images { get; set; }
        public double ProductPrice { get; set; }

        [StringLength(30)]
        public string ProductGender { get; set; }

        [StringLength(100)]
        public string ProductDescription { get; set; }
        public bool ProductStatus { get; set; }
        public int CategoryID { get; set; }//İlişkilendirme-1
        public virtual Category Category { get; set; }
        
        public ICollection<Stock> Stocks { get; set; }
    }
}
