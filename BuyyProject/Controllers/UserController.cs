using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLayer.Concrete;
using BusinessLayer.Concrete;
using BusinessLayer.Abstract;

namespace BuyyProject.Controllers
{
    [Route(Name = "/User")]
    public class UserController : Controller
    {
        IUserService _us;

        public UserController(IUserService userService) {
            this._us = userService;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        [Route(Name ="Register")]
        [HttpGet]
        public ActionResult Register() {
            if (Session["User"] != null) return Redirect("/");
            return View("~/Views/User/Register.cshtml");
        }

        [Route(Name = "Register")]
        [HttpPost]
        public ActionResult Register(UserDTO u)
        {
            if (Session["User"] != null) return Redirect("/");
            _us.RegisterUser(u ); 
            return Redirect("/User/Login");
        }

        [Route(Name ="Login")]
        [HttpGet]
        public ActionResult Login() {
            if (Session["User"] != null) return Redirect("/");
            return View("~/Views/User/Login.cshtml");
        }

        [Route(Name = "Login")]
        [HttpPost]
        public ActionResult Login(UserDTO u)
        {
            if (Session["User"] != null) return Redirect("/");
            User a = _us.LoginUser(u);
            if (a != null) {
                Session["User"] = a;
                return Redirect("/");
            }
            return Redirect("/User/Login");
        }


        [Route(Name = "Logout")]
        [HttpGet]
        public ActionResult Logout() {
            if (Session["User"] == null) return Redirect("/User/Login");
            Session["User"] = null;
            return Redirect("/");
        }

        //public ActionResult Register([Bind("Username,Password,PasswordVerify,Email")])
    }
}