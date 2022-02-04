using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
