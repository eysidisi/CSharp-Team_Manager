using Dapper.Contrib.Extensions;
using System.Data.SQLite;
using TeamManager.Service.Management.Models;

namespace TeamManager.Service.Management.DatabaseConnection
{
    public class ManagerSQLiteDatabaseConnection : IManagerDatabaseConnection
    {
        readonly string connString;

        public ManagerSQLiteDatabaseConnection(string connString)
        {
            this.connString = connString;
        }

        public bool DeleteTeam(Team team)
        {
            using (var connection = new SQLiteConnection(connString))
            {
                return connection.Delete(team);
            }
        }

        public bool DeleteUser(User user)
        {
            using (var connection = new SQLiteConnection(connString))
            {
                return connection.Delete(user);
            }
        }

        public bool DeleteUserIDToTeamID(UserIDToTeamID userIDToTeamID)
        {
            using (var connection = new SQLiteConnection(connString))
            {
                return connection.Delete(userIDToTeamID);
            }
        }

        public List<Team> GetAllTeams()
        {
            using (var connection = new SQLiteConnection(connString))
            {
                return connection.GetAll<Team>().ToList();
            }
        }

        public List<UserIDToTeamID> GetAllUserIDToTeamID()
        {
            using (var connection = new SQLiteConnection(connString))
            {
                return connection.GetAll<UserIDToTeamID>().ToList();
            }
        }

        public List<User> GetAllUsers()
        {
            using (var connection = new SQLiteConnection(connString))
            {
                return connection.GetAll<User>().ToList();
            }
        }

        public void SaveTeam(Team newTeam)
        {
            using (var connection = new SQLiteConnection(connString))
            {
                connection.Insert(newTeam);
            }
        }

        public void SaveUser(User user)
        {
            using (var connection = new SQLiteConnection(connString))
            {
                connection.Insert(user);
            }
        }

        public void SaveUserIDToTeamID(UserIDToTeamID userIDToTeamID)
        {
            using (var connection = new SQLiteConnection(connString))
            {
                connection.Insert(userIDToTeamID);
            }
        }
    }
}
