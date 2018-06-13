using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using MvcApplication1.Tools;
using MvcApplication1.Helper;
using MvcApplication1.Services;

namespace MvcApplication1.Controllers
{

    public class UserController : BaseController
    {
        //UserInfoDal userInfo = new UserInfoDal();
        //RoleInfoDal roleInfo = new RoleInfoDal();
        RoleServices roleInfo = new RoleServices();
        UserServices userInfo = new UserServices();
        //
        // GET: /User/

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
            List<User> list = userInfo.GetAll();
            //foreach (var time in list)
            //{
            //    // 将时间戳转换为时间
            //    time.CreateTime = TimeHelper.GetTime(time.CreateTime.ToString());
            //}
            //ViewData["msg"] = list;
            return Json(list, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// 分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult PageList()
        {
            // 当前页
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            // 一页显示多少条
            int pageSize = Request["rows"]!=null?int.Parse(Request["rows"]):5;//int.Parse(Request["rows"]);
            int sizeCount = userInfo.getCount();
            //int totalCount= userInfo.getTotalIndex(pageSize);
            List<User> list = userInfo.GetList(pageIndex, pageSize);
            return Json(new { rows = list, total = sizeCount }, JsonRequestBehavior.AllowGet);
        }
        
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <returns>返回yes或者No</returns>
        [HttpPost]
        public ActionResult Add(User user)
        {
            
            user.IsActive = true;
            // MD5加密
            user.Password = SecurityHelper.str2md5(user.Password);
            // 将时间转换为时间戳
            user.CreateTime = TimeHelper.ConvertDateTimeInt(DateTime.Now);
            // 受影响的行数（为1，则表示成功）
            int issuccess= userInfo.Add(user);

            //if (issuccess > 0)
            //{
            //    return Content("ok");
            //}
            //else 
            //{
            //    return Content("no");
            //}
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
        /// 删除用户信息
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns>返回yes或者No</returns>
        [HttpPost]
        public ActionResult delete() 
        {
            int id= Convert.ToInt32(Request["strId"]);
            // 受影响的行数（为1，则表示成功）
            int issuccess = userInfo.delete(id);
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
        public ActionResult Edit(User user)
        {
            // 受影响的行数（为1，则表示成功）
            int issuccess = userInfo.Edit(user.ID,user.Name,user.Account,user.PhoneNumber);
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
        /// 为用户分配角色
        /// </summary>
        /// <returns></returns>
        
        public ActionResult SetUserRoleInfo()
        {
            // 接受用户ID
            int id = int.Parse(Request["id"]);
            // 查询用户信息
            var userMsg= userInfo.GetUserInfo(id);
            //赋值以便在前台显示
            ViewBag.UserInfo = userMsg;

            // 查询所有角色的信息
            var roleMsg = roleInfo.GetAll();
            ViewBag.RoleInfo = roleMsg;

            

            return View();
        }

        /// <summary>
        /// 为用户分配角色
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetUserRoleInfo(FormCollection collection) 
        {
            // 接受用户编号
            int userId = int.Parse(Request["userId"]);
            int roleId = Request["ID"] == null ? 0 : int.Parse(Request["ID"]);
            // 给用户分配角色
            int issuccess= userInfo.Edit(userId,roleId);

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
