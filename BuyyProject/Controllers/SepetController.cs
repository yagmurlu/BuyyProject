using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuyyProject.Controllers
{
    public class SepetController : Controller
    {
        OrderManager om = new OrderManager(new EfOrderDal());
        // GET: Sepet
        public ActionResult Index()
        {
            var order = om.GetById(1);
            foreach (var item in order.OrderProducts)
            {

            }
            //order.OrderProducts.ToList()[0].Product
            return View(order);
        }
    }
}