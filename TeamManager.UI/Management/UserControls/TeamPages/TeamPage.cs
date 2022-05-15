using System.Data;
using TeamManager.Service.Management;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Models;

namespace TeamManager.UI.Management.UserControls
{
    public partial class TeamPage : UserControl
    {
        TeamPageService teamPageService;
        IManagementDatabaseConnection connection;
        public TeamPage(IManagementDatabaseConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            teamPageService = new TeamPageService(connection);
            AdjustPaginationComponent();
            DisplayTeamsInPage(1);
        }

        private void AdjustPaginationComponent()
        {
            paginationComponent.SetMaxPageNum(teamPageService.MaxNumOfPages);
            paginationComponent.OnCurrentPageNumChanged += PageNumChanged;
        }

        private void PageNumChanged(int pageNum)
        {
            DisplayTeamsInPage(pageNum);
        }

        private void DisplayTeamsInPage(int pageNum)
        {
            List<Team> teamsToDisplay = teamPageService.GetTeamsInPage(pageNum);
            var teamsDataTable = HelperFunctions.ConvertListToDatatable(teamsToDisplay);
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
                TryToDeleteSelectedTeam();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TryToDeleteSelectedTeam()
        {
            Team teamToDelete = GetSelectedTeamInDataGridView();

            DialogResult d = MessageBox.Show($"Do you want to delete team '{teamToDelete.Name}'?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (d == DialogResult.Yes)
            {
                teamPageService.DeleteTeam(teamToDelete);
                (dataGridViewTeams.SelectedRows[0].DataBoundItem as DataRowView).Delete();
                RefreshTeamsDataGridView();
            }
        }

        private Team GetSelectedTeamInDataGridView()
        {
            if (dataGridViewTeams.SelectedRows.Count < 1)
            {
                throw new Exception("No item is selected! Please select an item first!");
            }

            int selectedItemID = FindSelectedItemIDInDataGridView();
            var allTeams = teamPageService.GetAllTeams();
            var selectedItem = allTeams.Find(u => u.ID == selectedItemID);

            return selectedItem;
        }

        private int FindSelectedItemIDInDataGridView()
        {
            DataRowView selectedRow = dataGridViewTeams.SelectedRows[0].DataBoundItem as DataRowView;
            int selectedItemID = (int)selectedRow["ID"];
            return selectedItemID;
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
                HideAllItems();
                Team team = GetSelectedTeamInDataGridView();
                CreateTeamDetailsPage(team);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ExposeAllItems();
            }
        }

        private void CreateTeamDetailsPage(Team team)
        {
            var teamDetailsPageUserControl = new TeamDetailsPage(connection, team);
            teamDetailsPageUserControl.OnBackButtonClicked += OnBackButtonClicked;
            Controls.Add(teamDetailsPageUserControl);
        }

        private void OnBackButtonClicked(UserControl pageToClose)
        {
            pageToClose.Dispose();
            RefreshTeamsDataGridView();
            ExposeAllItems();
        }

        private void RefreshTeamsDataGridView()
        {
            teamPageService = new TeamPageService(connection);
            AdjustPaginationComponent();
            DisplayTeamsInPage(1);
        }

        private void buttonEditTeam_Click(object sender, EventArgs e)
        {
            try
            {
                HideAllItems();
                Team team = GetSelectedTeamInDataGridView();
                CreateEditTeamPage(team);
            }
            catch (Exception ex)
            {
                ExposeAllItems();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateEditTeamPage(Team team)
        {
            var editTeamPageUserControl = new EditTeamPage(connection, team);
            editTeamPageUserControl.OnBackButtonClicked += OnBackButtonClicked;
            Controls.Add(editTeamPageUserControl);
        }
    }
}
