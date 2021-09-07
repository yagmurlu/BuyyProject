using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLayer.Concrete;

namespace BuyyProject.Controllers
{
    public class HomeController : Controller
    {

        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        ProductManager pm = new ProductManager(new EfProductDal());

        public ActionResult Index()
        {
            var productValues = pm.GetList();

            productValues.Sort(
                delegate (Product a, Product b)
                {
                    int i = a.CategoryID.CompareTo(b.CategoryID);
                    if(i == 0)
                    {
                        i = a.Name.CompareTo(b.Name);
                    }
                    return i;
                }
            );

            return View(productValues);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


    }
}