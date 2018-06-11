using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class HomeController : BaseController
    {
        //
        // GET: /Home/

        public ActionResult Index(User user)
        {
<<<<<<< HEAD
            user.Name = "33";
            return View(user);
=======
            
            return View();
>>>>>>> 84fa867c39e71e587f766acd52a9b029090b4ea4
        }

    }
}
