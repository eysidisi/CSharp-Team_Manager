using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;

namespace TeamManager.Service.ManagerSection
{
    public class NewUserPageService
    {
        IManagerDatabaseConnection connection;

        public NewUserPageService(IManagerDatabaseConnection connection)
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
