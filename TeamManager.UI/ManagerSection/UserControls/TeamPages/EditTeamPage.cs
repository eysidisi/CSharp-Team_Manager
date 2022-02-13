using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeamManager.Service.ManagerSection;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.ManagerSection.TeamServices;
using TeamManager.Service.Models;

namespace TeamManager.UI.ManagerSection.UserControls
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
            dataGridViewTeamUsers.AutoResizeColumns();
        }

        private void FillUsersTable()
        {
            var allUsers = editTeamPageService.GetUsers();
            var usersDataTable = HelperFunctions.ConvertToDatatable(allUsers);
            dataGridViewUsers.DataSource = usersDataTable;
            dataGridViewUsers.AutoResizeColumns();
        }

        private void buttonAddSelectedUser_Click(object sender, EventArgs e)
        {
            try
            {
                User selectedUser = GetUserToAdd();
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

        private User GetUserToAdd()
        {
            DataRowView selectedRow = dataGridViewUsers.SelectedRows[0].DataBoundItem as DataRowView;
            int selectedUserId = (int)selectedRow["ID"];
            var allUsers = editTeamPageService.GetUsers();
            return allUsers.Find(u => u.ID == selectedUserId);
        }

        private void buttonRemoveSelectedUser_Click(object sender, EventArgs e)
        {
            try
            {
                User selectedUser = GetUserToRemove();
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

        private User GetUserToRemove()
        {
            DataRowView selectedRow = dataGridViewTeamUsers.SelectedRows[0].DataBoundItem as DataRowView;
            int selectedUserId = (int)selectedRow["ID"];
            var allUsers = editTeamPageService.GetUsers();
            return allUsers.Find(u => u.ID == selectedUserId);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            OnBackButtonClicked?.Invoke(this);
        }
    }
}
