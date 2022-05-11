using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SQLite;
using TeamManager.Service.Models;

namespace TeamManager.Service.Management.Database
{
    public abstract class ManagementDatabaseManager : IManagementDatabaseConnection
    {
        List<User> users = null;
        List<Team> teams = null;
        List<UserIDToTeamID> userIDsToTeamIDs = null;
        protected IDbConnection dbConnection;
        protected string connString;

        public ManagementDatabaseManager(string connString)
        {
            this.connString = connString;
            CreateConnection();
        }

        public void SaveUser(User user)
        {
            dbConnection.Insert(user);

            if (users != null)
            {
                users.Add(user);
            }
        }

        public bool DeleteUser(User user)
        {
            if (dbConnection.Delete(user) == false)
            {
                return false;
            }

            else
            {
                if (users != null)
                {
                    users.Remove(user);
                }
                return true;
            }
        }

        public List<User> GetAllUsers()
        {
            if (users == null)
            {
                users = dbConnection.GetAll<User>().ToList();
            }

            return users;
        }

        public void SaveTeam(Team team)
        {
            dbConnection.Insert(team);
            if (teams != null)
            {
                teams.Add(team);
            }
        }

        public List<Team> GetAllTeams()
        {
            if (teams == null)
            {
                teams = dbConnection.GetAll<Team>().ToList();
            }

            return teams;
        }

        public bool DeleteTeam(Team team)
        {
            if (dbConnection.Delete(team) == false)
            {
                return false;
            }
            else
            {
                if (teams != null)
                {
                    teams.Remove(team);
                }
                return true;
            }
        }

        public List<UserIDToTeamID> GetAllUserIDToTeamID()
        {
            if (userIDsToTeamIDs == null)
            {
                userIDsToTeamIDs = dbConnection.GetAll<UserIDToTeamID>().ToList();
            }
            return userIDsToTeamIDs;
        }

        public void SaveUserIDToTeamID(UserIDToTeamID userIDToTeamID)
        {
            dbConnection.Insert(userIDToTeamID);
            if (userIDsToTeamIDs != null)
            {
                userIDsToTeamIDs.Add(userIDToTeamID);
            }
        }

        public bool DeleteUserIDToTeamID(UserIDToTeamID userIDToTeamID)
        {
            if (dbConnection.Delete(userIDToTeamID) == false)
            {
                return false;
            }
            else
            {
                if (userIDsToTeamIDs != null)
                {
                    userIDsToTeamIDs.Remove(userIDToTeamID);
                }
                return true;
            }
        }
        protected abstract void CreateConnection();
    }
}
