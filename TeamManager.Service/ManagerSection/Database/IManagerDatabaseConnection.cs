using TeamManager.Service.Models;

namespace TeamManager.Service.ManagerSection.Database
{
    public interface IManagerDatabaseConnection
    {
        void SaveUser(User user);
        bool DeleteUser(User user);
        List<User> GetAllUsers();
        Team GetTeamWithName(string name);
        List<Team> GetAllTeams();
        bool DeleteTeam(Team team);
        void SaveTeam(Team newTeam);
    }
}
