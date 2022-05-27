using System.Data;
using TeamManager.Service.Management.DatabaseManagers;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.UserServices;
using TeamManager.UI.Management.UserControls.UserPages;

namespace TeamManager.UI.Management.UserControls
{
    public partial class UserPage : UserControl
    {
        readonly UserPageService userPageService;
        readonly ManagerDatabaseController databaseManager;
        DataViewPage<User> dataViewPage;

        public UserPage(ManagerDatabaseController databaseManager)
        {
            InitializeComponent();
            this.databaseManager = databaseManager;
            userPageService = new UserPageService(databaseManager);
            CreateDataViewPage();
        }

        private void CreateDataViewPage()
        {
            dataViewPage = new DataViewPage<User>(panelDataViewPage);
            SetUpDataViewPage();
        }

        private void SetUpDataViewPage()
        {
            dataViewPage.SetUpPage(userPageService.GetUsers());
        }

        private void OpenNewUserPage()
        {
            var saveNewUserPage = new NewUserPage(databaseManager);
            saveNewUserPage.OnCancelClick += OnBackButtonClicked;
            Controls.Add(saveNewUserPage);
        }

        private void OnBackButtonClicked(UserControl userControl)
        {
            userControl.Dispose();
            SetUpDataViewPage();
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
                TryToDeleteSelectedUser();
                SetUpDataViewPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TryToDeleteSelectedUser()
        {
            User userToDelete = TryToGetSelectedUser();
            DialogResult d = MessageBox.Show($"Do you want to delete user '{userToDelete.Name}'?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (d == DialogResult.Yes)
            {
                userPageService.DeleteUser(userToDelete);
                dataViewPage.DeleteSelectedRow();
            }
        }

        private User TryToGetSelectedUser()
        {
            DataRowView selectedRow = dataViewPage.GetSelectedRow();
            User selectedItem = GetSelectedUserUsingRow(selectedRow);

            return selectedItem;
        }

        private User GetSelectedUserUsingRow(DataRowView selectedRow)
        {
            int selectedItemID = (int)selectedRow["ID"];
            User userWithID = TryToFindUserUsingID(selectedItemID);

            return userWithID;
        }

        private User TryToFindUserUsingID(int selectedItemID)
        {
            var allUsers = userPageService.GetUsers();
            var selectedItem = allUsers.Find(u => u.ID == selectedItemID);

            if (selectedItem == null)
            {
                throw new Exception("Can't find the selected item! Please refresh the page!");
            }

            return selectedItem;
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
                User selectedUser = TryToGetSelectedUser();
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
            UserDetailsPage userDetailsPage = new UserDetailsPage(databaseManager, selectedUser);
            userDetailsPage.OnBackButtonClicked += OnBackButtonClicked;
            Controls.Add(userDetailsPage);
        }
    }
}
