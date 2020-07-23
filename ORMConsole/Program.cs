using ORMBLL;
using System;

namespace ORMConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ShowData showData = new ShowData();
            //showData.FindUser();
            //showData.FindCompany();

            //showData.FindUserList();
            //showData.FindCompanyList();
            showData.InsertCompanyRows();
        }

        
    }
}
