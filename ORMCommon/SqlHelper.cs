using ORMModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace ORMCommon
{
    public enum DataType
    {
        String,
        DateTime,
        Int32
    }
    public static class SqlHelper
    {
        static string connectStr = "Data Source=LocalHost;Integrated Security=SSPI;Database = CustomerDB;";

        public static int ExecuteNonQuery(string sql)
        {
            using (SqlConnection conn = new SqlConnection(connectStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        #region 用DataReader去访问数据库，将得到的结果通过反射生成实体对象；
        /// <summary>
        /// 用DataReader去访问数据库，将得到的结果通过反射生成实体对象；
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T Find<T>(int id) where T : BaseModel
        {
            Type type = typeof(T);

            string sql = $"Select {string.Join(",", type.GetProperties().Select(p => p.Name))} from [{type.Name}] where id = {id}";
            object objType = Activator.CreateInstance(type);

            using (SqlConnection conn = new SqlConnection(connectStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        foreach (PropertyInfo prop in type.GetProperties())
                        {
                            prop.SetValue(objType, reader[prop.Name]);
                        }
                    }
                }
            }
            return (T)objType;
        }

        public static List<T> FindAll<T>() where T : BaseModel
        {
            Type type = typeof(T);

            string sql = $"Select {string.Join(",", type.GetProperties().Select(p => p.Name))} from [{type.Name}]";
            object objType = Activator.CreateInstance(type);

            using (SqlConnection conn = new SqlConnection(connectStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        foreach (PropertyInfo prop in type.GetProperties())
                        {
                            prop.SetValue(objType, reader[prop.Name]);
                        }
                    }
                }
            }
            return default;
        }
        #endregion

    }
}
