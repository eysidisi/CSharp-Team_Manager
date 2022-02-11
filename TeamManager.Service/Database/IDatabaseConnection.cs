using TeamManager.Service.Models;

namespace TeamManager.Service.Database
{
    public interface IDatabaseConnection
    {
        public bool CheckIfManagerExists(Manager user);
        List<Purpose> GetPurposes(string userName);
        public void SavePurpose(Purpose purpose);
        Manager GetManager(string userName);
        void SaveUser(User user);
        bool DeleteUser(User user);
        List<User> GetAllUsers();
        void SaveTeam(Team team);
        Team GetTeamWithName(string name);
        void SaveUserToTheTeam(int iD1, int iD2);
        void DeleteUserFromTheTeam(int iD1, int iD2);
        List<Team> GetAllTeams();
        bool DeleteTeam(Team team);
    }
}