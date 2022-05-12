using TeamManager.Service.Management;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Models;

namespace TeamManager.UI.Management.UserControls
{
    public partial class NewUserPage : UserControl
    {
        public Action<UserControl> OnCancelClick;
        NewUserPageService newUserPageService;

        public NewUserPage(IManagementDatabaseConnection connection)
        {
            newUserPageService = new NewUserPageService(connection);
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                TryToSaveNewUser();
                ReturnPreviousPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TryToSaveNewUser()
        {
            User user = GetNewUserInformation();
            newUserPageService.SaveNewUser(user);
            MessageBox.Show($"User {user.Name} saved succesfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private User GetNewUserInformation()
        {
            User user = new User();
            user.Name = textBoxName.Text;
            user.Surname = textBoxSurname.Text;
            user.PhoneNumber = textBoxPhoneNumber.Text;
            user.Title = textBoxTitle.Text;
            return user;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            ReturnPreviousPage();
        }

        private void ReturnPreviousPage()
        {
            OnCancelClick?.Invoke(this);
        }
    }
}
