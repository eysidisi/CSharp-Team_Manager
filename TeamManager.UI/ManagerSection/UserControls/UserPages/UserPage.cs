using System.Data;
using TeamManager.Service.ManagerSection;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;
using TeamManager.UI.ManagerSection.UserControls.UserPages;

namespace TeamManager.UI.ManagerSection.UserControls
{
    public partial class UserPage : UserControl
    {
        UserPageService userPageService;
        DataTable usersDataTable;
        List<User> allUsers;
        IManagerDatabaseConnection connection;

        public UserPage(IManagerDatabaseConnection connection)
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

        private void OpenNewUserPage()
        {
            var saveNewUserPage = new NewUserPage(connection);
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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private User GetSelectedUser()
        {
            DataRowView selectedRow = dataGridViewUsers.SelectedRows[0].DataBoundItem as DataRowView;
            int selectedUserId = (int)selectedRow["ID"];
            return allUsers.Find(u => u.ID == selectedUserId);
        }

        private void buttonAddUser_Click(object sender, EventArgs e)
        {
            try
            {
                HideAllItems();
                OpenNewUserPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            try
            {
                User selectedUser = GetSelectedUser();
                HideAllItems();
                OpenNewUserDetailsPage(selectedUser);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void OpenNewUserDetailsPage(User selectedUser)
        {
            UserDetailsPage userDetailsPage = new UserDetailsPage(connection, selectedUser);
            userDetailsPage.OnBackButtonClicked += OnBackButtonClicked;
            Controls.Add(userDetailsPage);
        }
    }
}
