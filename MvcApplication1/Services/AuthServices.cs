using MvcApplication1.Helper;
using MvcApplication1.Models;
using MvcApplication1.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MvcApplication1.Services
{
    public class AuthServices
    {
        public AuthServices() {  }


        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        public List<Auth> GetAll() 
        {
            string sql = "SELECT * FROM [Auth]";
            var datatable = SqlHelper.ExecuteDataTable(sql, CommandType.Text, null);
            var result = ConvertHelper.GetEntities<Auth>(datatable).ToList();
            return result;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="pageSize"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public List<Auth> GetPagination(int currentPage, int pageSize, out int Count) 
        {
            string all = "SELECT * FROM [Auth]";
            var allDataTable = SqlHelper.ExecuteDataTable(all, CommandType.Text, null);
            Count = ConvertHelper.GetEntities<Auth>(allDataTable).Count;

            string sql = "Select * From ( Select Row_Number() Over(Order By ID asc) Rows, * From [Auth] ) tb Where Rows > @Begin And Rows <= @End";
            SqlParameter[] parameters = {
                new SqlParameter("@Begin", (currentPage-1) * pageSize),
                new SqlParameter("@End", currentPage * pageSize)
            };
            var datatable = SqlHelper.ExecuteDataTable(sql, CommandType.Text, parameters);
            var result = ConvertHelper.GetEntities<Auth>(datatable).ToList();
            return result;
        }


        /// <summary>
        /// 获取当前角色所有用的权限Url
        /// </summary>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public List<Auth> GetUrlByraRelationAndAuth(int roleID)
        {
            string sql = "select * from [Auth] inner join [Role_Auth_Relation] on Role_Auth_Relation.AuthID=[Auth].ID where Role_Auth_Relation.RoleID=@RoleID";
            SqlParameter[] parameters = {
                new SqlParameter("@RoleID", roleID),
            };
            var datatable = SqlHelper.ExecuteDataTable(sql, CommandType.Text, parameters);
            var result = ConvertHelper.GetEntities<Auth>(datatable).ToList();
            return result;
        }


        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public int Insert(Auth auth)
        {
            string sql = "INSERT [Auth](url) VALUES(@url)";
            var parms = ConvertHelper.ToSqlParameterArray<Auth>(auth);
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, parms);
        }

        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        public int Update(Auth auth) 
        {
            string sql = "UPDATE [Auth] SET Url=@url where ID=@id";
            var parms = ConvertHelper.ToSqlParameterArray<Auth>(auth);
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, parms);
        }
        
    }
}