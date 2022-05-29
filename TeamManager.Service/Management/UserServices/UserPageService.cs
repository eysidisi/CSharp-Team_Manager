using TeamManager.Service.Management.DatabaseControllers;
using TeamManager.Service.Management.Models;

namespace TeamManager.Service.Management.UserServices
{
    public class UserPageService
    {
        private readonly ManagerDatabaseController databaseController;

        public UserPageService(ManagerDatabaseController databaseController)
        {
            this.databaseController = databaseController;
        }

        public List<User> GetUsers()
        {
            return databaseController.GetAllUsers();
        }

        public void DeleteUser(User user)
        {
            if (databaseController.DeleteUser(user) == false)
            {
                throw new ArgumentException("Can't delete the user!");
            }

            DeleteAllUserIDToTeamIDEntries(user);
        }

        private void DeleteAllUserIDToTeamIDEntries(User user)
        {
            var allUserIDToTeamIDs = databaseController.GetAllUserIDToTeamID();
            var userToTeamIDs = allUserIDToTeamIDs.Where(a => a.UserID == user.ID).ToList();

            foreach (var userToTeamID in userToTeamIDs)
            {
                databaseController.DeleteUserIDToTeamID(userToTeamID);
            }
        }
    }
}
