using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Wizard.Database;

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
            return databaseConnection.CheckIfUserExists(new User(userName, password));
        }

        public User GetUser(string userName)
        {
            return databaseConnection.GetUser(userName);
        }
    }
}
