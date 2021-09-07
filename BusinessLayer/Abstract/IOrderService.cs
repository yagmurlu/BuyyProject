using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IOrderService
    {
        List<Order> GetList();
        void OrderAddBL(Order order);
        Order GetById(int id);
        void OrderDelete(Order order);
        void OrderUpdate(Order order);
    }
}
