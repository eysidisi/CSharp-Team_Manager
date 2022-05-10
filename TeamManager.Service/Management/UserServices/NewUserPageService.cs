using TeamManager.Service.Management.Database;
using TeamManager.Service.Models;

namespace TeamManager.Service.Management
{
    public class NewUserPageService
    {
        IManagementDatabaseConnection connection;

        public NewUserPageService(IManagementDatabaseConnection connection)
        {
            this.connection = connection;
        }

        public void SaveNewUser(User user)
        {
            AddCreationTimeInfo(user);
            
            if(CheckIfUserIsValid(user)==false)
            {
                throw new ArgumentException("User is not valid!");
            }

            connection.SaveUser(user);
        }

        private bool CheckIfUserIsValid(User user)
        {
            if (string.IsNullOrEmpty(user.Name))
            {
                return false; 
            }

            return true;
        }

        private void AddCreationTimeInfo(User user)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            user.CreationDate = sqlFormattedDate;
        }

    }
}
