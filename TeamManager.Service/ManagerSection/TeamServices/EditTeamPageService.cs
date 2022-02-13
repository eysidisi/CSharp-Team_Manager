using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;

namespace TeamManager.Service.ManagerSection.TeamServices
{
    public class EditTeamPageService
    {
        private IManagerDatabaseConnection connection;

        public EditTeamPageService(IManagerDatabaseConnection connection)
        {
            this.connection = connection;
        }

        public void AddUserToTheTeam(User user, Team team)
        {
            UserIDToTeamID userIDToTeamID = new UserIDToTeamID() { UserID = user.ID, TeamID = team.ID };

            if (CheckIfUserIsInTheTeam(userIDToTeamID))
            {
                throw new ArgumentException("User is already in the team!");
            }

            connection.SaveUserIDToTeamID(userIDToTeamID);
        }

        public List<User> GetUsers()
        {
            return connection.GetAllUsers();
        }

        public List<User> GetUsersInTeam(Team team)
        {
            var users = connection.GetAllUsers();
            var teams = connection.GetAllTeams();
            var userIDToTeamIDs = connection.GetAllUserIDToTeamID();

            if (users == null || teams == null || userIDToTeamIDs == null)
            {
                return new List<User>();
            }

            var userIDsBelongedToTeam = userIDToTeamIDs.Where(u => u.TeamID == team.ID)
                                                        .Select(u => u.UserID).ToList();
            return users.Where(u => userIDsBelongedToTeam.Contains(u.ID)).ToList();
        }

        public void RemoveUserFromTheTeam(User userToRemove, Team teamToRemoveFrom)
        {
            UserIDToTeamID userIDToTeamID = new UserIDToTeamID()
            {
                UserID = userToRemove.ID,
                TeamID = teamToRemoveFrom.ID
            };

            if (CheckIfUserIsInTheTeam(userIDToTeamID) == false)
            {
                throw new ArgumentException("User is not in the team!");
            }

            UserIDToTeamID correctUserIDToTeamID = GetCorrectEntryFromDB(userIDToTeamID);

            connection.DeleteUserIDToTeamID(correctUserIDToTeamID);
        }

        private UserIDToTeamID GetCorrectEntryFromDB(UserIDToTeamID userIDToTeamID)
        {
            var allUserIDsToTeamIDs = connection.GetAllUserIDToTeamID();

            // No entry
            if (allUserIDsToTeamIDs == null)
            {
                return null;
            }

            UserIDToTeamID? copyItem = allUserIDsToTeamIDs.Find(u => u.UserID == userIDToTeamID.UserID && u.TeamID == userIDToTeamID.TeamID);

            return copyItem;
        }

        private bool CheckIfUserIsInTheTeam(UserIDToTeamID userIDToTeamID)
        {
            var allUserIDsToTeamIDs = connection.GetAllUserIDToTeamID();

            // No entry
            if (allUserIDsToTeamIDs == null)
            {
                return false;
            }

            UserIDToTeamID? copyItem = allUserIDsToTeamIDs.Find(u => u.UserID == userIDToTeamID.UserID && u.TeamID == userIDToTeamID.TeamID);

            if (copyItem != null)
            {
                return true;
            }

            return false;
        }
    }
}
