using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityLayer.Concrete;
using MySql.Data.MySqlClient;
using System.Dynamic;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;

namespace BuyyProject.Controllers
{
    public class TicketController : Controller
    {
        TicketManager tm = new TicketManager(new EfTicketDal());
        TicketStatusManger tms = new TicketStatusManger(new EfTicketStatusDal());
        TicketTypeManger ttm = new TicketTypeManger(new EfTicketTypeDal());
        MessageManager mm = new MessageManager(new EfMessageDal());
        public ActionResult Index()
        {
            var a = tm.GetList();
            ViewBag.Status = tms.GetList();
            ViewBag.Type = ttm.GetList();
            var searchType = Request.QueryString["searchType"];
            return View(a);
        }
        public ActionResult ShowTicket(int id)
        {
            ViewBag.Message = mm.GetList();
            var a = tm.GetById(id);
            return View(a);
        }
        [HttpPost]
        public ActionResult SendMessage(Messages M)
        {
            //TODO user from user manager
            M.UserID = 2;
            M.MessagesCreated_at = DateTime.UtcNow;
            mm.MessageAddBL(M);
            return Redirect(Request.UrlReferrer.ToString()) ;
        }
    }
}