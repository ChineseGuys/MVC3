
using MvcApplication1.Models;
using MvcApplication1.Services;
using MvcApplication1.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Login/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user) 
        {
            var _userServices = new UserServices();
            string pwd = SecurityHelper.str2md5(user.Password);
            
            var dbUser = _userServices.GetAll().Where(p => p.Account == user.Account && p.Password == pwd && p.IsActive).FirstOrDefault();
            if (dbUser!=null)
            {
                var rv = new { success = true };
                Session["UserName"] = user.Account;
                return Json(rv, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var rv = new { success = false };
                Session["UserName"] = null;
                return Json(rv, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
