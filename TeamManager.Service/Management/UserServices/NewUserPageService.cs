using TeamManager.Service.Management.DatabaseController;
using TeamManager.Service.Management.Models;

namespace TeamManager.Service.Management.UserServices
{
    public class NewUserPageService
    {
        readonly ManagerDatabaseController databaseController;

        public NewUserPageService(ManagerDatabaseController databaseController)
        {
            this.databaseController = databaseController;
        }

        public void SaveNewUser(User user)
        {
            AddCreationTimeInfo(user);

            if (CheckIfUserIsValid(user) == false)
            {
                throw new ArgumentException("User is not valid!");
            }

            databaseController.SaveUser(user);
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
