using MvcApplication1.Helper;
using MvcApplication1.Models;
using MvcApplication1.Tools;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace MvcApplication1.Services
{
    public class Role_Auth_RelationServices
    {
        public Role_Auth_RelationServices() { }

        public List<int> GetRoleAuthByRoleID(int roleID) 
        {
            List<int> intList = new List<int>();
            string sql = "SELECT * FROM [Role_Auth_Relation] where RoleID="+roleID;
            var datatable = SqlHelper.ExecuteDataTable(sql, CommandType.Text, null);
            var result = ConvertHelper.GetEntities<Role_Auth_Relation>(datatable);
            foreach (var item in result)
            {
                intList.Add(item.AuthID);
            }

            return intList;
        }

        public int InsertRoleAuth(int roleID, List<int> authIDList) 
        {
            string del = "DELETE FROM [dbo].[Role_Auth_Relation] WHERE RoleID = " + roleID;
            SqlHelper.ExecuteNonQuery(del, CommandType.Text, null);

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