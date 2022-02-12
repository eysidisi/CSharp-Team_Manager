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
            AddCreationTimeInfo(user);
            CheckIfUserIsValid(user);
            connection.SaveUser(user);
        }

        private void CheckIfUserIsValid(User user)
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                throw new ArgumentException("User name can not be empty!");
            }
        }
        private void AddCreationTimeInfo(User user)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            user.CreationDate = sqlFormattedDate;
        }

    }
}
