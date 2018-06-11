using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MvcApplication1.Helper
{
    /// <summary>
    /// 数据库帮助类
    /// </summary>
    public  class SqlHelper
    {
        // 获取连接字符串
        public static readonly string conString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
        

        /// <summary>
        /// 该方法执行传入增删改SQL语句，返回受影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="cmdType"></param>
        /// <param name="pms"></param>
        /// <returns></returns>
        public static int ExecuteNonQuery(string sql, CommandType cmdType, params SqlParameter[] pms)
        {
            //using在连接数据库操作完毕之后自动释放资源
            //创建SqlConnection对象连接数据库
            using (SqlConnection con = new SqlConnection(conString))
            {
                //创建SqlCommand对象执行sql语句
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    //确定SqlCommand对象执行的类型是SQL语句还是存储过程
                    cmd.CommandType = cmdType;
                    //如果参数不为空就添加到SQL语句中
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    //打开数据库连接
                    con.Open();
                    //返回执行结果
                    return cmd.ExecuteNonQuery();
                }
            }
        }



        /// <summary>
        /// 执行查询，放回单个值的方法
        /// </summary>
        /// <param name="sql">SQL语句</param>
        /// <param name="cmdType">执行类型</param>
        /// <param name="pms">参数</param>
        /// <returns></returns>
        public static object ExecuteScalar(string sql, CommandType cmdType, params SqlParameter[] pms)
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, con))
                {
                    cmd.CommandType = cmdType;
                    if (pms != null)
                    {
                        cmd.Parameters.AddRange(pms);
                    }
                    con.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }


        /// <summary>
        /// 执行查询，返回多行多列的方法
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdType">执行类型</param>
        /// <param name="pms">参数</param>
        /// <returns></returns>
        public static SqlDataReader ExecuteReader(string sql, CommandType cmdType, params SqlParameter[] pms)
        {
            //SqlConnection在这不能使用using，因为ExecuteReader方法在连接数据库，传输数据时要保证数据库的连接是打开的，而using相当于try...finally，在传输数据时，就数据库连接就已经关了，这样是不行的。
            SqlConnection con = new SqlConnection(conString);
            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                cmd.CommandType = cmdType;
                if (pms != null)
                {
                    cmd.Parameters.AddRange(pms);
                }
                //把连接数据库的代码监视起来，如果出现异常则执行catch方法，关闭数据库连接并释放资源。
                try
                {
                    con.Open();
                    //System.Data.CommandBehavior.CloseConnection这个枚举参数，表示将来使用完毕SqlDataReader后，在关闭reader的同时，在SqlDataReader内部会将关联的Connection对象也关闭掉
                    return cmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
                catch (Exception)
                {
                    con.Close();
                    con.Dispose();
                    throw;
                }
            }
        }

        /// <summary>
        /// 查询数据返回DataTable
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="cmdType">执行类型</param>
        /// <param name="pms">参数</param>
        /// <returns></returns>
        public static DataTable ExecuteDataTable(string sql, CommandType cmdType, params SqlParameter[] pms)
        {
            //创建一个表格对象，用来存储来自数据库数据
            DataTable dt = new DataTable();
            //创建SqlDataAdapter对象，传入SQL语句和连接字符串，它会自动连接并获取数据
            using (SqlDataAdapter adapter = new SqlDataAdapter(sql, conString))
            {
                adapter.SelectCommand.CommandType = cmdType;
                if (pms != null)
                {
                    //调用SelectCommand方法把参数添加到adapter对象
                    adapter.SelectCommand.Parameters.AddRange(pms);
                }
                //把从数据库获取的数据填充到表格中
                adapter.Fill(dt);
            }
            //返回表格
            return dt;
        }
       
    }
}