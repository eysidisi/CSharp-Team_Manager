using TeamManager.Service.Management.DatabaseManagers;
using TeamManager.Service.Management.Models;

namespace TeamManager.Service.Management.UserServices
{
    public class UserDetailsPageService
    {
        public int NumOfTeamsPerPage = 10;

        private readonly ManagerDatabaseController databaseManager;
        private readonly User user;

        public UserDetailsPageService(ManagerDatabaseController databaseManager, User user)
        {
            this.databaseManager = databaseManager;
            this.user = user;
        }
        public List<Team> GetTeamsThatUserIn()
        {
            var allUserIDToTeamIDs = databaseManager.GetAllUserIDToTeamID();
            var allTeams = databaseManager.GetAllTeams();

            var teamIDs = allUserIDToTeamIDs.Where(a => a.UserID == user.ID)?.
                Select(a => a.TeamID).ToList();

            return allTeams.Where(a => teamIDs.Contains(a.ID)).ToList();
        }
    }
}
