using TeamManager.Service.Management.DatabaseControllers;
using TeamManager.Service.Management.Models;

namespace TeamManager.Service.Management.TeamServices
{
    public class EditTeamPageService
    {
        readonly ManagerDatabaseController databaseController;
        readonly Team team;
        public EditTeamPageService(ManagerDatabaseController databaseController, Team teamToEdit)
        {
            this.databaseController = databaseController;
            team = teamToEdit;
        }

        public void AddUserToTheTeam(User user)
        {
            if (CheckIfUserExistsInDB(user) == false)
            {
                throw new ArgumentException("User doesn't exist in the DB!");
            }

            if (CheckIfTeamExistsInDB() == false)
            {
                throw new ArgumentException("Team doesn't exist in the DB!");
            }

            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { UserID = user.ID, TeamID = team.ID };

            if (CheckIfUserIsInTheTeam(userIDToTeamID))
            {
                throw new ArgumentException("User is already in the team!");
            }

            databaseController.SaveUserIDToTeamID(userIDToTeamID);
        }

        public List<User> GetAllUsers()
        {
            return databaseController.GetAllUsers();
        }

        public List<User> TryToGetUsersInTheTeam()
        {
            if (CheckIfTeamExistsInDB() == false)
            {
                throw new ArgumentException("Team doesn't exist in DB!");
            }

            return GetUsersInTheTeam();
        }

        private List<User> GetUsersInTheTeam()
        {
            List<int> userIDsThatAreInTheTeam = FindUserIDsThatAreInTheTeam();

            var users = databaseController.GetAllUsers();
            return users.Where(u => userIDsThatAreInTheTeam.Contains(u.ID)).ToList();
        }

        private List<int> FindUserIDsThatAreInTheTeam()
        {
            var userIDToTeamIDs = databaseController.GetAllUserIDToTeamID();
            var userIDsBelongedToTeam = userIDToTeamIDs
                                        .Where(u => u.TeamID == team.ID)
                                        .Select(u => u.UserID).ToList();

            return userIDsBelongedToTeam;
        }

        public void RemoveUserFromTheTeam(User userToRemove)
        {
            if (CheckIfUserExistsInDB(userToRemove) == false)
            {
                throw new ArgumentException("User doesn't exist in the DB!");
            }

            if (CheckIfTeamExistsInDB() == false)
            {
                throw new ArgumentException("Team doesn't exist in the DB!");
            }

            UserIDToTeamID userIDToTeamID = new UserIDToTeamID()
            {
                UserID = userToRemove.ID,
                TeamID = team.ID
            };

            if (CheckIfUserIsInTheTeam(userIDToTeamID) == false)
            {
                throw new ArgumentException("User is not in the team!");
            }

            UserIDToTeamID correctUserIDToTeamID = GetCorrectEntryFromDB(userIDToTeamID);

            databaseController.DeleteUserIDToTeamID(correctUserIDToTeamID);
        }

        private UserIDToTeamID GetCorrectEntryFromDB(UserIDToTeamID userIDToTeamID)
        {
            var allUserIDsToTeamIDs = databaseController.GetAllUserIDToTeamID();

            UserIDToTeamID copyItem = allUserIDsToTeamIDs.Find(u => u.UserID == userIDToTeamID.UserID && u.TeamID == userIDToTeamID.TeamID);

            return copyItem;
        }

        private bool CheckIfUserIsInTheTeam(UserIDToTeamID userIDToTeamID)
        {
            var allUserIDsToTeamIDs = databaseController.GetAllUserIDToTeamID();

            UserIDToTeamID? copyItem = allUserIDsToTeamIDs.Find(u => u.UserID == userIDToTeamID.UserID && u.TeamID == userIDToTeamID.TeamID);

            if (copyItem != null)
            {
                return true;
            }

            return false;
        }

        private bool CheckIfUserExistsInDB(User user)
        {
            return databaseController.GetAllUsers().Find(u => u.ID == user.ID) != null ? true : false;
        }

        private bool CheckIfTeamExistsInDB()
        {
            return databaseController.GetAllTeams().Find(t => t.ID == team.ID) != null ? true : false;
        }
    }
}
