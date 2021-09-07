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
    public class OrderProductManager : IOrderProductService
    {
        IOrderProductDal _orderProductDal;

        public OrderProductManager(IOrderProductDal orderProductDal)
        {
            _orderProductDal = orderProductDal;
        }

        public OrderProduct GetById(int id)
        {
            return _orderProductDal.Get(x => x.Id == id);
        }

        public List<OrderProduct> GetList()
        {
            return _orderProductDal.List();
        }

        public void OrderProductAddBL(OrderProduct orderProduct)
        {
            throw new NotImplementedException();
        }

        public void OrderProductDelete(OrderProduct orderProduct)
        {
            throw new NotImplementedException();
        }

        public void OrderProductUpdate(OrderProduct orderProduct)
        {
            throw new NotImplementedException();
        }
    }
}
