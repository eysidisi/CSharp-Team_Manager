using TeamManager.Service.Management.DatabaseManagers;
using TeamManager.Service.Management.Models;

namespace TeamManager.Service.Management.UserServices
{
    public class UserPageService
    {
        private readonly DatabaseManager databaseManager;

        public UserPageService(DatabaseManager databaseManager)
        {
            this.databaseManager = databaseManager;
        }

        public List<User> GetUsers()
        {
            return databaseManager.GetAllUsers();
        }

        public void DeleteUser(User user)
        {
            if (databaseManager.DeleteUser(user) == false)
            {
                throw new ArgumentException("Can't delete the user!");
            }

            DeleteAllUserIDToTeamIDEntries(user);
        }

        private void DeleteAllUserIDToTeamIDEntries(User user)
        {
            var allUserIDToTeamIDs = databaseManager.GetAllUserIDToTeamID();
            var userToTeamIDs = allUserIDToTeamIDs.Where(a => a.UserID == user.ID).ToList();

            foreach (var userToTeamID in userToTeamIDs)
            {
                databaseManager.DeleteUserIDToTeamID(userToTeamID);
            }
        }
    }
}
