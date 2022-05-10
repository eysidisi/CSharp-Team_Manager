using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Models;

namespace TeamManager.Service.Management
{
    public class NewTeamPageService
    {
        private IManagementDatabaseConnection connection;

        public NewTeamPageService(IManagementDatabaseConnection connection)
        {
            this.connection = connection;
        }

        public void SaveTeam(Team newTeam)
        {
            AddCreationTimeInfo(newTeam);

            if (CheckIfTeamIsValid(newTeam) == false)
            {
                throw new ArgumentException("Make sure that all of the sections are filled!");
            }
            var allTeams = connection.GetAllTeams();

            if (allTeams != null && allTeams.Any(t => t.Name == newTeam.Name))
            {
                throw new ArgumentException("A team with the same name already exists!");
            }

            connection.SaveTeam(newTeam);
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
