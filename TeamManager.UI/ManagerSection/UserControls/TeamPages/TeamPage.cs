using System.Data;
using TeamManager.Service.ManagerSection;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;

namespace TeamManager.UI.ManagerSection.UserControls
{
    public partial class TeamPage : UserControl
    {
        TeamPageService teamPageService;
        IManagerDatabaseConnection connection;
        public TeamPage(IManagerDatabaseConnection connection)
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
            ResizeColumns(dataGridViewTeams);
        }
        private void ResizeColumns(DataGridView dataGrid)
        {
            int width = dataGrid.Width;
            int minColWidth = (int)Math.Ceiling(width / (double)dataGrid.Columns.Count);
            for (int i = 0; i < dataGrid.Columns.Count; i++)
            {
                dataGrid.Columns[i].MinimumWidth = minColWidth;
            }
        }

        private void buttonDeleteTeam_Click(object sender, EventArgs e)
        {
            try
            {
                Team teamToDelete = GetSelectedTeam();
                
                DialogResult d = MessageBox.Show($"Do you want to delete team '{teamToDelete.Name}'?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (d == DialogResult.No)
                {
                    return;
                }

                teamPageService.DeleteTeam(teamToDelete);
                (dataGridViewTeams.SelectedRows[0].DataBoundItem as DataRowView).Delete();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
            try
            {
                HideAllItems();
                OpenNewTeamPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenNewTeamPage()
        {
           var newTeamPageUserControl = new NewTeamPage(connection);
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
            try
            {
                Team team = GetSelectedTeam();
                var teamDetailsPageUserControl = new TeamDetailsPage(connection, team);
                teamDetailsPageUserControl.OnBackButtonClicked += OnBackButtonClicked;
                HideAllItems();
                Controls.Add(teamDetailsPageUserControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OnBackButtonClicked(UserControl pageToClose)
        {
            pageToClose.Dispose();
            ExposeAllItems();
        }

        private void buttonEditTeam_Click(object sender, EventArgs e)
        {
            try
            {
                Team team = GetSelectedTeam();
                var editTeamPageUserControl = new EditTeamPage(connection, team);
                editTeamPageUserControl.OnBackButtonClicked += OnBackButtonClicked;
                HideAllItems();
                Controls.Add(editTeamPageUserControl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
