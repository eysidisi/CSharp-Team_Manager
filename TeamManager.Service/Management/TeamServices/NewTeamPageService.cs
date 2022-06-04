using TeamManager.Service.Management.DatabaseController;
using TeamManager.Service.Management.Models;

namespace TeamManager.Service.Management.TeamServices
{
    public class NewTeamPageService
    {
        readonly ManagerDatabaseController databaseController;

        public NewTeamPageService(ManagerDatabaseController databaseController)
        {
            this.databaseController = databaseController;
        }

        public void SaveTeam(Team newTeam)
        {
            AddCreationTimeInfo(newTeam);

            if (CheckIfTeamIsValid(newTeam) == false)
            {
                throw new ArgumentException("Make sure that all of the sections are filled!");
            }
            var allTeams = databaseController.GetAllTeams();

            if (allTeams.Any(t => t.Name == newTeam.Name))
            {
                throw new ArgumentException("A team with the same name already exists!");
            }

            databaseController.SaveTeam(newTeam);
        }

        private void AddCreationTimeInfo(Team newTeam)
        {
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            newTeam.CreationDate = sqlFormattedDate;
        }

        private bool CheckIfTeamIsValid(Team newTeam)
        {
            if (string.IsNullOrEmpty(newTeam.Name))
            {
                return false;
            }

            return true;
        }
    }
}
