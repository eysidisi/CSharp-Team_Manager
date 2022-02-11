using TeamManager.Service.Models;
using TeamManager.Service.Wizard;
using TeamManager.Service.Wizard.Database;

namespace TeamManager.UI.UserControls
{
    public partial class LoginPageUserControl : UserControl
    {
        public Action<string> OnSuccessfulLogin;

        LoginPageService loginPageService;
        public LoginPageUserControl(IWizardDatabaseConnection databaseConnection)
        {
            InitializeComponent();
            loginPageService = new LoginPageService(databaseConnection);
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = textBoxUserName.Text;
                string password = textBoxPassword.Text;
                Manager manager = new Manager()
                {
                    UserName = userName,
                    Password = password
                };

                if (loginPageService.CheckIfManagerExists(manager))
                {
                    OnSuccessfulLogin?.Invoke(userName);
                }

                else
                {
                    MessageBox.Show("Can't find the manager!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
