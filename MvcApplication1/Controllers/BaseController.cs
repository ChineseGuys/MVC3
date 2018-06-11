using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class BaseController : Controller
    {
<<<<<<< HEAD
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            var userName = Session["UserName"] as String;
            if (String.IsNullOrEmpty(userName))
            {
                //重定向至登录页面
                filterContext.Result = RedirectToAction("Index", "Login");
                return;
            }

        }
=======
        //
        // GET: /Base/

        //public ActionResult Index()
        //{
        //    if (Session["UserInfo"] == null) 
        //    {
        //        Response.Redirect("/Login/index");
        //    }
            
        //    return View();
        //}
>>>>>>> 84fa867c39e71e587f766acd52a9b029090b4ea4

    }
}
