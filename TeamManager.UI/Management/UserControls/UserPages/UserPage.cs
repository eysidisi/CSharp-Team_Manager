using System.Data;
using TeamManager.Service.Management;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Models;
using TeamManager.UI.Management.UserControls.UserPages;

namespace TeamManager.UI.Management.UserControls
{
    public partial class UserPage : UserControl
    {
        const int NumOfUsersPerPage = 10;
        UserPageService userPageService;
        IManagementDatabaseConnection connection;

        public UserPage(IManagementDatabaseConnection connection)
        {
            InitializeComponent();
            InitializeVariables(connection);
            AdjustPaginationComponent();
            DisplayUsersInPage(1);
        }

        private void InitializeVariables(IManagementDatabaseConnection connection)
        {
            userPageService = new UserPageService(connection);
            this.connection = connection;
        }

        private void AdjustPaginationComponent()
        {
            int maxNumOfPages = (int)Math.Ceiling(userPageService.GetUsers().Count / ((double)NumOfUsersPerPage));
            maxNumOfPages = Math.Max(maxNumOfPages, 1);
            paginationComponent.SetMaxPageNum(maxNumOfPages);
            paginationComponent.OnCurrentPageNumChanged += PageNumChanged;
        }

        private void PageNumChanged(int pageNum)
        {
            DisplayUsersInPage(pageNum);
        }

        private void DisplayUsersInPage(int pageNum)
        {
            int startingIndexInList = ((pageNum - 1) * NumOfUsersPerPage);
            Range range = new Range(startingIndexInList, startingIndexInList + NumOfUsersPerPage);
            var usersToDisplay = userPageService.GetUsers().Take(range).ToList();
            DisplayUsersInDataGridView(usersToDisplay);
        }

        private void DisplayUsersInDataGridView(List<User> usersToShowInPage)
        {
            var usersDataTable = HelperFunctions.ConvertToDatatable(usersToShowInPage);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TryToDeleteSelectedUser()
        {
            User userToDelete = GetSelectedUser(dataGridViewUsers);
            DialogResult d = MessageBox.Show($"Do you want to delete user '{userToDelete.Name}'?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (d == DialogResult.Yes)
            {
                userPageService.DeleteUser(userToDelete);
                (dataGridViewUsers.SelectedRows[0].DataBoundItem as DataRowView).Delete();
            }
        }

        private User GetSelectedUser(DataGridView dataGridView)
        {
            if (dataGridView.SelectedRows.Count < 1)
            {
                throw new Exception("No item is selected! Please select an item first!");
            }

            DataRowView selectedRow = dataGridView.SelectedRows[0].DataBoundItem as DataRowView;
            int selectedItemID = (int)selectedRow["ID"];
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
                User selectedUser = GetSelectedUser(dataGridViewUsers);
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
