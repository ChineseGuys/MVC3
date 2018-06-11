using MvcApplication1.Dal;
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
            var _userServices = new UserInfoDal();
            List<Auth> authList = null;
            var dbUser = _userServices.GetAll().Where(p => p.Account == Session["UserName"].ToString() && p.IsActive).FirstOrDefault();
            if (dbUser!=null)
            {
                authList = authServices.GetUrlByraRelationAndAuth(dbUser.fkRole);
                 
            }

            return View(authList);
        }

    }
}
