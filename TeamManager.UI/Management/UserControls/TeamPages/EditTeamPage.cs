using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeamManager.Service.Management;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Management.TeamServices;
using TeamManager.Service.Models;

namespace TeamManager.UI.Management.UserControls
{
    public partial class EditTeamPage : UserControl
    {
        public Action<EditTeamPage> OnBackButtonClicked;
        const int NumberOfElementsPerPage = 10;
        Team teamToEdit;
        EditTeamPageService editTeamPageService;

        public EditTeamPage(IManagementDatabaseConnection connection, Team teamToEdit)
        {
            InitializeComponent();
            this.teamToEdit = teamToEdit;
            editTeamPageService = new EditTeamPageService(connection);
            SetHeaderText();
            FillUsersTable();
            FillTeamUsersTable();
        }

        private void SetHeaderText()
        {
            labelTeamName.Text = teamToEdit.Name;
        }

        private void FillTeamUsersTable()
        {
            var allUsersInTheTeam = editTeamPageService.GetUsersInTeam(teamToEdit);
            var numberOfMaxPages = CalculateNumberOfMaxPages(allUsersInTheTeam);
            AdjustPaginationPageTeamUsers(numberOfMaxPages);
            DisplayTeamUsersInPage(1);
        }

        private void DisplayTeamUsersInPage(int pageNumber)
        {
            var usersInTeam = editTeamPageService.GetUsersInTeam(teamToEdit);
            Range range = GetCorrectRangeForPage(pageNumber);
            var usersToDisplay = usersInTeam.Take(range).ToList();
            var usersDataTable = HelperFunctions.ConvertToDatatable(usersToDisplay);
            dataGridViewTeamUsers.DataSource = usersDataTable;
            ResizeColumns(dataGridViewAllUsers);
        }

        private void AdjustPaginationPageTeamUsers(int numberOfMaxPages)
        {
            paginationPageTeamUsers.SetMaxPageNum(numberOfMaxPages);
            paginationPageTeamUsers.OnCurrentPageNumChanged += OnTeamUsersPaginationNumberChanged;
        }

        private void OnTeamUsersPaginationNumberChanged(int pageNumber)
        {
            DisplayTeamUsersInPage(pageNumber);
        }

        private void FillUsersTable()
        {
            var allUsers = editTeamPageService.GetUsers();
            var numberOfMaxPages = CalculateNumberOfMaxPages(allUsers);
            AdjustPaginationPageAllUsers(numberOfMaxPages);
            DisplayAllUsersInPage(1);
        }

        private void DisplayAllUsersInPage(int pageNumber)
        {
            var allUsers = editTeamPageService.GetUsers();
            Range range = GetCorrectRangeForPage(pageNumber);
            var usersToDisplay = allUsers.Take(range).ToList();
            var usersDataTable = HelperFunctions.ConvertToDatatable(usersToDisplay);
            dataGridViewAllUsers.DataSource = usersDataTable;
            ResizeColumns(dataGridViewAllUsers);
        }

        private Range GetCorrectRangeForPage(int pageNumber)
        {
            int startingIndex = (pageNumber - 1) * NumberOfElementsPerPage;
            int endingIndex = startingIndex + NumberOfElementsPerPage;
            Range range = new Range(startingIndex, endingIndex);
            return range;
        }

        private void AdjustPaginationPageAllUsers(int numberOfMaxPages)
        {
            paginationPageAllUsers.SetMaxPageNum(numberOfMaxPages);
            paginationPageAllUsers.OnCurrentPageNumChanged += OnUsersPaginationNumberChanged;
        }

        private int CalculateNumberOfMaxPages(List<User> allUsers)
        {
            return (int)Math.Ceiling((double)allUsers.Count / (NumberOfElementsPerPage));
        }

        private void OnUsersPaginationNumberChanged(int pageNumber)
        {
            DisplayAllUsersInPage(pageNumber);
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

        private void buttonAddSelectedUser_Click(object sender, EventArgs e)
        {
            try
            {
                TryToAddSelectedUser();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TryToAddSelectedUser()
        {
            User selectedUser = TryToGetSelectedUserInDataGridView(dataGridViewAllUsers);

            DialogResult d = MessageBox.Show($"Do you want to add user '{selectedUser.Name}' to the team?", "Add", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (d == DialogResult.Yes)
            {
                editTeamPageService.AddUserToTheTeam(selectedUser, teamToEdit);
                FillTeamUsersTable();
            }
        }

        private User TryToGetSelectedUserInDataGridView(DataGridView dataGridView)
        {
            if (dataGridView.SelectedRows.Count < 1)
            {
                throw new Exception("No user is selected! Please select a user first!");
            }

            DataRowView selectedRow = dataGridView.SelectedRows[0].DataBoundItem as DataRowView;
            int selectedUserId = (int)selectedRow["ID"];
            var allUsers = editTeamPageService.GetUsers();
            var selectedUser = allUsers.Find(u => u.ID == selectedUserId);

            if (selectedUser == null)
            {
                throw new Exception("Can't find the selected user! Please refresh the page!");
            }

            return selectedUser;
        }

        private void buttonRemoveSelectedUser_Click(object sender, EventArgs e)
        {
            try
            {
                TryToRemoveSelectedUser();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TryToRemoveSelectedUser()
        {
            User selectedUser = TryToGetSelectedUserInDataGridView(dataGridViewTeamUsers);
            DialogResult d = MessageBox.Show($"Do you want to remove user '{selectedUser.Name}' from the team?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (d == DialogResult.Yes)
            {
                editTeamPageService.RemoveUserFromTheTeam(selectedUser, teamToEdit);
                FillTeamUsersTable();
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            OnBackButtonClicked?.Invoke(this);
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            FillUsersTable();
            FillTeamUsersTable();
        }
    }
}
