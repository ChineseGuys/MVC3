using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using MvcApplication1.Dal;

namespace MvcApplication1.Controllers
{
    public class RoleController : Controller
    {
        Role role = new Role();
        RoleInfoDal roleInfoDal = new RoleInfoDal();
        //
        // GET: /Role/

        public ActionResult Index()
        {
            return View();
        }


        /// <summary>
        /// 获取信息
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMsg()
        {
            List<Role> list = roleInfoDal.GetAll();
            //ViewData["msg"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }



        /// <summary>
        /// 添加信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public ActionResult Add(Role role) 
        {
            role.IsActive = true;
            int issuccess = roleInfoDal.Add(role);
            if (issuccess == 1)
            {
                var ret = new
                {
                    message = true
                };
                // 返回json
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var ret = new
                {
                    message = false
                };
                return Json(ret, JsonRequestBehavior.AllowGet);
            }   
 
        }


        /// <summary>
        /// 删除信息
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete() 
        {
            int id = Convert.ToInt32(Request["strId"]);
            // 受影响的行数（为1，则表示成功）
            int issuccess = roleInfoDal.delete(id);
            if (issuccess == 1)
            {
                var ret = new
                {
                    message = "yes"
                };
                // 返回json
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var ret = new
                {
                    message = "no"
                };
                return Json(ret, JsonRequestBehavior.AllowGet);
            }   
        }


        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(Role role)
        {
            // 受影响的行数（为1，则表示成功）
            int issuccess = roleInfoDal.Edit(role);
            if (issuccess == 1)
            {
                var ret = new
                {
                    message = "yes"
                };
                // 返回json
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var ret = new
                {
                    message = "no"
                };
                return Json(ret, JsonRequestBehavior.AllowGet);
            }
        }

    }
}
