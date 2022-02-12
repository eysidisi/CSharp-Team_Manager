using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;

namespace TeamManager.Service.ManagerSection
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
            
            if (users == null)
            {
                return new List<User>();
            }

           return users.Where(u => u.TeamName == team.Name).ToList();
        }
    }
}
