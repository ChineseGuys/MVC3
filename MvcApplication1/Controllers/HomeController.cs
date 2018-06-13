using MvcApplication1.Models;
using MvcApplication1.Services;
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

        
        public ActionResult Index()
        {
            AuthServices authServices = new AuthServices();
            var _userServices = new UserServices();
            List<Auth> authList = null;
            // 获取当前登录用户的角色
            var dbUser = _userServices.GetAll().Where(p => p.Account == Session["UserName"].ToString() && p.IsActive).FirstOrDefault();
            if (dbUser!=null)
            {
                // 获取当前角色所拥有的权限
                authList = authServices.GetUrlByraRelationAndAuth(dbUser.fkRole);
                 
            }

            return View(authList);
        }

    }
}
