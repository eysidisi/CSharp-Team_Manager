using TeamManager.Service.ManagerSection;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;

namespace TeamManager.UI.ManagerSection.UserControls
{
    public partial class NewUserPageUserControl : UserControl
    {
        public Action<UserControl> OnCancelClick;
        NewUserPageService newUserPageService;

        public NewUserPageUserControl(IManagerDatabaseConnection connection)
        {
            newUserPageService = new NewUserPageService(connection);
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User();
                user.Name = textBoxName.Text;
                user.Surname = textBoxSurname.Text;
                user.PhoneNumber = textBoxPhoneNumber.Text;
                user.Title = textBoxTitle.Text;
                newUserPageService.SaveNewUser(user);
                MessageBox.Show($"User {user.Name} saved succesfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            OnCancelClick?.Invoke(this);
        }
    }
}
