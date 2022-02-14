using TeamManager.Service.Models;
using TeamManager.Service.WizardSection.Database;
using TeamManager.UI.ManagerSection;
using TeamManager.UI.WizardSection.UserControls;

namespace TeamManager.UI.WizardSection
{
    public partial class WizardForm : Form
    {
        string connectionString = $@"Data Source = {Directory.GetCurrentDirectory()}\TestDBFiles\TestDB.db; Version = 3";

        IWizardDatabaseConnection connection;
        LoginPage loginPageUserControl;
        PurposePage purposePageUserControl;

        public WizardForm()
        {
            InitializeComponent();
            this.CenterToScreen();
            connection = new WizardSQLiteConnection(connectionString);
            AdjustLoginPage();
        }

        private void AdjustLoginPage()
        {
            loginPageUserControl = new LoginPage(connection);
            panelCenter.Controls.Add(loginPageUserControl);
            loginPageUserControl.OnSuccessfulLogin += OnSuccessfulLogin;
        }

        private void OnSuccessfulLogin(Manager manager)
        {
            panelCenter.Controls.Remove(loginPageUserControl);
            AdjustPurposePage(manager);
        }

        private void AdjustPurposePage(Manager manager)
        {
            purposePageUserControl = new PurposePage(connection, manager);
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