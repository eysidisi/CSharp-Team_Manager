using System.Data;
using TeamManager.Service.ManagerSection;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;

namespace TeamManager.UI.UserControls
{
    public partial class TeamPageUserControl : UserControl
    {
        TeamPageService teamPageService;
        DataTable teamsDataTable;
        public TeamPageUserControl(IManagerDatabaseConnection connection)
        {
            InitializeComponent();
            teamPageService = new TeamPageService(connection);
            FillTeamsTable();
        }

        private void FillTeamsTable()
        {
            var teams = teamPageService.GetAllTeams();
            teamsDataTable = HelperFunctions.ConvertToDatatable(teams);
            dataGridViewTeams.DataSource = teamsDataTable;
            dataGridViewTeams.AutoResizeColumns();
        }

        private void buttonDeleteTeam_Click(object sender, EventArgs e)
        {
            Team teamToDelete = GetTeamToDelete();
            teamPageService.DeleteTeam(teamToDelete);
            (dataGridViewTeams.SelectedRows[0].DataBoundItem as DataRowView).Delete();
        }

        private Team GetTeamToDelete()
        {
            DataRowView selectedRow = dataGridViewTeams.SelectedRows[0].DataBoundItem as DataRowView;
            int selectedUserId = (int)selectedRow["ID"];
            var allTeams = teamPageService.GetAllTeams();
            return allTeams.Find(t => t.ID == selectedUserId);
        }
    }
}
