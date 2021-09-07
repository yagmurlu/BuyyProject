using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Property
    {
        public int Id { get; set; }

        public int StockFieldId { get; set; }

        public StockField StockField { get; set; }

        public int StockId { get; set; }

        public Stock Stock { get; set; }

        public string Value { get; set; }

        public string Context { get; set; }
    }
}
