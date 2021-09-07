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
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public Order GetById(int id)
        {
            return _orderDal.Get(x => x.OrderID == id);
        }

        public List<Order> GetList()
        {
            return _orderDal.List();
        }

        public void OrderAddBL(Order order)
        {
            _orderDal.Insert(order);
        }

        public void OrderDelete(Order order)
        {
            _orderDal.Delete(order);
        }

        public void OrderUpdate(Order order)
        {
            _orderDal.Update(order);
        }
    }
}
