using TeamManager.Service.Management.DatabaseManagers;
using TeamManager.Service.Management.Models;

namespace TeamManager.Service.Management.TeamServices
{
    public class EditTeamPageService
    {
        readonly DatabaseManager databaseManager;
        readonly Team team;
        public EditTeamPageService(DatabaseManager databaseManager, Team teamToEdit)
        {
            this.databaseManager = databaseManager;
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

            databaseManager.SaveUserIDToTeamID(userIDToTeamID);
        }

        public List<User> GetAllUsers()
        {
            return databaseManager.GetAllUsers();
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

            var users = databaseManager.GetAllUsers();
            return users.Where(u => userIDsThatAreInTheTeam.Contains(u.ID)).ToList();
        }

        private List<int> FindUserIDsThatAreInTheTeam()
        {
            var userIDToTeamIDs = databaseManager.GetAllUserIDToTeamID();
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

            databaseManager.DeleteUserIDToTeamID(correctUserIDToTeamID);
        }

        private UserIDToTeamID GetCorrectEntryFromDB(UserIDToTeamID userIDToTeamID)
        {
            var allUserIDsToTeamIDs = databaseManager.GetAllUserIDToTeamID();

            UserIDToTeamID copyItem = allUserIDsToTeamIDs.Find(u => u.UserID == userIDToTeamID.UserID && u.TeamID == userIDToTeamID.TeamID);

            return copyItem;
        }

        private bool CheckIfUserIsInTheTeam(UserIDToTeamID userIDToTeamID)
        {
            var allUserIDsToTeamIDs = databaseManager.GetAllUserIDToTeamID();

            UserIDToTeamID? copyItem = allUserIDsToTeamIDs.Find(u => u.UserID == userIDToTeamID.UserID && u.TeamID == userIDToTeamID.TeamID);

            if (copyItem != null)
            {
                return true;
            }

            return false;
        }

        private bool CheckIfUserExistsInDB(User user)
        {
            return databaseManager.GetAllUsers().Find(u => u.ID == user.ID) != null ? true : false;
        }

        private bool CheckIfTeamExistsInDB()
        {
            return databaseManager.GetAllTeams().Find(t => t.ID == team.ID) != null ? true : false;
        }
    }
}
