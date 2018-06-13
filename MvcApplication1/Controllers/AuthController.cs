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

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetAll() 
        {
            int pageIndex, pageSize;
            pageIndex = int.Parse(Request["page"]);     //第几页的数据  
            pageSize = int.Parse(Request["rows"]);  //每页多少条数据  
            int total = 0;         //返回数据条数总值  

            _authServices = new AuthServices();
            var list = _authServices.GetPagination(pageIndex, pageSize,out total);

            var data = new
            {
                total,      //将数据total加载到data中，返回到前台。  
                rows = list
            };  

            return Json(data, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public ActionResult Create(Auth auth) 
        {
            _authServices = new AuthServices();

            var flag =  _authServices.Insert(auth);

            if (flag > 0)
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
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
