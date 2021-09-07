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
using BusinessLayer.Abstract;

namespace BuyyProject.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        ProductManager pm = new ProductManager(new EfProductDal());
        CategoryManager cm = new CategoryManager(new EfCategoryDal());
        //CategoryManager cm = new CategoryManager(new EfCategoryDal());
        ProductValidator productvalidator = new ProductValidator();

        IUserService uservice;


        public ProductController(IUserService userService) {
            this.uservice = userService;
        }
            
        public ActionResult Index()
        {
            if (uservice.HasPermission((User)Session["User"], "app.products.manage"))
            {
                var productValues = pm.GetList();
                return View(productValues);
            }
            return Redirect("/");
        }
        [HttpGet]
        public ActionResult AddProduct()
        {
            List<SelectListItem> valueProduct = (from x in cm.GetList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.CategoryName,
                                                     Value = x.CategoryID.ToString()
                                                 }).ToList();



            ViewBag.vlc = valueProduct;
            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Product p)
        {
            if (uservice.HasPermission((User) Session["User"], "app.products.manage")) { 
            
                ValidationResult results = productvalidator.Validate(p);
                if (results.IsValid)
                {
                    pm.ProductAddBL(p);
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
            return Redirect("/");
        }
        [HttpGet]
        public ActionResult EditProduct(int id)
        {
            if (uservice.HasPermission((User)Session["User"], "app.products.manage")) {
                var productValue = pm.GetById(id);
                List<SelectListItem> valueProduct = (from x in cm.GetList()
                                                     select new SelectListItem
                                                     {
                                                         Text = x.CategoryName,
                                                         Value = x.CategoryID.ToString()
                                                     }).ToList();
                ViewBag.vlce = valueProduct;
                return View(productValue);
            }
            return Redirect("/");
            //TODO uyarlanması gerek
        
            
        }
        [HttpPost]
        public ActionResult EditProduct(Product p)
        {
            //pm.ProductUpdate(p);
            //return RedirectToAction("Index");

            ValidationResult results = productvalidator.Validate(p);
            if (results.IsValid)
            {
                pm.ProductUpdate(p);
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
        public ActionResult DeleteProduct(int id)
        {
            var productValue = pm.GetById(id);
            pm.ProductDelete(productValue);
            return RedirectToAction("Index"); // Go to INDEX
        }
    }
}