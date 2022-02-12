using TeamManager.Service.WizardSection.Database;
using TeamManager.UI.ManagerSection;
using TeamManager.UI.WizardSection.UserControls;

namespace TeamManager.UI.WizardSection
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
            this.CenterToScreen();
            connection = new WizardSQLiteConnection(connectionString);
            AdjustLoginPage();
        }

        private void AdjustLoginPage()
        {
            loginPageUserControl = new LoginPageUserControl(connection);
            panelCenter.Controls.Add(loginPageUserControl);
            loginPageUserControl.OnSuccessfulLogin += OnSuccessfulLogin;
        }

        private void OnSuccessfulLogin(string managerUserName)
        {
            panelCenter.Controls.Remove(loginPageUserControl);
            AdjustPurposePage(managerUserName);
        }

        private void AdjustPurposePage(string managerUserName)
        {
            purposePageUserControl = new PurposePageUserControl(connection, managerUserName);
            purposePageUserControl.OnSuccessfulPurposeEnter += OnSuccessfulPurposeEnter;
            panelCenter.Controls.Add(purposePageUserControl);
        }

        private void OnSuccessfulPurposeEnter()
        {
            panelCenter.Controls.Remove(purposePageUserControl);
            this.Hide();
            var managerForm = new ManagerForm();
            managerForm.Closed += (s, args) => this.Close();
            managerForm.Show();
        }
    }
}