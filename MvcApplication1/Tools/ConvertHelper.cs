using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Web;

namespace MvcApplication1.Tools
{
    public static class ConvertHelper
    {

        public static T GetEntity<T>(DataTable table) where T : new()
        {
            T entity = new T();
            foreach (DataRow row in table.Rows)
            {
                foreach (var item in entity.GetType().GetProperties())
                {
                    if (row.Table.Columns.Contains(item.Name))
                    {
                        if (DBNull.Value != row[item.Name])
                        {
                            item.SetValue(entity, Convert.ChangeType(row[item.Name], item.PropertyType), null);
                        }

                    }
                }
            }

            return entity;
        }

        public static IList<T> GetEntities<T>(DataTable table) where T : new()
        {
            IList<T> entities = new List<T>();
            foreach (DataRow row in table.Rows)
            {
                T entity = new T();
                foreach (var item in entity.GetType().GetProperties())
                {
                    item.SetValue(entity, Convert.ChangeType(row[item.Name], item.PropertyType), null);
                }
                entities.Add(entity);
            }
            return entities;
        }

        public static SqlParameter[] ToSqlParameterArray<T>(this T entityClass) where T : class
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            PropertyInfo[] propertys = entityClass.GetType().GetProperties();
            foreach (PropertyInfo pi in propertys)
            {
                // 判断此属性是否有Getter
                if (!pi.CanRead)
                {
                    continue;
                }
                object value = pi.GetValue(entityClass, null);
                parms.Add(new SqlParameter("@" + pi.Name.ToLower(), value));
            }
            return parms.ToArray();
        }
    }
}