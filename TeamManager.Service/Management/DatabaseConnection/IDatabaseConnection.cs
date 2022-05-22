using TeamManager.Service.Management.Models;

namespace TeamManager.Service.Management.DatabaseConnection
{
    public interface IDatabaseConnection
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
