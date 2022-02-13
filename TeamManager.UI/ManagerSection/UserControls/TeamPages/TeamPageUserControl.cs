using System.Data;
using TeamManager.Service.ManagerSection;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;

namespace TeamManager.UI.ManagerSection.UserControls
{
    public partial class TeamPageUserControl : UserControl
    {
        TeamPageService teamPageService;
        IManagerDatabaseConnection connection;
        public TeamPageUserControl(IManagerDatabaseConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            teamPageService = new TeamPageService(connection);
            FillTeamsTable();
        }

        private void FillTeamsTable()
        {
            var teams = teamPageService.GetAllTeams();
            var teamsDataTable = HelperFunctions.ConvertToDatatable(teams);
            dataGridViewTeams.DataSource = teamsDataTable;
            dataGridViewTeams.AutoResizeColumns();
        }

        private void buttonDeleteTeam_Click(object sender, EventArgs e)
        {
            Team teamToDelete = GetSelectedTeam();
            teamPageService.DeleteTeam(teamToDelete);
            (dataGridViewTeams.SelectedRows[0].DataBoundItem as DataRowView).Delete();
        }

        private Team GetSelectedTeam()
        {
            DataRowView selectedRow = dataGridViewTeams.SelectedRows[0].DataBoundItem as DataRowView;
            int selectedTeamID = (int)selectedRow["ID"];
            var allTeams = teamPageService.GetAllTeams();
            return allTeams.Find(t => t.ID == selectedTeamID);
        }

        private void buttonAddNewTeam_Click(object sender, EventArgs e)
        {
            HideAllItems();
            OpenNewTeamPage();
        }

        private void OpenNewTeamPage()
        {
           var newTeamPageUserControl = new NewTeamPageUserControl(connection);
            newTeamPageUserControl.OnBackButtonClicked += OnBackButtonClicked;
            Controls.Add(newTeamPageUserControl);
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

        private void buttonTeamDetails_Click(object sender, EventArgs e)
        {
            Team team = GetSelectedTeam();
            var teamDetailsPageUserControl = new TeamDetailsPageUserControl(connection, team);
            teamDetailsPageUserControl.OnBackButtonClicked += OnBackButtonClicked;
            HideAllItems();
            Controls.Add(teamDetailsPageUserControl);
        }

        private void OnBackButtonClicked(UserControl pageToClose)
        {
            pageToClose.Dispose();
            ExposeAllItems();
        }

        private void buttonEditTeam_Click(object sender, EventArgs e)
        {
            Team team = GetSelectedTeam();
            var editTeamPageUserControl = new EditTeamPageUserControl(connection, team);
            editTeamPageUserControl.OnBackButtonClicked += OnBackButtonClicked;
            HideAllItems();
            Controls.Add(editTeamPageUserControl);
        }
    }
}
