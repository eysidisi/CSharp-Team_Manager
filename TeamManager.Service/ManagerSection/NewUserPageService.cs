using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Database;
using TeamManager.Service.Models;

namespace TeamManager.Service.ManagerSection
{
    public class NewUserPageService
    {
        IDatabaseConnection connection;

        public NewUserPageService(IDatabaseConnection connection)
        {
            this.connection = connection;
        }

        public void SaveNewUser(User user)
        {
            CheckUser(user);
            connection.SaveUser(user);
        }

        private void CheckUser(User user)
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                throw new ArgumentException("User name can not be empty!");
            }
        }
    }
}
