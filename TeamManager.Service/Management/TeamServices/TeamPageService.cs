using TeamManager.Service.Management.Database;
using TeamManager.Service.Models;

namespace TeamManager.Service.Management
{
    public class TeamPageService
    {
        IManagerDatabaseConnection connection;

        public TeamPageService(IManagerDatabaseConnection connection)
        {
            this.connection = connection;
        }

        public List<Team> GetAllTeams()
        {
            return connection.GetAllTeams();
        }

        public void DeleteTeam(Team team)
        {
            if (CheckIfTeamHasAnyMembers(team))
            {
                throw new ArgumentException("Can't delete the team! Team has members!");
            }
            if (connection.DeleteTeam(team) == false)
            {
                throw new ArgumentException("Can't delete the team!");
            }

            // Delete all user to team connections
            var allUserIDToTeamIDs = connection.GetAllUserIDToTeamID();
            var userToTeamIDs = allUserIDToTeamIDs.Where(a => a.TeamID == team.ID);
            foreach (var userToTeamID in userToTeamIDs)
            {
                connection.DeleteUserIDToTeamID(userToTeamID);
            }
        }

        private bool CheckIfTeamHasAnyMembers(Team team)
        {
            var allUserIDToTeamIDs = connection.GetAllUserIDToTeamID();

            if (allUserIDToTeamIDs != null && allUserIDToTeamIDs.Any(a => a.TeamID == team.ID))
            {
                return true;
            }

            return false;
        }
    }
}
