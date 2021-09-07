using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuyyProject.Controllers
{
    public class OrderProductController : Controller
    {
        // GET: OrderProduct
        OrderProductManager opm = new OrderProductManager(new EfOrderProductDal());
        public ActionResult Index()
        {
            var orderValues = opm.GetList();
            return View(orderValues);
        }
    }
}