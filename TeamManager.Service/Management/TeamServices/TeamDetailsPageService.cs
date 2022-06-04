using TeamManager.Service.Management.DatabaseController;
using TeamManager.Service.Management.Models;

namespace TeamManager.Service.Management.TeamServices
{
    public class TeamDetailsPageService
    {
        readonly ManagerDatabaseController databaseController;
        readonly Team team;

        public TeamDetailsPageService(ManagerDatabaseController databaseController, Team team)
        {
            this.databaseController = databaseController;
            this.team = team;
        }

        public List<User> GetUsersInTeam()
        {
            var users = databaseController.GetAllUsers();
            var teams = databaseController.GetAllTeams();
            var userIDToTeamIDs = databaseController.GetAllUserIDToTeamID();

            var userIDsBelongedToTeam = userIDToTeamIDs.Where(u => u.TeamID == team.ID)
                                                        .Select(u => u.UserID).ToList();
            return users.Where(u => userIDsBelongedToTeam.Contains(u.ID)).ToList();
        }
    }
}
