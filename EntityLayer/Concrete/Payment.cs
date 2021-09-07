using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{

    public class Payment
    {
        public enum PaymentType { 
            CreditCard, EFT
        }

        [Key]
        public int PaymentID { get; set; }
        public PaymentType PaymnetType { get; set; }

        public int BankId { get; set; }

        public Bank Bank { get; set; }

        public string PaymentNumber { get; set; }

        public double Price { get; set; }

        public int OrderId { get; set; }

        public Order Order { get; set; }//ilişkilendirme
    }
}
