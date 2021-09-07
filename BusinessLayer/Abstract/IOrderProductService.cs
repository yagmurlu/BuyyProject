using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IOrderProductService
    {
        List<OrderProduct> GetList();
        void OrderProductAddBL(OrderProduct orderProduct);
        OrderProduct GetById(int id);
        void OrderProductDelete(OrderProduct orderProduct);
        void OrderProductUpdate(OrderProduct orderProduct);
    }
}
