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
    public class RoleServices
    {
        /// <summary>
        /// 获取角色信息
        /// </summary>
        /// <returns>数据集合</returns>
        public List<Models.Role> GetAll()
        {
            string sql = "SELECT ID,Name,IsActive FROM [Role]";
            var datatable = SqlHelper.ExecuteDataTable(sql, CommandType.Text, null);
            var result = ConvertHelper.GetEntities<Role>(datatable).ToList();
            return result;
        }

        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="role">角色信息</param>
        /// <returns></returns>
        public int Add(Models.Role role)
        {
            string sql = @"INSERT into 
                                  [Role](Name,IsActive) 
                                  VALUES(@Name,@IsActive);";
            var parms = ConvertHelper.ToSqlParameterArray<Role>(role);
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, parms);
        }

        /// <summary>
        /// 更新信息
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public int Edit(Models.Role role)
        {
            string sql = @"update [Role] 
                               set 
                              Name=@Name
                                where ID=@ID";
            var parms = ConvertHelper.ToSqlParameterArray<Role>(role);
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, parms);
        }


        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int delete(int id)
        {
            string sql = @"delete [Role] where ID=@id";
            SqlParameter[] para = new SqlParameter[] {
                new SqlParameter("@id", id)
            };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
        }


        /// <summary>
        /// 总条数
        /// </summary>
        /// <returns></returns>
        public int getCount()
        {
            string sql = "select count(*) from [Role]";

            int count = Convert.ToInt32((SqlHelper.ExecuteScalar(sql, CommandType.Text)));
            return count;
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<Role> GetList(int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            string listSql = "select * from (select row_number() over(order by ID) as num,* from [Role]) as t where t.num>=@start and t.num<=@end";
            SqlParameter[] pars = { 
                                 new SqlParameter("@start",start),
                                 new SqlParameter("@end",end)
                                 };
            var datatable = SqlHelper.ExecuteDataTable(listSql, CommandType.Text, pars);
            var result = ConvertHelper.GetEntities<Role>(datatable).ToList();
            return result;



        }
    }
}