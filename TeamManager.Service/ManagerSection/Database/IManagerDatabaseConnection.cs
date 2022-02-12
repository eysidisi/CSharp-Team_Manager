using TeamManager.Service.Models;

namespace TeamManager.Service.ManagerSection.Database
{
    public interface IManagerDatabaseConnection
    {
        List<User> GetAllUsers();
        void SaveUser(User user);
        bool DeleteUser(User user);
        Team GetTeamWithName(string name);
        List<Team> GetAllTeams();
        bool DeleteTeam(Team team);
        void SaveTeam(Team newTeam);
        List<UserIDToTeamID> GetAllUserIDToTeamID();
    }
}
