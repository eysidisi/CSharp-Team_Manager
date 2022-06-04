using TeamManager.Service.Management.DatabaseController;
using TeamManager.Service.Management.Models;

namespace TeamManager.Service.Management.UserServices
{
    public class UserDetailsPageService
    {
        public int NumOfTeamsPerPage = 10;

        private readonly ManagerDatabaseController databaseController;
        private readonly User user;

        public UserDetailsPageService(ManagerDatabaseController databaseController, User user)
        {
            this.databaseController = databaseController;
            this.user = user;
        }
        public List<Team> GetTeamsThatUserIn()
        {
            var allUserIDToTeamIDs = databaseController.GetAllUserIDToTeamID();
            var allTeams = databaseController.GetAllTeams();

            var teamIDs = allUserIDToTeamIDs.Where(a => a.UserID == user.ID)?.
                Select(a => a.TeamID).ToList();

            return allTeams.Where(a => teamIDs.Contains(a.ID)).ToList();
        }
    }
}
