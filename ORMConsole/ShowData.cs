using ORMBLL;
using System;
using System.Collections.Generic;
using System.Text;

namespace ORMConsole
{
    public class ShowData
    {
        DataManager dataManager;
        public ShowData()
        {
            dataManager = DataManager.GetInstance();
        }
        public string GetTableName()
        {
            Console.WriteLine("请输入tableName:");
            return Console.ReadLine();
        }
        public void Find()
        {
            Console.WriteLine("请输入id:");
            dataManager.Find(GetTableName(), Convert.ToInt32(Console.ReadLine()));
        }
        public void FindAll()
        {
            dataManager.FindAll(GetTableName().ToString());
        }
        public void FindUser()
        {
            GetTableName();
            Find();
            if (dataManager.user != null)
            {
                Console.WriteLine($"员工ID是:{ dataManager.user.Id}");
                Console.WriteLine($"员工名称是:{ dataManager.user.Name}");
                Console.WriteLine($"员工账号是:{ dataManager.user.Account}");
                Console.WriteLine($"员工密码是:{ dataManager.user.Password}");
                Console.WriteLine($"员工邮箱是:{ dataManager.user.Email}");
                Console.WriteLine($"员工手机号码是:{ dataManager.user.Mobile}");
                Console.WriteLine($"员工公司ID是:{ dataManager.user.CompanyId}");
                Console.WriteLine($"员工公司名称是:{ dataManager.user.CompanyName}");
                Console.WriteLine($"员工状态是:{ dataManager.user.State}");
                Console.WriteLine($"员工类型是:{ dataManager.user.UserType}");
                Console.WriteLine($"员工上次登录时间是:{ dataManager.user.LastLoginTime}");
                Console.WriteLine($"员工创建时间是:{ dataManager.user.CreateTime}");
                Console.WriteLine($"员工创建ID是:{ dataManager.user.CreatorId}");
                Console.WriteLine($"员工上次修改ID是:{ dataManager.user.LastModifierId}");
                Console.WriteLine($"员工上次修改时间是:{ dataManager.user.LastModifyTime}");
            }
        }
        public void FindUserList()
        {
            FindAll();
            if (dataManager.userList != null)
            {
                for (int i = 0; i < dataManager.userList.Count; i++)
                {
                    Console.WriteLine($"员工ID是:{ dataManager.userList[i].Id}");
                    Console.WriteLine($"员工名称是:{ dataManager.userList[i].Name}");
                    Console.WriteLine($"员工账号是:{ dataManager.userList[i].Account}");
                    Console.WriteLine($"员工密码是:{ dataManager.userList[i].Password}");
                    Console.WriteLine($"员工邮箱是:{ dataManager.userList[i].Email}");
                    Console.WriteLine($"员工手机号码是:{ dataManager.userList[i].Mobile}");
                    Console.WriteLine($"员工公司ID是:{ dataManager.userList[i].CompanyId}");
                    Console.WriteLine($"员工公司名称是:{ dataManager.userList[i].CompanyName}");
                    Console.WriteLine($"员工状态是:{ dataManager.userList[i].State}");
                    Console.WriteLine($"员工类型是:{ dataManager.userList[i].UserType}");
                    Console.WriteLine($"员工上次登录时间是:{ dataManager.userList[i].LastLoginTime}");
                    Console.WriteLine($"员工创建时间是:{ dataManager.userList[i].CreateTime}");
                    Console.WriteLine($"员工创建ID是:{ dataManager.userList[i].CreatorId}");
                    Console.WriteLine($"员工上次修改ID是:{ dataManager.userList[i].LastModifierId}");
                    Console.WriteLine($"员工上次修改时间是:{ dataManager.userList[i].LastModifyTime}");
                }
            }
        }
        public void FindCompany()
        {
            GetTableName();
            Find();
            if (dataManager.company != null)
            {
                Console.WriteLine($"公司ID是:{ dataManager.company.Id}");
                Console.WriteLine($"公司名称是:{ dataManager.company.Name}");
                Console.WriteLine($"公司创建时间是:{ dataManager.company.CreateTime}");
                Console.WriteLine($"公司创建人是:{ dataManager.company.CreatorId}");
                Console.WriteLine($"公司修改人是:{ dataManager.company.LastModifierId}");
                Console.WriteLine($"公司上次修改时间是:{ dataManager.company.LastModifyTime}");
            }
        }
        public void FindCompanyList()
        {
            FindAll();
            if (dataManager.companyList != null)
            {
                for (int i = 0; i < dataManager.companyList.Count; i++)
                {
                    Console.WriteLine($"公司ID是:{ dataManager.companyList[i].Id}");
                    Console.WriteLine($"公司名称是:{ dataManager.companyList[i].Name}");
                    Console.WriteLine($"公司创建时间是:{ dataManager.companyList[i].CreateTime}");
                    Console.WriteLine($"公司创建人是:{ dataManager.companyList[i].CreatorId}");
                    Console.WriteLine($"公司修改人是:{ dataManager.companyList[i].LastModifierId}");
                    Console.WriteLine($"公司上次修改时间是:{ dataManager.companyList[i].LastModifyTime}");
                }
            }
        }

        public void GetProperties(string tableName)
        {
            List<string> names = dataManager.GetProperties(tableName);
            Console.WriteLine(tableName + "的属性有：");
            for (int i = 0; i < names.Count; i++)
            {
                Console.WriteLine(names[i]);
            }
        }

        public void InsertCompanyRows()
        {
            FindCompanyList();
            dataManager.InsertRows(dataManager.companyList);
        }
    }
}
