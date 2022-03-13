using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SQLite;
using TeamManager.Service.Models;

namespace TeamManager.Service.Management.Database
{
    public class ManagerSQLiteConnetion : IManagerDatabaseConnection
    {
        string connString;
        List<User> users = null;
        List<Team> teams = null;
        List<UserIDToTeamID> userIDsToTeamIDs = null;
        public ManagerSQLiteConnetion(string connString)
        {
            this.connString = connString;
        }

        public void SaveUser(User user)
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(user);
                users.Add(user);
            }
        }

        public bool DeleteUser(User user)
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                if (cnn.Delete(user) == false)
                {
                    return false;
                }

                else
                {
                    users.Remove(user);
                    return true;
                }
            }
        }

        public List<User> GetAllUsers()
        {
            if (users == null)
            {
                using (IDbConnection cnn = new SQLiteConnection(connString))
                {
                    users = cnn.GetAll<User>().ToList();
                }
            }

            return users;
        }

        public void SaveTeam(Team team)
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(team);
                teams.Add(team);
            }
        }

        public List<Team> GetAllTeams()
        {
            if (teams == null)
            {
                using (IDbConnection cnn = new SQLiteConnection(connString))
                {
                    teams = cnn.GetAll<Team>().ToList();
                }
            }

            return teams;
        }

        public bool DeleteTeam(Team team)
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                if (cnn.Delete(team) == false)
                {
                    return false;
                }
                else
                {
                    teams.Remove(team);
                    return true;
                }
            }
        }

        public List<UserIDToTeamID> GetAllUserIDToTeamID()
        {
            if (userIDsToTeamIDs == null)
            {
                using (IDbConnection cnn = new SQLiteConnection(connString))
                {
                    userIDsToTeamIDs = cnn.GetAll<UserIDToTeamID>().ToList();
                }
            }
            return userIDsToTeamIDs;
        }

        public void SaveUserIDToTeamID(UserIDToTeamID userIDToTeamID)
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                cnn.Insert(userIDToTeamID);
                userIDsToTeamIDs.Add(userIDToTeamID);
            }
        }

        public bool DeleteUserIDToTeamID(UserIDToTeamID userIDToTeamID)
        {
            using (IDbConnection cnn = new SQLiteConnection(connString))
            {
                if (cnn.Delete(userIDToTeamID) == false)
                {
                    return false;
                }
                else
                {
                    userIDsToTeamIDs.Remove(userIDToTeamID);
                    return true;
                }
            }
        }
    }
}
