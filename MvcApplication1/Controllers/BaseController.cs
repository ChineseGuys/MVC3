using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// 重写OnActionExecuting（）方法，在Action之前执行次方法，实现登录状态的校验
        /// </summary>
        /// <param name="filterContext"></param>
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

    }
}
