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
    public class UserServices
    {
        /// <summary>
        /// 获取用户信息(根据ID获取)
        /// </summary>
        /// <returns></returns>
        public List<User> GetUserInfo(int id)
        {
            List<User> list = new List<User>();
            // sql语句
            string sql = @"select ID,fkRole,Name,Account,PhoneNumber,Password,CreateTime,IsActive from [User] where ID=@id ";
            // 参数
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@id",id)
            };
            // 调用SqlHelper中的ExecuteReader方法获取数据
            using (SqlDataReader reader = SqlHelper.ExecuteReader(sql, CommandType.Text, para))
            {
                //如果查询到了数据继续往下执行
                if (reader.HasRows)
                {
                    //读取数据
                    if (reader.Read())
                    {
                        //给model层的对象赋值
                        User userInfo = new User();
                        userInfo.ID = reader.GetInt32(0);
                        userInfo.fkRole = reader.GetInt32(1);//reader["fkRole"];//reader.GetInt32(1);
                        userInfo.Name = reader.GetString(2);
                        userInfo.Account = reader.GetString(3);
                        userInfo.PhoneNumber = reader.GetInt64(4);
                        userInfo.Password = reader.GetString(5);
                        userInfo.CreateTime = reader.GetInt64(6);
                        userInfo.IsActive = reader.GetBoolean(7);
                        list.Add(userInfo);

                    }
                }
            }

            return list;
        }


        /* 添加备用
        public int Add(User model)
        {
           string sql=@"INSERT [User](fkRole,Name,Account,PhoneNumber,Password,CreateTime,IsActive) 
            VALUES(@fkRole,@Name,@Account,@PhoneNumber,@Password,@CreateTime,@IsActive);";
        }
         */


        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns>返回收影响的行数</returns>
        public int delete(int id)
        {
            string sql = @"delete [User] where ID=@id";
            SqlParameter[] para = new SqlParameter[] {
                new SqlParameter("@id", id)
            };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
        }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns>list集合</returns>
        public List<User> GetAll()
        {
            string sql = "SELECT ID,fkRole,Name,Account,PhoneNumber,Password,CreateTime,IsActive FROM [User]";
            var datatable = SqlHelper.ExecuteDataTable(sql, CommandType.Text, null);
            var result = ConvertHelper.GetEntities<User>(datatable).ToList();
            return result;
        }

        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="user">返回收影响的行数</param>
        /// <returns></returns>
        public int Add(User user)
        {
            string sql = @"INSERT into 
                                  [User](fkRole,Name,Account,PhoneNumber,Password,CreateTime,IsActive) 
                                  VALUES(@fkRole,@Name,@Account,@PhoneNumber,@Password,@CreateTime,@IsActive);";
            var parms = ConvertHelper.ToSqlParameterArray<User>(user);
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, parms);
        }



        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Edit(User user)
        {
            string sql = @"update [User] 
                               set 
                              Name=@Name,Account=@Account,PhoneNumber=@PhoneNumber
                                where ID=@ID";
            var parms = ConvertHelper.ToSqlParameterArray<User>(user);
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, parms);
        }


        /// <summary>
        /// 编辑用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public int Edit(int id, string name, string account, long phoneNumber)
        {
            string sql = @"update [User] 
                               set 
                              Name=@Name,Account=@Account,PhoneNumber=@PhoneNumber
                                where ID=@ID";

            SqlParameter[] para = new SqlParameter[]{
            new SqlParameter("@ID",id),
            new SqlParameter("Name",name),
            new SqlParameter("@Account",account),
            new SqlParameter("@PhoneNumber",phoneNumber)
            };
            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);
        }


        /// <summary>
        /// 给用户分配角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int Edit(int id, int roleId)
        {
            string sql = @"update [User] 
                               set 
                              fkRole=@fkRole
                                where ID=@ID";
            SqlParameter[] para = new SqlParameter[]
            {
                new SqlParameter("@fkRole",roleId),
                new SqlParameter("@ID",id)
            };

            return SqlHelper.ExecuteNonQuery(sql, CommandType.Text, para);

        }


        /// <summary>
        /// 总条数
        /// </summary>
        /// <returns></returns>
        public int getCount()
        {
            string sql = "select count(*) from [User]";

            int count = Convert.ToInt32((SqlHelper.ExecuteScalar(sql, CommandType.Text)));
            return count;
        }


        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public List<User> GetList(int pageIndex, int pageSize)
        {
            int start = (pageIndex - 1) * pageSize + 1;
            int end = pageIndex * pageSize;
            string listSql = "select * from (select row_number() over(order by ID) as num,* from [User]) as t where t.num>=@start and t.num<=@end";
            SqlParameter[] pars = { 
                                 new SqlParameter("@start",start),
                                 new SqlParameter("@end",end)
                                 };
            var datatable = SqlHelper.ExecuteDataTable(listSql, CommandType.Text, pars);
            var result = ConvertHelper.GetEntities<User>(datatable).ToList();
            return result;



        }
    }
}