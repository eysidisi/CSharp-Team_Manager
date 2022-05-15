using TeamManager.Service.Management.Database;
using TeamManager.Service.Models;

namespace TeamManager.Service.Management
{
    public class TeamPageService
    {
        const int NumOfTeamsPerPage = 10;
        IManagementDatabaseConnection connection;
        List<Team> teams;
        public int MaxNumOfPages { get;private set; }

        public TeamPageService(IManagementDatabaseConnection connection)
        {
            this.connection = connection;
            teams = connection.GetAllTeams();
            CalculateMaximumNumberOfPages();
        }

        private void CalculateMaximumNumberOfPages()
        {
            MaxNumOfPages = (int)Math.Ceiling(teams.Count / ((double)NumOfTeamsPerPage));
            MaxNumOfPages = Math.Max(MaxNumOfPages, 1);
        }

        public List<Team> GetAllTeams()
        {
            return connection.GetAllTeams();
        }

        public List<Team> GetTeamsInPage(int pageNum)
        {
            int startingIndexInList = ((pageNum - 1) * NumOfTeamsPerPage);
            int endingIndexInList = startingIndexInList + NumOfTeamsPerPage;
            Range range = new Range(startingIndexInList, endingIndexInList);
            return teams.Take(range).ToList();
        }

        public void DeleteTeam(Team team)
        {
            if (CheckIfTeamHasAnyMembers(team))
            {
                throw new ArgumentException("Can't delete the team! Team has members!");
            }

            if (connection.DeleteTeam(team) == false)
            {
                throw new ArgumentException("Can't delete the team!");
            }
        }

        private bool CheckIfTeamHasAnyMembers(Team team)
        {
            var allUserIDToTeamIDs = connection.GetAllUserIDToTeamID();

            if (allUserIDToTeamIDs != null && allUserIDToTeamIDs.Any(a => a.TeamID == team.ID))
            {
                return true;
            }

            return false;
        }
    }
}
