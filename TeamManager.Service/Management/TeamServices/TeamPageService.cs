using TeamManager.Service.Management.DatabaseController;
using TeamManager.Service.Management.Models;

namespace TeamManager.Service.Management.TeamServices
{
    public class TeamPageService
    {
        readonly ManagerDatabaseController databaseController;

        public TeamPageService(ManagerDatabaseController databaseController)
        {
            this.databaseController = databaseController;
        }

        public List<Team> GetAllTeams()
        {
            return databaseController.GetAllTeams();
        }
        public void DeleteTeam(Team team)
        {
            if (CheckIfTeamHasAnyMembers(team))
            {
                throw new ArgumentException("Can't delete the team! Team has members!");
            }

            if (databaseController.DeleteTeam(team) == false)
            {
                throw new ArgumentException("Can't delete the team!");
            }
        }
        private bool CheckIfTeamHasAnyMembers(Team team)
        {
            var allUserIDToTeamIDs = databaseController.GetAllUserIDToTeamID();

            if (allUserIDToTeamIDs.Any(a => a.TeamID == team.ID))
            {
                return true;
            }

            return false;
        }
    }
}
