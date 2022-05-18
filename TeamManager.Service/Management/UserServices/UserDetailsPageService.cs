using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Models;

namespace TeamManager.Service.Management.UserServices
{
    public class UserDetailsPageService
    {
        public int NumOfTeamsPerPage = 10;

        private IManagementDatabaseConnection connection;
        private User user;

        public UserDetailsPageService(IManagementDatabaseConnection connection, User user)
        {
            this.connection = connection;
            this.user = user;
        }

        public List<Team> GetTeamsThatUserIn()
        {
            var allUserIDToTeamIDs = connection.GetAllUserIDToTeamID();
            var allTeams = connection.GetAllTeams();

            var teamIDs = allUserIDToTeamIDs.Where(a => a.UserID == user.ID)?.
                Select(a => a.TeamID).ToList();

            return allTeams.Where(a => teamIDs.Contains(a.ID)).ToList();
        }
    }
}
