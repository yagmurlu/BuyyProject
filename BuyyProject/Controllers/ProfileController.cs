using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuyyProject.Controllers
{
    public class ProfileController : Controller
    {

        // GET: Profile
        ProfileManager pm = new ProfileManager(new EfProfileDal());
        public ActionResult Index()
        {
            var profileValues = pm.GetList();
            return View(profileValues);
        }
    }
}