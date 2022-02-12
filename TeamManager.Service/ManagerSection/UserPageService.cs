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

        public List<User> GetUsers()
        {
            return connection.GetAllUsers();
        }

        public void DeleteUser(User user)
        {
            if (connection.DeleteUser(user) == false)
            {
                throw new ArgumentException("Can't delete the user!");
            }
        }
    }
}
