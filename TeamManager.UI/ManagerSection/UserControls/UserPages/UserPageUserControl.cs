using System.Data;
using TeamManager.Service.ManagerSection;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;

namespace TeamManager.UI.ManagerSection.UserControls
{
    public partial class UserPageUserControl : UserControl
    {
        UserPageService userPageService;
        DataTable usersDataTable;
        List<User> allUsers;
        IManagerDatabaseConnection connection;
        NewUserPageUserControl saveNewUserPage;

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
            saveNewUserPage = new NewUserPageUserControl(connection);
            saveNewUserPage.OnCancelClick += OnAddNewUserCancelClicked;
            Controls.Add(saveNewUserPage);
        }

        private void OnAddNewUserCancelClicked()
        {
            saveNewUserPage.Dispose();
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
            User userToDelete = GetUserToDelete();

            DialogResult d = MessageBox.Show($"Do you want to delete user '{userToDelete.Name}'?", "Delete", MessageBoxButtons.YesNo,MessageBoxIcon.Warning);
            if (d == DialogResult.No)
            {
                return;
            }

            userPageService.DeleteUser(userToDelete);
            (dataGridViewUsers.SelectedRows[0].DataBoundItem as DataRowView).Delete();
        }

        private User GetUserToDelete()
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
    }
}
