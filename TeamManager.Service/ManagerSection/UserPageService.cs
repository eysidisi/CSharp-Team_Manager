using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;

namespace TeamManager.Service.ManagerSection
{
    public class UserPageService
    {
        private IManagerDatabaseConnection connection;

        public UserPageService(IManagerDatabaseConnection connection)
        {
            this.connection = connection;
        }

        public void AddUser(User user)
        {
            AddCreationTimeInfo(user);
            if (CheckIfUserIsValid(user))
            {
                connection.SaveUser(user);
            }
            else
            {
                throw new ArgumentException("You need to fill all fields!");
            }
        }

        public List<User> GetUsers()
        {
            return connection.GetAllUsers();
        }

        public bool DeleteUser(User user)
        {
            return connection.DeleteUser(user);
        }

        // TODO: Add check mechanisms
        private bool CheckIfUserIsValid(User user)
        {
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
