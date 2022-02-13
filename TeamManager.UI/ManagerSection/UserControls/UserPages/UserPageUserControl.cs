using System.Data;
using TeamManager.Service.ManagerSection;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;
using TeamManager.UI.ManagerSection.UserControls.UserPages;

namespace TeamManager.UI.ManagerSection.UserControls
{
    public partial class UserPageUserControl : UserControl
    {
        UserPageService userPageService;
        DataTable usersDataTable;
        List<User> allUsers;
        IManagerDatabaseConnection connection;

        public UserPageUserControl(IManagerDatabaseConnection connection)
        {
            InitializeComponent();
            userPageService = new UserPageService(connection);
            this.connection = connection;
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            allUsers = userPageService.GetUsers();
            usersDataTable = HelperFunctions.ConvertToDatatable(allUsers);
            dataGridViewUsers.DataSource = usersDataTable;
            dataGridViewUsers.AutoResizeColumns();
        }

        private void OpenNewUserPage()
        {
            var saveNewUserPage = new NewUserPageUserControl(connection);
            saveNewUserPage.OnCancelClick += OnBackButtonClicked;
            Controls.Add(saveNewUserPage);
        }

        private void OnBackButtonClicked(UserControl userControl)
        {
            userControl.Dispose();
            FillDataGrid();
            ExposeAllItems();
        }

        private void ExposeAllItems()
        {
            foreach (Control control in Controls)
            {
                control.Visible = true;
            }
        }

        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            User userToDelete = GetSelectedUser();

            DialogResult d = MessageBox.Show($"Do you want to delete user '{userToDelete.Name}'?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (d == DialogResult.No)
            {
                return;
            }

            userPageService.DeleteUser(userToDelete);
            (dataGridViewUsers.SelectedRows[0].DataBoundItem as DataRowView).Delete();
        }

        private User GetSelectedUser()
        {
            DataRowView selectedRow = dataGridViewUsers.SelectedRows[0].DataBoundItem as DataRowView;
            int selectedUserId = (int)selectedRow["ID"];
            return allUsers.Find(u => u.ID == selectedUserId);
        }

        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            HideAllItems();
            OpenNewUserPage();
        }

        private void HideAllItems()
        {
            foreach (Control control in Controls)
            {
                control.Hide();
            }
        }

        private void buttonUserDetails_Click(object sender, EventArgs e)
        {
            User selectedUser = GetSelectedUser();
            HideAllItems();
            OpenNewUserDetailsPage(selectedUser);
        }

        private void OpenNewUserDetailsPage(User selectedUser)
        {
            UserDetailsPageUserControl userDetailsPage = new UserDetailsPageUserControl(connection, selectedUser);
            userDetailsPage.OnBackButtonClicked += OnBackButtonClicked;
            Controls.Add(userDetailsPage);
        }
    }
}
