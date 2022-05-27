using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Management.Models;

namespace TeamManager.Service.Management.DatabaseConnection.DapperSupportedDatabaseConnections
{
    public abstract class ManagerDapperSupportedDatabaseConnection : IManagerDatabaseConnection
    {
        protected string connectionString;

        protected ManagerDapperSupportedDatabaseConnection(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected abstract DbConnection CreateConnection();

        public bool DeleteTeam(Team team)
        {
            using (var connection = CreateConnection())
            {
                return connection.Delete(team);
            }
        }

        public bool DeleteUser(User user)
        {
            using (var connection = CreateConnection())
            {
                return connection.Delete(user);
            }
        }

        public bool DeleteUserIDToTeamID(UserIDToTeamID userIDToTeamID)
        {
            using (var connection = CreateConnection())
            {
                return connection.Delete(userIDToTeamID);
            }
        }

        public List<Team> GetAllTeams()
        {
            using (var connection = CreateConnection())
            {
                return connection.GetAll<Team>().ToList();
            }
        }

        public List<UserIDToTeamID> GetAllUserIDToTeamID()
        {
            using (var connection = CreateConnection())
            {
                return connection.GetAll<UserIDToTeamID>().ToList();
            }
        }

        public List<User> GetAllUsers()
        {
            using (var connection = CreateConnection())
            {
                return connection.GetAll<User>().ToList();
            }
        }

        public void SaveTeam(Team newTeam)
        {
            using (var connection = CreateConnection())
            {
                connection.Insert(newTeam);
            }
        }

        public void SaveUser(User user)
        {
            using (var connection = CreateConnection())
            {
                connection.Insert(user);
            }
        }

        public void SaveUserIDToTeamID(UserIDToTeamID userIDToTeamID)
        {
            using (var connection = CreateConnection())
            {
                connection.Insert(userIDToTeamID);
            }
        }
    }
}
