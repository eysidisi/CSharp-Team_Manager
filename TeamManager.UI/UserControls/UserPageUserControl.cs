using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeamManager.Service.Database;
using TeamManager.Service.ManagerSection;
using TeamManager.Service.Models;

namespace TeamManager.UI.UserControls
{
    public partial class UserPageUserControl : UserControl
    {
        UserPageService userPageService;
        DataTable usersDataTable;
        List<User> allUsers;
        IDatabaseConnection connection;
        NewUserPageUserControl saveNewUserPage;

        public UserPageUserControl(IDatabaseConnection connection)
        {
            InitializeComponent();
            userPageService = new UserPageService(connection);
            this.connection = connection;
            FillTable();
        }


        private void FillTable()
        {
            allUsers = userPageService.GetUsers();
            usersDataTable = userPageService.ConvertToDatatable(allUsers);
            dataGridViewUsers.DataSource = usersDataTable;
            dataGridViewUsers.AutoResizeColumns();
        }

        private void buttonDeleteUser_Click(object sender, EventArgs e)
        {
            User userToDelete = GetUserToDelete();
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

        private void OpenNewUserPage()
        {
            saveNewUserPage = new NewUserPageUserControl(connection);
            saveNewUserPage.OnCancelClick += AddNewUserCancelClicked;
            Controls.Add(saveNewUserPage);
        }

        private void HideAllItems()
        {
            foreach (Control control in Controls)
            {
                control.Hide();
            }
        }
        private void AddNewUserCancelClicked()
        {
            saveNewUserPage.Dispose();
            FillTable();
            ExposeAllItems();
        }

        private void ExposeAllItems()
        {
            foreach (Control control in Controls)
            {
                control.Visible = true;
            }
        }
    }
}
