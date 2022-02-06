using TeamManager.UI.UserControls;
using TeamManager.Service.Wizard;
using TeamManager.Service.Wizard.Database;
using TeamManager.Service;

namespace TeamManager.UI
{
    public partial class WizardForm : Form
    {
        string connectionString = $@"Data Source = {Directory.GetCurrentDirectory()}\TestDB.db; Version = 3";

        IDatabaseConnection connection;
        LoginPageUserControl loginPageUserControl;

        public WizardForm()
        {
            InitializeComponent();
            connection = new SQLiteDataAccess(connectionString);
            AdjustLoginPage();
        }

        private void AdjustLoginPage()
        {
            loginPageUserControl = new LoginPageUserControl(connection);
            Controls.Add(loginPageUserControl);
            loginPageUserControl.OnSuccessfulLogin += SuccessfulLogin;
        }

        private void SuccessfulLogin(User user)
        {
            Controls.Remove(loginPageUserControl);
        }
    }
}