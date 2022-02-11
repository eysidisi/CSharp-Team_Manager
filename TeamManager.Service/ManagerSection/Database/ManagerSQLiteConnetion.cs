using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SQLite;
using TeamManager.Service.Models;

namespace TeamManager.Service.ManagerSection.Database
{
    public class ManagerSQLiteConnetion : IManagerDatabaseConnection
    {
        string connString;

        public ManagerSQLiteConnetion(string connString)
        {
            this.connString = connString;
        }

        public void SaveUser(User user)
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(user);
            }
        }

        public bool DeleteUser(User user)
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                return cnn.Delete(user);
            }
        }

        public List<User> GetAllUsers()
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                var users = cnn.GetAll<User>().ToList();

                return users;
            }
        }

        public void SaveTeam(Team team)
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(team);
            }
        }

        public Team GetTeamWithName(string name)
        {
            string query = $"SELECT * From Teams where Name = @Name";

            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                var output = cnn.Query<Team>(query, new Team() { Name = name });

                if (output == null || output.Count() == 0)
                {
                    throw new Exception("Can't find team!");
                }

                return output.First();
            }
        }

        public void SaveUserToTheTeam(int userID, int teamID)
        {
            throw new NotImplementedException();
        }

        public void DeleteUserFromTheTeam(int userID, int teamID)
        {
            throw new NotImplementedException();
        }

        public List<Team> GetAllTeams()
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                var teams = cnn.GetAll<Team>().ToList();

                return teams;
            }
        }

        public bool DeleteTeam(Team team)
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                return cnn.Delete(team);
            }
        }
    }
}
