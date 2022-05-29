using System.Data;
using TeamManager.Service.Management.DatabaseControllers;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.TeamServices;

namespace TeamManager.UI.Management.UserControls
{
    public partial class EditTeamPage : UserControl
    {
        public Action<EditTeamPage> OnBackButtonClicked;
        readonly EditTeamPageService editTeamPageService;
        DataViewPage<User> allUsersDataViewPage;
        DataViewPage<User> teamUsersDataViewPage;

        public EditTeamPage(ManagerDatabaseController databaseController, Team teamToEdit)
        {
            InitializeComponent();
            editTeamPageService = new EditTeamPageService(databaseController, teamToEdit);
            SetHeaderText(teamToEdit.Name);
            CreateAllUsersDataViewPage();
            CreateTeamUsersDataViewPage();
        }

        private void CreateTeamUsersDataViewPage()
        {
            teamUsersDataViewPage = new DataViewPage<User>(panelTeamUsersDataViewPage);
            SetUpTeamUsersDataViewPage();
        }

        private void SetUpTeamUsersDataViewPage()
        {
            var usersInTeam = editTeamPageService.TryToGetUsersInTheTeam();
            teamUsersDataViewPage.SetUpPage(usersInTeam);
        }

        private void CreateAllUsersDataViewPage()
        {
            allUsersDataViewPage = new DataViewPage<User>(panelAllUsersDataViewPage);
            SetUpAllUsersDataViewPage();
        }

        private void SetUpAllUsersDataViewPage()
        {
            var allUsers = editTeamPageService.GetAllUsers();
            allUsersDataViewPage.SetUpPage(allUsers);
        }

        private void SetHeaderText(string teamName)
        {
            labelTeamName.Text = teamName;
        }


        private void buttonAddSelectedUser_Click(object sender, EventArgs e)
        {
            try
            {
                TryToAddSelectedUser();
                SetUpTeamUsersDataViewPage();
                MessageBox.Show("User added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TryToAddSelectedUser()
        {
            User selectedUser = TryToGetSelectedUserInDataGridView(allUsersDataViewPage);

            DialogResult d = MessageBox.Show($"Do you want to add user '{selectedUser.Name}' to the team?", "Add", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (d == DialogResult.Yes)
            {
                editTeamPageService.AddUserToTheTeam(selectedUser);
            }
        }

        private User TryToGetSelectedUserInDataGridView(DataViewPage<User> dataViewPage)
        {
            DataRowView selectedRow = dataViewPage.GetSelectedRow();
            User selectedUser = GetUserFromTheRow(selectedRow);

            return selectedUser;
        }

        private User GetUserFromTheRow(DataRowView selectedRow)
        {
            int selectedUserId = (int)selectedRow["ID"];
            var allUsers = editTeamPageService.GetAllUsers();
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
                SetUpTeamUsersDataViewPage();
                MessageBox.Show("User removed successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void TryToRemoveSelectedUser()
        {
            User selectedUser = TryToGetSelectedUserInDataGridView(teamUsersDataViewPage);
            DialogResult d = MessageBox.Show($"Do you want to remove user '{selectedUser.Name}' from the team?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (d == DialogResult.Yes)
            {
                editTeamPageService.RemoveUserFromTheTeam(selectedUser);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            OnBackButtonClicked?.Invoke(this);
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            SetUpTeamUsersDataViewPage();
            SetUpAllUsersDataViewPage();
        }
    }
}
