using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;

namespace TeamManager.Service.ManagerSection
{
    public class TeamPageService
    {
        IManagerDatabaseConnection connection;

        public TeamPageService(IManagerDatabaseConnection connection)
        {
            this.connection = connection;
        }
        private bool CheckIfTeamExists(Team team)
        {
            Team t = connection.GetTeamWithName(team.Name);

            if (t == null)
                return false;

            return true;
        }

        public List<Team> GetAllTeams()
        {
            return connection.GetAllTeams();
        }

        public void DeleteTeam(Team team)
        {
            connection.DeleteTeam(team);
        }
    }
}
