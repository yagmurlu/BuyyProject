using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuyyProject.Controllers
{
    public class ProductsController : Controller
    {
        public class ProductFilter // WTF
        {
            int CategoryID = -1;
            int MinValue = -1;
            int MaxValue = -1;
            //TODO: Checkbox Data?
            List<bool> SelectedColors;
            List<bool> SelectedSizes;
        }

        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        ProductManager pm = new ProductManager(new EfProductDal());
        StockManager sm = new StockManager(new EfStockDal());

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Stocks = sm.GetList();
            ViewBag.Categories = cm.GetList();
            ViewBag.MaxPrice = Math.Ceiling(cm.MostExpensive(null)); //TODO: Take Selected Category Here.


            var productValues = pm.GetList();
            return View(productValues);
        }

        [HttpGet]
        public ActionResult ChangeCategory(int cat)
        {
            ViewBag.Stocks = sm.GetList();
            ViewBag.Categories = cm.GetList();
            ViewBag.MaxPrice = Math.Ceiling(cm.MostExpensive(cm.GetById(ViewBag.Categories[cat])));


            var productValues = pm.GetList(ViewBag.Categories[cat]);
            return View(productValues);
        }
    }
}