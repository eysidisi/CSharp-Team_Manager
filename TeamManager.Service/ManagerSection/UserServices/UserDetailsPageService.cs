using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;

namespace TeamManager.Service.ManagerSection.UserServices
{
    public class UserDetailsPageService
    {
        private IManagerDatabaseConnection connection;

        public UserDetailsPageService(IManagerDatabaseConnection connection)
        {
            this.connection = connection;
        }

        public List<Team> GetTeamsThatUserIn(User user)
        {
            var allUserIDToTeamIDs = connection.GetAllUserIDToTeamID();
            var allTeams = connection.GetAllTeams();
            
            var teamIDs = allUserIDToTeamIDs.Where(a => a.UserID == user.ID)?.
                Select(a => a.TeamID).ToList();

            if (teamIDs == null)
            {
                return new List<Team>();
            }

            return allTeams.Where(a => teamIDs.Contains(a.ID)).ToList();
        }
    }
}
