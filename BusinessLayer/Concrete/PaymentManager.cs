using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class PaymentManager : IPaymentService
    {
        IPaymentDal _paymentDal;

        public PaymentManager(IPaymentDal paymentDal)
        {
            _paymentDal = paymentDal;
        }

        public Payment GetById(int id)
        {
            return _paymentDal.Get(x => x.PaymentID == id);
        }

        public List<Payment> GetList()
        {
            return _paymentDal.List();
        }

        public void PaymentAddBL(Payment payment)
        {
            _paymentDal.Insert(payment);
        }

        public void PaymentDelete(Payment payment)
        {
            _paymentDal.Delete(payment);
        }

        public void PaymentUpdate(Payment payment)
        {
            _paymentDal.Update(payment);
        }
    }
}
