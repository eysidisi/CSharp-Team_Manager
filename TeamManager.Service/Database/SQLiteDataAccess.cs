using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SQLite;
using TeamManager.Service.Models;

namespace TeamManager.Service.Database
{
    public class SQLiteDataAccess : IDatabaseConnection
    {
        string connString;

        public SQLiteDataAccess(string connString)
        {
            this.connString = connString;
        }

        public bool CheckIfManagerExists(Manager manager)
        {
            string query = $"SELECT * From Managers where UserName = @UserName and Password = @Password";

            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                List<Manager>? output = cnn.Query<Manager>(query, manager).ToList();

                if (output != null && output.Count() == 1)
                {
                    return true;
                }
                else if (output.Count() > 1)
                {
                    throw new Exception("Two same managers are registered to the DB");
                }
                else
                {
                    return false;
                }
            }
        }

        public void SavePurpose(Purpose purpose)
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(purpose);
            }
        }

        public List<Purpose> GetPurposes(string userName)
        {
            string query = $"SELECT * From Purposes where UserName = @UserName";

            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                var output = cnn.Query<Purpose>(query, new Purpose() { UserName = userName });

                if (output == null || output.Count() == 0)
                {
                    throw new Exception("Can't find purpose related to that manager!");
                }

                return output.ToList();
            }
        }

        public Manager GetManager(string userName)
        {
            string query = $"SELECT * From Purposes where UserName = @UserName";

            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                var output = cnn.Query<Manager>(query, new Manager() { UserName = "asd" });

                if (output == null || output.Count() == 0)
                {
                    throw new Exception("Can't find manager!");
                }

                return output.First();
            }
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
