using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Database;
using TeamManager.Service.Models;

namespace TeamManager.Service.Wizard.LoginPage
{
    public class LoginPageService
    {
        IDatabaseConnection databaseConnection;

        public LoginPageService(IDatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }

        public bool CheckIfUserExists(string userName, string password)
        {
            return databaseConnection.CheckIfManagerExists(new Manager(userName, password));
        }

        public Manager GetManager(string userName)
        {
            return databaseConnection.GetManager(userName);
        }
    }
}
