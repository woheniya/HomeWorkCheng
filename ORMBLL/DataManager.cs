using ORMDAL;
using ORMModel;
using System;
using System.Collections.Generic;

namespace ORMBLL
{
    public class DataManager
    {
        private static DataManager dataManager;
        public User user { get; set; }
        public Company company { get; set; }
        public List<User> userList { get; set; }
        public List<Company> companyList { get; set; }
        private DataManager() { }
        public static DataManager GetInstance()
        {
            return dataManager == null ? new DataManager() : dataManager;
        }
        public void Find(string tblName,int id)
        {
            if (tblName.Trim().ToLower() == "user")
            {
                user = SqlHelper.Find<User>(id);
            }
            else if (tblName.Trim().ToLower() == "company")
            {
                company = SqlHelper.Find<Company>(id);
            }
        }
        public void FindAll(string tblName)
        {
            if (tblName.Trim().ToLower() == "user")
            {
                userList = new List<User>();
                userList = SqlHelper.FindAll<User>();
            }
            else if (tblName.Trim().ToLower() == "company")
            {
                companyList = new List<Company>();
                companyList = SqlHelper.FindAll<Company>();
            }
        }
        public List<string> GetProperties(string tblName)
        {
            List<string> names = new List<string>();
            if (tblName.Trim().ToLower() == "user")
            {
                names = SqlHelper.GetPropties<User>();
            }
            else if (tblName.Trim().ToLower() == "company")
            {
                names = SqlHelper.GetPropties<Company>();
            }
            return names;
        }
        public void InsertRows<T>(List<T> ts)
        {
            SqlHelper.InsertRows(ts);
        }
    }
}
