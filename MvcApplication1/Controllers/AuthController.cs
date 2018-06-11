using MvcApplication1.Models;
using MvcApplication1.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class AuthController : BaseController
    {
        private  AuthServices _authServices;

        //
        // GET: /Auth/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetAll() 
        {
            _authServices = new AuthServices();
            var list = _authServices.GetAll();
            //return View(list);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Create(Auth auth) 
        {
            _authServices = new AuthServices();

            var flag =  _authServices.Insert(auth);

            if (flag > 0)
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Update(Auth auth) 
        {
            _authServices = new AuthServices();
            var flag = _authServices.Update(auth);

            if (flag > 0)
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

        }

    }
}
