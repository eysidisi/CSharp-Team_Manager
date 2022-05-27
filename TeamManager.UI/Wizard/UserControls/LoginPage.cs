using TeamManager.Service.Wizard;
using TeamManager.Service.Wizard.DatabaseControllers;
using TeamManager.Service.Wizard.Models;

namespace TeamManager.UI.Wizard.UserControls
{
    public partial class LoginPage : UserControl
    {
        public Action<Manager> OnSuccessfulLogin;
        readonly LoginPageService loginPageService;
        public LoginPage(WizardDatabaseController databaseManager)
        {
            InitializeComponent();
            loginPageService = new LoginPageService(databaseManager);
        }

        private void buttonEnter_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = textBoxUserName.Text;
                string password = textBoxPassword.Text;
                Manager enteredManager = new Manager()
                {
                    UserName = userName,
                    Password = password
                };

                var managerFromDB = loginPageService.GetManager(enteredManager);

                OnSuccessfulLogin?.Invoke(managerFromDB);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
