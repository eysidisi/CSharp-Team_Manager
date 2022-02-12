using System.Data;
using TeamManager.Service.ManagerSection;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;

namespace TeamManager.UI.ManagerSection.UserControls
{
    public partial class TeamPageUserControl : UserControl
    {
        TeamPageService teamPageService;
        NewTeamPageUserControl newTeamPageUserControl;
        DataTable teamsDataTable;
        IManagerDatabaseConnection connection;
        public TeamPageUserControl(IManagerDatabaseConnection connection)
        {
            InitializeComponent();
            this.connection=connection; 
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

        private void buttonAddNewTeam_Click(object sender, EventArgs e)
        {
            HideAllItems();
            OpenNewTeamPage();
        }

        private void OpenNewTeamPage()
        {
            newTeamPageUserControl = new NewTeamPageUserControl(connection);
            newTeamPageUserControl.OnCancelClick += OnAddNewTeamCancelClicked;
            Controls.Add(newTeamPageUserControl);
        }

        private void OnAddNewTeamCancelClicked()
        {
            newTeamPageUserControl.Dispose();
            FillTeamsTable();
            ExposeAllItems();
        }

        private void ExposeAllItems()
        {
            foreach (Control control in Controls)
            {
                control.Visible = true;
            }
        }

        private void HideAllItems()
        {
            foreach (Control control in Controls)
            {
                control.Hide();
            }
        }
    }
}
