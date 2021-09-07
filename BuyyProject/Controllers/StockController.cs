using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuyyProject.Controllers
{
    public class StockController : Controller
    {
        // GET: Stock
        StockManager sm = new StockManager(new EfStockDal());
        ProductManager pm = new ProductManager(new EfProductDal());
        StockValidator stockValidator = new StockValidator();

        public ActionResult Index()
        {
            var stockValues = sm.GetList();
            return View(stockValues);
        }
        [HttpGet]
        public ActionResult AddStock()
        {
             List<SelectListItem> valueStock = (from x in pm.GetList()
                                                  select new SelectListItem
                                                  {
                                                      Text = x.Name,
                                                      Value = x.ProductID.ToString()
                                                  }).ToList();
            

        
            ViewBag.vlc = valueStock;
            return View();
        }
        [HttpPost]
        public ActionResult AddStock(Stock p)
        {

            ValidationResult results = stockValidator.Validate(p);
            if (results.IsValid)
            {
                sm.StockAddBL(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult EditStock(int id)
        {
            List<SelectListItem> valueStock = (from x in pm.GetList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.Name,
                                                     Value = x.ProductID.ToString()
                                                 }).ToList();
            ViewBag.vsb = valueStock;
            return View();
        }
        [HttpPost]
        public ActionResult EditStock(Stock p)
        {
            //pm.ProductUpdate(p);
            //return RedirectToAction("Index");

            ValidationResult results = stockValidator.Validate(p);
            if (results.IsValid)
            {
                sm.StockUpdate(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        public ActionResult DeleteStock(int id)
        {
            var stockValue = sm.GetById(id);
            sm.StockDelete(stockValue);
            return RedirectToAction("Index"); // Go to INDEX
        }

    }
}