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

        private bool CheckIfUserIsInTheTeam(UserIDToTeamID userIDToTeamID)
        {
            var allUserIDsToTeamIDs = connection.GetAllUserIDToTeamID();
            
            // No entry
            if (allUserIDsToTeamIDs==null)
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
