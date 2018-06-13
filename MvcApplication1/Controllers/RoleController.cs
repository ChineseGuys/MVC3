using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using MvcApplication1.Services;
using MvcApplication1.Services;

namespace MvcApplication1.Controllers
{
    public class RoleController : BaseController
    {
        Role role = new Role();
        AuthServices authServices = new AuthServices();
        Role_Auth_RelationServices raRalationServices = new Role_Auth_RelationServices();
        RoleServices roleInfoDal = new RoleServices();
        
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
        /// 分页列表
        /// </summary>
        /// <returns></returns>
        public ActionResult PageList()
        {
            // 当前页
            int pageIndex = Request["page"] != null ? int.Parse(Request["page"]) : 1;
            // 一页显示多少条
            int pageSize = int.Parse(Request["rows"]);
            int sizeCount = roleInfoDal.getCount();
            //int totalCount= userInfo.getTotalIndex(pageSize);
            List<Role> list = roleInfoDal.GetList(pageIndex, pageSize);
            return Json(new { rows = list, total = sizeCount }, JsonRequestBehavior.AllowGet);
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

        public ActionResult SetRoleAuthInfo()
        {
            // 接受用户ID
            int id = int.Parse(Request["id"]);

            // 查询所有角色的信息
            var roleMsg = roleInfoDal.GetAll().Where(p=>p.ID==id).FirstOrDefault();
            ViewBag.RoleInfo = roleMsg;

            var authMsg = authServices.GetAll();
            ViewBag.AuthInfo = authMsg;

            var authIDList = raRalationServices.GetRoleAuthByRoleID(roleMsg.ID);
            ViewBag.AuthIDList = authIDList;

            return View();
        }

        /// <summary>
        /// 为用户分配角色
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult SetRoleAuthInfo(FormCollection collection)
        {
            int roleId = int.Parse(Request["roleId"]);
          

            // 已有权限
            //var dbRoleAuthList = raRalationServices.GetRoleAuthByRoleID(roleId);

            string[] keys = Request.Form.AllKeys;
            if (keys.Count()>2)
            {
                //选择的权限
                List<int> authIDList = new List<int>();
                for (int i = 1; i < keys.Count() - 1; i++)
                {
                    authIDList.Add(int.Parse(collection[keys[i]]));
                }

                // 去重
                //var resultList = authIDList.Where((x, i) => authIDList.FindIndex(z => z == x) == i).ToList();
                var result = raRalationServices.InsertRoleAuth(roleId, authIDList);
            }

            if (keys.Count() == 1)
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
