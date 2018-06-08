using MvcApplication1.Models;
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
        public ActionResult Index(User user) 
        {

            string pwd = SecurityHelper.str2md5(user.Password);

            if (User!=null)
            {
                Response.Redirect("/home/index");
                
            }
            return View();

        }

    }
}
