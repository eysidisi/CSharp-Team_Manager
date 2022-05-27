using TeamManager.Service.Management.DatabaseManagers;
using TeamManager.Service.Management.Models;

namespace TeamManager.Service.Management.TeamServices
{
    public class TeamPageService
    {
        readonly ManagerDatabaseController databaseManager;

        public TeamPageService(ManagerDatabaseController databaseManager)
        {
            this.databaseManager = databaseManager;
        }

        public List<Team> GetAllTeams()
        {
            return databaseManager.GetAllTeams();
        }
        public void DeleteTeam(Team team)
        {
            if (CheckIfTeamHasAnyMembers(team))
            {
                throw new ArgumentException("Can't delete the team! Team has members!");
            }

            if (databaseManager.DeleteTeam(team) == false)
            {
                throw new ArgumentException("Can't delete the team!");
            }
        }
        private bool CheckIfTeamHasAnyMembers(Team team)
        {
            var allUserIDToTeamIDs = databaseManager.GetAllUserIDToTeamID();

            if (allUserIDToTeamIDs != null && allUserIDToTeamIDs.Any(a => a.TeamID == team.ID))
            {
                return true;
            }

            return false;
        }
    }
}
