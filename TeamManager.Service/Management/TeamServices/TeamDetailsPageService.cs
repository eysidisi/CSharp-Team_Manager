using TeamManager.Service.Management.DatabaseManagers;
using TeamManager.Service.Management.Models;

namespace TeamManager.Service.Management.TeamServices
{
    public class TeamDetailsPageService
    {
        readonly DatabaseManager databaseManager;
        readonly Team team;

        public TeamDetailsPageService(DatabaseManager databaseManager, Team team)
        {
            this.databaseManager = databaseManager;
            this.team = team;
        }

        public List<User> GetUsersInTeam()
        {
            var users = databaseManager.GetAllUsers();
            var teams = databaseManager.GetAllTeams();
            var userIDToTeamIDs = databaseManager.GetAllUserIDToTeamID();

            var userIDsBelongedToTeam = userIDToTeamIDs.Where(u => u.TeamID == team.ID)
                                                        .Select(u => u.UserID).ToList();
            return users.Where(u => userIDsBelongedToTeam.Contains(u.ID)).ToList();
        }
    }
}
