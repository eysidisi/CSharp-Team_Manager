using TeamManager.Service.Models;

namespace TeamManager.Service.Management.Database
{
    public interface IManagementDatabaseConnection
    {
        List<User> GetAllUsers();
        void SaveUser(User user);
        bool DeleteUser(User user);
        List<Team> GetAllTeams();
        bool DeleteTeam(Team team);
        void SaveTeam(Team newTeam);
        List<UserIDToTeamID> GetAllUserIDToTeamID();
        void SaveUserIDToTeamID(UserIDToTeamID userIDToTeamID);
        bool DeleteUserIDToTeamID(UserIDToTeamID userIDToTeamID);
    }
}
