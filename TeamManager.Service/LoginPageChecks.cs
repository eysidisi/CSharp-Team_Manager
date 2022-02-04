using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamManager.Service
{
    public class LoginPageChecks
    {
        IDatabaseConnection databaseConnection;

        public LoginPageChecks(IDatabaseConnection databaseConnection)
        {
            this.databaseConnection = databaseConnection;
        }

        public bool CheckIfUserExists(string userName, string password)
        {
            return databaseConnection.CheckIfUserExists(new User(userName,password));
        }
    }
}
