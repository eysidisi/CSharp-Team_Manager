using System.Data;
using TeamManager.Service.Management.DatabaseConnection;
using TeamManager.Service.Management.Models;

namespace TeamManager.Service.Management.DatabaseManagers
{
    public abstract class DatabaseManager
    {
        protected string connString;
        List<User> users;
        List<Team> teams;
        List<UserIDToTeamID> userIDsToTeamIDs;
        readonly IDatabaseConnection connection;

        public DatabaseManager()
        {

        }

        protected DatabaseManager(string connString)
        {
            this.connString = connString;
            connection = CreateConnection();
        }

        protected abstract IDatabaseConnection CreateConnection();

        public virtual void SaveUser(User user)
        {
            connection.SaveUser(user);

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

        public virtual bool DeleteUser(User user)
        {
            if (connection.DeleteUser(user) == false)
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

        public virtual List<User> GetAllUsers()
        {
            if (users == null)
            {
                FillUsersList();
            }

            return users;
        }

        private void FillUsersList()
        {
            users = connection.GetAllUsers();
            users = users.OrderBy(u => u.ID).ToList();
        }

        public virtual void SaveTeam(Team team)
        {
            connection.SaveTeam(team);
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

        public virtual List<Team> GetAllTeams()
        {
            if (teams == null)
            {
                FillTeamsList();
            }

            return teams;
        }

        private void FillTeamsList()
        {
            teams = connection.GetAllTeams();
            teams = teams.OrderBy(t => t.ID).ToList();
        }

        public virtual bool DeleteTeam(Team team)
        {
            if (connection.DeleteTeam(team) == false)
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

        public virtual List<UserIDToTeamID> GetAllUserIDToTeamID()
        {
            if (userIDsToTeamIDs == null)
            {
                userIDsToTeamIDs = connection.GetAllUserIDToTeamID();
            }
            return userIDsToTeamIDs;
        }

        public virtual void SaveUserIDToTeamID(UserIDToTeamID userIDToTeamID)
        {
            connection.SaveUserIDToTeamID(userIDToTeamID);
            if (userIDsToTeamIDs != null)
            {
                userIDsToTeamIDs.Add(userIDToTeamID);
            }
        }

        public virtual bool DeleteUserIDToTeamID(UserIDToTeamID userIDToTeamID)
        {
            if (connection.DeleteUserIDToTeamID(userIDToTeamID) == false)
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
