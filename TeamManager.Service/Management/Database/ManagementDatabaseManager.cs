using Dapper;
using Dapper.Contrib.Extensions;
using System.Data;
using System.Data.SQLite;
using TeamManager.Service.Models;

namespace TeamManager.Service.Management.Database
{
    public abstract class ManagementDatabaseManager : IManagementDatabaseConnection
    {
        List<User> users;
        List<Team> teams;
        List<UserIDToTeamID> userIDsToTeamIDs;
        protected IDbConnection dbConnection;
        protected string connString;

        public ManagementDatabaseManager(string connString)
        {
            this.connString = connString;
            CreateConnection();
        }

        protected abstract void CreateConnection();

        public void SaveUser(User user)
        {
            dbConnection.Insert(user);

            if (users != null)
            {
                InsertUser(user);
            }
        }

        private void InsertUser(User user)
        {
            users.Add(user);
            users = users.OrderBy(u => u.ID).ToList();
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
                FillUsersList();
            }

            return users;
        }

        private void FillUsersList()
        {
            users = dbConnection.GetAll<User>().ToList();
            users = users.OrderBy(u => u.ID).ToList();
        }

        public void SaveTeam(Team team)
        {
            dbConnection.Insert(team);
            if (teams != null)
            {
                InsertTeam(team);
            }
        }

        private void InsertTeam(Team team)
        {
            teams.Add(team);
            teams = teams.OrderBy(t => t.ID).ToList();
        }

        public List<Team> GetAllTeams()
        {
            if (teams == null)
            {
                FillTeamsList();
            }

            return teams;
        }

        private void FillTeamsList()
        {
            teams = dbConnection.GetAll<Team>().ToList();
            teams = teams.OrderBy(t => t.ID).ToList();
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
    }
}
