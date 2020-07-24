using ORMModel;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace ORMDAL
{
    public enum DataType
    {
        String,
        DateTime,
        Int32
    }
    public class SqlHelper
    {
        static string connectStr = "Data Source=LocalHost;Integrated Security=SSPI;Database = CustomerDB;";
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
            //string sql = $"select {string.Join(',', type.GetProperties().Select(p => p.Name))} from  [{type.Name}] where id={id}";
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
        #endregion

        #region 用DataReader去访问数据库，将得到的结果通过反射生成实体对象集合返回；
        /// <summary>
        /// 用DataReader去访问数据库，将得到的结果通过反射生成实体对象集合返回；
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<T> FindAll<T>() where T : BaseModel
        {
            Type type = typeof(T);
            List<T> list = new List<T>();
            object objType = null;
            string sql = $"Select {string.Join(",", type.GetProperties().Select(p => p.Name))} from [{type.Name}]";
            //string sql = $"select {string.Join(',', type.GetProperties().Select(p => p.Name))} from  [{type.Name}] where id={id}";
            using (SqlConnection conn = new SqlConnection(connectStr))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        objType = Activator.CreateInstance(type);
                        foreach (PropertyInfo prop in type.GetProperties())
                        {
                            prop.SetValue(objType, reader[prop.Name]);
                        }
                        list.Add((T)objType);
                    }
                }
            }
            return list;
        }
        #endregion

        #region 获取类的属性
        /// <summary>
        /// 获取类的属性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static List<string> GetPropties<T>()
        {
            List<string> names = new List<string>();
            Type type = typeof(T);
            foreach (PropertyInfo prop in type.GetProperties())
            {
                names.Add(prop.Name);
            }
            return names;
        }
        #endregion

        #region 泛型的数据库实体插入
        /// <summary>
        /// 插入
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool InsertRows<T>(List<T> t)
        {
            Type type = typeof(T);
            string sql = $"Insert into {type.Name} (";

            //string sql = $"Insert into ({string.Join(",", type.GetProperties().Select(p => p.Name))}) values()]";

            IEnumerable<string> tNameList = type.GetProperties().Select(p => p.Name);

            List<string> list = tNameList.ToList();
            list.RemoveAt(list.Count - 1);

            foreach (string name in list)
            {
                if (name == list.Last())
                {
                    sql += name + ") Values";
                }
                else
                {
                    sql += name + ",";
                }
            }

            for (int i = 0; i < t.Count; i++)
            {
                sql += "(";
                for (int j = 0; j < type.GetProperties().Length - 1; j++)
                {
                    if (i == t.Count - 1)
                    {
                        if (type.GetProperties()[j].PropertyType.Name == DataType.String.ToString())
                        {
                            sql += "'" + type.GetProperties()[j].GetValue(t[i], null) + "'" + ")";
                        }
                        //如何得到实体的值？？？info.GetValue(t, null)
                        else if (type.GetProperties()[j].PropertyType.Name == DataType.DateTime.ToString())
                        {
                            sql += "'" + type.GetProperties()[j].GetValue(t[i], null) + "'" + ")";
                        }
                        else if (type.GetProperties()[j].PropertyType.Name == DataType.Int32.ToString())
                        {
                            sql += type.GetProperties()[j].GetValue(t[i], null) + ")";
                        }
                    }
                    else
                    {
                        if (type.GetProperties()[j].PropertyType.Name == DataType.String.ToString())
                        {
                            sql += "'" + type.GetProperties()[j].GetValue(t[i], null) + "'" + "),";
                        }
                        //如何得到实体的值？？？info.GetValue(t, null)
                        else if (type.GetProperties()[j].PropertyType.Name == DataType.DateTime.ToString())
                        {
                            sql += "'" + type.GetProperties()[j].GetValue(t[i], null) + "'" + "),";
                        }
                        else if (type.GetProperties()[j].PropertyType.Name == DataType.Int32.ToString())
                        {
                            sql += type.GetProperties()[j].GetValue(t[i], null) + "),";
                        }
                    }

                }
            }

            return default;
            //return ExcuteSql(sql) > 0 ? true : false;
        }
        #endregion

        #region 泛型的数据库实体插入
        /// <summary>
        /// 插入
        //UPDATE categories
        //     SET display_order = CASE id
        //         WHEN 1 THEN 3
        //         WHEN 2 THEN 4
        //         WHEN 3 THEN 5
        //     END,
        //     title = CASE id
        //         WHEN 1 THEN 'New Title 1'
        //         WHEN 2 THEN 'New Title 2'
        //         WHEN 3 THEN 'New Title 3'
        //     END
        //WHERE id IN(1,2,3)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool UpdateRows<T>(List<T> t)
        {
            Type type = typeof(T);
            string sql = $"Update {type.Name} set(";

            //string sql = $"Insert into ({string.Join(",", type.GetProperties().Select(p => p.Name))}) values()]";

            IEnumerable<string> tNameList = type.GetProperties().Select(p => p.Name);

            List<string> list = tNameList.ToList();
            list.RemoveAt(list.Count - 1);

            foreach (string name in list)
            {
                if (name == list.Last())
                {
                    sql += name + ") Values";
                }
                else
                {
                    sql += name + ",";
                }
            }

            for (int i = 0; i < t.Count; i++)
            {
                sql += "(";
                for (int j = 0; j < type.GetProperties().Length - 1; j++)
                {
                    if (i == t.Count - 1)
                    {
                        if (type.GetProperties()[j].PropertyType.Name == DataType.String.ToString())
                        {
                            sql += "'" + type.GetProperties()[j].GetValue(t[i], null) + "'" + ")";
                        }
                        //如何得到实体的值？？？info.GetValue(t, null)
                        else if (type.GetProperties()[j].PropertyType.Name == DataType.DateTime.ToString())
                        {
                            sql += "'" + type.GetProperties()[j].GetValue(t[i], null) + "'" + ")";
                        }
                        else if (type.GetProperties()[j].PropertyType.Name == DataType.Int32.ToString())
                        {
                            sql += type.GetProperties()[j].GetValue(t[i], null) + ")";
                        }
                    }
                    else
                    {
                        if (type.GetProperties()[j].PropertyType.Name == DataType.String.ToString())
                        {
                            sql += "'" + type.GetProperties()[j].GetValue(t[i], null) + "'" + "),";
                        }
                        //如何得到实体的值？？？info.GetValue(t, null)
                        else if (type.GetProperties()[j].PropertyType.Name == DataType.DateTime.ToString())
                        {
                            sql += "'" + type.GetProperties()[j].GetValue(t[i], null) + "'" + "),";
                        }
                        else if (type.GetProperties()[j].PropertyType.Name == DataType.Int32.ToString())
                        {
                            sql += type.GetProperties()[j].GetValue(t[i], null) + "),";
                        }
                    }

                }
            }

            return default;
            //return ExcuteSql(sql) > 0 ? true : false;
        }
        #endregion




    }
}
