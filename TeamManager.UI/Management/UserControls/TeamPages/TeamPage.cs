using System.Data;
using TeamManager.Service.Management.DatabaseController;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.TeamServices;

namespace TeamManager.UI.Management.UserControls
{
    public partial class TeamPage : UserControl
    {
        readonly TeamPageService teamPageService;
        readonly ManagerDatabaseController databaseController;
        DataViewPage<Team> dataViewPage;

        public TeamPage(ManagerDatabaseController databaseController)
        {
            InitializeComponent();
            this.databaseController = databaseController;
            teamPageService = new TeamPageService(databaseController);
            CreateDataViewPage();
        }

        private void CreateDataViewPage()
        {
            dataViewPage = new DataViewPage<Team>(panelTeamsDataGridView);
            SetUpDataViewPage();
        }

        private void SetUpDataViewPage()
        {
            dataViewPage.SetUpPage(teamPageService.GetAllTeams());
        }

        private void buttonDeleteTeam_Click(object sender, EventArgs e)
        {
            try
            {
                TryToDeleteSelectedTeam();
                SetUpDataViewPage();
                MessageBox.Show("Team deleted succesfully!", "Success", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TryToDeleteSelectedTeam()
        {
            Team teamToDelete = TryToGetSelectedTeamInDataGridView();

            DialogResult d = MessageBox.Show($"Do you want to delete team '{teamToDelete.Name}'?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (d == DialogResult.Yes)
            {
                teamPageService.DeleteTeam(teamToDelete);
                dataViewPage.DeleteSelectedRow();
            }
            else
            {
                throw new Exception("Team was not deleted!");
            }
        }

        private Team TryToGetSelectedTeamInDataGridView()
        {
            int selectedItemID = FindSelectedItemIDInDataGridView();

            var allTeams = teamPageService.GetAllTeams();
            var selectedTeam = allTeams.Find(u => u.ID == selectedItemID);

            if (selectedTeam == null)
            {
                throw new Exception("Can't find the selected user! Please refresh the page!");
            }

            return selectedTeam;
        }

        private int FindSelectedItemIDInDataGridView()
        {
            DataRowView selectedRow = dataViewPage.GetSelectedRow();
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
            var newTeamPageUserControl = new NewTeamPage(databaseController);
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
                Team team = TryToGetSelectedTeamInDataGridView();
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
            var teamDetailsPageUserControl = new TeamDetailsPage(databaseController, team);
            teamDetailsPageUserControl.OnBackButtonClicked += OnBackButtonClicked;
            Controls.Add(teamDetailsPageUserControl);
        }

        private void OnBackButtonClicked(UserControl pageToClose)
        {
            pageToClose.Dispose();
            SetUpDataViewPage();
            ExposeAllItems();
        }

        private void buttonEditTeam_Click(object sender, EventArgs e)
        {
            try
            {
                HideAllItems();
                Team team = TryToGetSelectedTeamInDataGridView();
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
            var editTeamPageUserControl = new EditTeamPage(databaseController, team);
            editTeamPageUserControl.OnBackButtonClicked += OnBackButtonClicked;
            Controls.Add(editTeamPageUserControl);
        }
    }
}
