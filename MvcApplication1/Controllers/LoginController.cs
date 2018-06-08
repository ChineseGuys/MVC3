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
            string pwd = SecurityHelper.str2md5(user.Password);
            
            var rv = new { success = true };
            return Json(rv, JsonRequestBehavior.AllowGet);
            

        }

    }
}
