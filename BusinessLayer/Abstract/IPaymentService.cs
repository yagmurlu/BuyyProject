using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IPaymentService
    {
        List<Payment> GetList();
        void PaymentAddBL(Payment payment);
        Payment GetById(int id);
        void PaymentDelete(Payment payment);
        void PaymentUpdate(Payment payment);
    }
}
