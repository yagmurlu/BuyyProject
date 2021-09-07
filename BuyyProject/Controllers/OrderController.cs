using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuyyProject.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        OrderManager om = new OrderManager(new EfOrderDal());
       
        public ActionResult Index()
        {
            
            var orderValues = om.GetList();
            return View(orderValues);
        }
        [HttpGet]
        public ActionResult EditOrder(int id)
        {
            var categoryvalue = om.GetById(id);
            return View(categoryvalue);
        }
        [HttpPost]
        public ActionResult EditOrder(Order p)
        {
            om.OrderUpdate(p);
            return RedirectToAction("Index");
        }
        public ActionResult OrderDelete(int id)
        {
            var orderValue = om.GetById(id);
            om.OrderDelete(orderValue);
            return RedirectToAction("Index"); // Go to INDEX
        }
        [HttpGet]
        public PartialViewResult OrderPartial(int id)
        {
            var orderValue = om.GetById(id);
            return PartialView(orderValue);
      
        }
    }
}