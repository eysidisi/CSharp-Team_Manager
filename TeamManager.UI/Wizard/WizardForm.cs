using TeamManager.UI.UserControls;
using TeamManager.Service.Wizard;
using TeamManager.Service.Database;
using TeamManager.Service;
using TeamManager.Service.Models;

namespace TeamManager.UI
{
    public partial class WizardForm : Form
    {
        string connectionString = $@"Data Source = {Directory.GetCurrentDirectory()}\TestDBFiles\TestDB.db; Version = 3";

        IDatabaseConnection connection;
        LoginPageUserControl loginPageUserControl;
        PurposePageUserControl purposePageUserControl;

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
            loginPageUserControl.OnSuccessfulLogin += OnSuccessfulLogin;
        }

        private void OnSuccessfulLogin(Manager manager)
        {
            Controls.Remove(loginPageUserControl);
            AdjustPurposePage(manager);
        }

        private void AdjustPurposePage(Manager user)
        {
            purposePageUserControl = new PurposePageUserControl(connection, user);
            purposePageUserControl.OnSuccessfulPurposeEnter += OnSuccessfulPurposeEnter;
            Controls.Add(purposePageUserControl);
        }

        private void OnSuccessfulPurposeEnter()
        {
            Controls.Remove(purposePageUserControl);
        }
    }
}