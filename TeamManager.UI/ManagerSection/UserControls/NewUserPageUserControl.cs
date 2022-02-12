using TeamManager.Service.ManagerSection;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;

namespace TeamManager.UI.ManagerSection.UserControls
{
    public partial class NewUserPageUserControl : UserControl
    {
        public Action OnCancelClick;
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
                newUserPageService.SaveNewUser(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            OnCancelClick?.Invoke();
        }
    }
}
