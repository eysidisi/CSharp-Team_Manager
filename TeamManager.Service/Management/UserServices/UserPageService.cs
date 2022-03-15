using TeamManager.Service.Management.Database;
using TeamManager.Service.Models;

namespace TeamManager.Service.Management
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

            DeleteAllUserIDToTeamIDEntries(user);
        }

        private void DeleteAllUserIDToTeamIDEntries(User user)
        {
            var allUserIDToTeamIDs = connection.GetAllUserIDToTeamID();
            var userToTeamIDs = allUserIDToTeamIDs.Where(a => a.UserID == user.ID).ToList();

            foreach (var userToTeamID in userToTeamIDs)
            {
                connection.DeleteUserIDToTeamID(userToTeamID);
            }
        }
    }
}
