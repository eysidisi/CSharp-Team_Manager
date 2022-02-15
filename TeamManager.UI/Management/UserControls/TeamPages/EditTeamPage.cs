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
        Team teamToEdit;
        EditTeamPageService editTeamPageService;
        public Action<EditTeamPage> OnBackButtonClicked;

        public EditTeamPage(IManagerDatabaseConnection connection, Team teamToEdit)
        {
            InitializeComponent();
            this.teamToEdit = teamToEdit;
            editTeamPageService = new EditTeamPageService(connection);
            SetHeaderText();
            FillUsersTable();
            FillTeamTable();
        }

        private void SetHeaderText()
        {
            labelTeamName.Text = teamToEdit.Name;
        }

        private void FillTeamTable()
        {
            var allUsersInTheTeam = editTeamPageService.GetUsersInTeam(teamToEdit);
            var usersDataTable = HelperFunctions.ConvertToDatatable(allUsersInTheTeam);
            dataGridViewTeamUsers.DataSource = usersDataTable;
            ResizeColumns(dataGridViewTeamUsers);
        }

        private void FillUsersTable()
        {
            var allUsers = editTeamPageService.GetUsers();
            var usersDataTable = HelperFunctions.ConvertToDatatable(allUsers);
            dataGridViewUsers.DataSource = usersDataTable;
            ResizeColumns(dataGridViewUsers);
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
                User selectedUser = GetSelectedUser(dataGridViewUsers);
                DialogResult d = MessageBox.Show($"Do you want to add user '{selectedUser.Name}' to the team?", "Add", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (d == DialogResult.No)
                {
                    return;
                }

                editTeamPageService.AddUserToTheTeam(selectedUser, teamToEdit);
                FillTeamTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private User GetSelectedUser(DataGridView dataGridView)
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
                User selectedUser = GetSelectedUser(dataGridViewTeamUsers);
                DialogResult d = MessageBox.Show($"Do you want to remove user '{selectedUser.Name}' from the team?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (d == DialogResult.No)
                {
                    return;
                }

                editTeamPageService.RemoveUserFromTheTeam(selectedUser, teamToEdit);
                FillTeamTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            OnBackButtonClicked?.Invoke(this);
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            FillUsersTable();
            FillTeamTable();
        }
    }
}
