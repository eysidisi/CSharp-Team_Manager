﻿using System.Data;
using TeamManager.Service.Management;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Models;

namespace TeamManager.UI.Management.UserControls
{
    public partial class TeamPage : UserControl
    {
        const int NumOfTeamsPerPage = 10;
        TeamPageService teamPageService;
        IManagementDatabaseConnection connection;
        public TeamPage(IManagementDatabaseConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            teamPageService = new TeamPageService(connection);
            DisplayTeamsInPage(1);
            AdjustPaginationComponent();
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

        private void AdjustPaginationComponent()
        {
            int maxNumOfPages = (int)Math.Ceiling(teamPageService.GetAllTeams().Count / ((double)NumOfTeamsPerPage));
            maxNumOfPages = Math.Max(maxNumOfPages, 1);
            paginationComponent.SetMaxPageNum(maxNumOfPages);
            paginationComponent.OnCurrentPageNumChanged += PageNumChanged;
        }

        private void PageNumChanged(int pageNum)
        {
            DisplayTeamsInPage(pageNum);
        }

        private void DisplayTeamsInPage(int pageNum)
        {
            int startingIndexInList = ((pageNum - 1) * NumOfTeamsPerPage);
            Range range = new Range(startingIndexInList, startingIndexInList + NumOfTeamsPerPage);
            var teamsToDisplay = teamPageService.GetAllTeams().Take(range).ToList();
            DisplayTeams(teamsToDisplay);
        }
        private void DisplayTeams(List<Team> teamsToShowInPage)
        {
            var teamsDataTable = HelperFunctions.ConvertToDatatable(teamsToShowInPage);
            dataGridViewTeams.DataSource = teamsDataTable;
            ResizeColumns(dataGridViewTeams);
        }

        private void buttonDeleteTeam_Click(object sender, EventArgs e)
        {
            try
            {
                Team teamToDelete = GetSelectedTeam(dataGridViewTeams);

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

        private Team GetSelectedTeam(DataGridView dataGridView)
        {
            if (dataGridView.SelectedRows.Count < 1)
            {
                throw new Exception("No item is selected! Please select an item first!");
            }

            DataRowView selectedRow = dataGridView.SelectedRows[0].DataBoundItem as DataRowView;
            int selectedItemID = (int)selectedRow["ID"];
            var allTeams = teamPageService.GetAllTeams();
            var selectedItem = allTeams.Find(u => u.ID == selectedItemID);

            if (selectedItem == null)
            {
                throw new Exception("Can't find the selected item! Please refresh the page!");
            }

            return selectedItem;
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
                Team team = GetSelectedTeam(dataGridViewTeams);
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
            DisplayTeamsInPage(paginationComponent.CurrentPageNumber);
        }

        private void buttonEditTeam_Click(object sender, EventArgs e)
        {
            try
            {
                Team team = GetSelectedTeam(dataGridViewTeams);
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
