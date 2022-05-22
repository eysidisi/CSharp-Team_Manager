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
        IManagementDatabaseConnection connection;
        Team team;
        public const int NumOfTeamsPerPage = 10;

        public TeamDetailsPageService(IManagementDatabaseConnection connection,Team team)
        {
            this.connection = connection;
            this.team = team;
        }

        public List<User> GetUsersInTeam()
        {
            var users = connection.GetAllUsers();
            var teams = connection.GetAllTeams();
            var userIDToTeamIDs = connection.GetAllUserIDToTeamID();

            var userIDsBelongedToTeam = userIDToTeamIDs.Where(u => u.TeamID == team.ID)
                                                        .Select(u => u.UserID).ToList();
            return users.Where(u => userIDsBelongedToTeam.Contains(u.ID)).ToList();
        }
    }
}
