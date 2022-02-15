using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Models;

namespace TeamManager.Service.Management
{
    public class TeamDetailsPageService
    {
        IManagerDatabaseConnection connection;

        public TeamDetailsPageService(IManagerDatabaseConnection connection)
        {
            this.connection = connection;
        }

        public List<User> GetUsersInTeam(Team team)
        {
            var users = connection.GetAllUsers();
            var teams = connection.GetAllTeams();
            var userIDToTeamIDs = connection.GetAllUserIDToTeamID();

            if (users == null || teams == null || userIDToTeamIDs == null)
            {
                return new List<User>();
            }

            var userIDsBelongedToTeam = userIDToTeamIDs.Where(u => u.TeamID == team.ID)
                                                        .Select(u => u.UserID).ToList();
            return users.Where(u => userIDsBelongedToTeam.Contains(u.ID)).ToList();
        }
    }
}
