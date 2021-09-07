using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EntityLayer.Concrete
{
    public class StockField
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [RegularExpression("/^[a - zA - Z0 - 9][a - zA - Z0 - 9_ -]{0, 64}[a-zA-Z0-9]$/", ErrorMessage = "Invalid tag for Stock Property!")]
        public string Tag { get; set; }

    }
}
