using TeamManager.Service.Management.DatabaseController;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.UserServices;

namespace TeamManager.UI.Management.UserControls
{
    public partial class NewUserPage : UserControl
    {
        public Action<UserControl> OnCancelClick;
        readonly NewUserPageService newUserPageService;

        public NewUserPage(ManagerDatabaseController databaseController)
        {
            newUserPageService = new NewUserPageService(databaseController);
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
