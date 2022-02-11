using TeamManager.Service.Wizard.Database;
using TeamManager.UI.UserControls;

namespace TeamManager.UI
{
    public partial class WizardForm : Form
    {
        string connectionString = $@"Data Source = {Directory.GetCurrentDirectory()}\TestDBFiles\TestDB.db; Version = 3";

        IWizardDatabaseConnection connection;
        LoginPageUserControl loginPageUserControl;
        PurposePageUserControl purposePageUserControl;

        public WizardForm()
        {
            InitializeComponent();
            connection = new WizardSQLiteConnection(connectionString);
            AdjustLoginPage();
        }

        private void AdjustLoginPage()
        {
            loginPageUserControl = new LoginPageUserControl(connection);
            Controls.Add(loginPageUserControl);
            loginPageUserControl.OnSuccessfulLogin += OnSuccessfulLogin;
        }

        private void OnSuccessfulLogin(string managerUserName)
        {
            Controls.Remove(loginPageUserControl);
            AdjustPurposePage(managerUserName);
        }

        private void AdjustPurposePage(string managerUserName)
        {
            purposePageUserControl = new PurposePageUserControl(connection, managerUserName);
            purposePageUserControl.OnSuccessfulPurposeEnter += OnSuccessfulPurposeEnter;
            Controls.Add(purposePageUserControl);
        }

        private void OnSuccessfulPurposeEnter()
        {
            Controls.Remove(purposePageUserControl);
        }
    }
}