using MvcApplication1.Helper;
using MvcApplication1.Models;
using MvcApplication1.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MvcApplication1.Services
{
    public class Role_Auth_RelationServices
    {
        public Role_Auth_RelationServices() { }

        /// <summary>
        /// 获取当前角色所用的权限
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<int> GetRoleAuthByRoleID(int roleID) 
        {
            List<int> intList = new List<int>();
            string sql = "SELECT * FROM [Role_Auth_Relation] where RoleID=@RoleID";
            SqlParameter[] parameters = {
                new SqlParameter("@RoleID", roleID),
            };
            var datatable = SqlHelper.ExecuteDataTable(sql, CommandType.Text, parameters);
            var result = ConvertHelper.GetEntities<Role_Auth_Relation>(datatable);
            foreach (var item in result)
            {
                intList.Add(item.AuthID);
            }

            return intList;
        }


        /// <summary>
        /// 给角色分配权限
        /// </summary>
        /// <param name="roleID">角色ID</param>
        /// <param name="authIDList">权限ID</param>
        /// <returns></returns>
        public int InsertRoleAuth(int roleID, List<int> authIDList) 
        {
            string del = "DELETE FROM [dbo].[Role_Auth_Relation] WHERE RoleID =@RoleID ";
            SqlParameter[] parameters = {
                new SqlParameter("@RoleID", roleID),
            };
            SqlHelper.ExecuteNonQuery(del, CommandType.Text, parameters);

            string sql = string.Empty;
            string allSql = string.Empty;
            foreach (var item in authIDList)
            {
                sql = string.Format("INSERT INTO [dbo].[Role_Auth_Relation]  ([RoleID],[AuthID]) VALUES ({0},{1}) ", roleID, item);
                allSql += sql;
            }
            return SqlHelper.ExecuteNonQuery(allSql, CommandType.Text, null);
        }
    }
}