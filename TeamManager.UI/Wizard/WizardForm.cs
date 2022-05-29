using System.Configuration;
using TeamManager.Service.Management.DatabaseControllers;
using TeamManager.Service.Wizard.DatabaseControllers;
using TeamManager.Service.Wizard.Models;
using TeamManager.UI.Management;
using TeamManager.UI.Wizard.UserControls;

namespace TeamManager.UI.Wizard
{
    public partial class WizardForm : Form
    {
        readonly string connectionString = ConfigurationManager.ConnectionStrings["SQLiteTestLargeDB"].ConnectionString;
        //readonly string connectionString = ConfigurationManager.ConnectionStrings["MySQLTestLargeDB"].ConnectionString;

        readonly WizardDatabaseController wizardDatabaseController;
        readonly ManagerDatabaseController managerDatabaseController;
        LoginPage loginPageUserControl;
        PurposePage purposePageUserControl;

        public WizardForm()
        {
            InitializeComponent();
            CenterToScreen();
            wizardDatabaseController = new WizardSQLiteDatabaseController(connectionString);
            managerDatabaseController = new ManagerSQLiteDatabaseController(connectionString);
            AdjustLoginPage();
        }

        private void AdjustLoginPage()
        {
            loginPageUserControl = new LoginPage(wizardDatabaseController);
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
            purposePageUserControl = new PurposePage(wizardDatabaseController, manager);
            purposePageUserControl.OnSuccessfulPurposeEnter += OnSuccessfulPurposeEnter;
            panelCenter.Controls.Add(purposePageUserControl);
        }

        private void OnSuccessfulPurposeEnter()
        {
            panelCenter.Controls.Remove(purposePageUserControl);
            this.Hide();
            var managerForm = new ManagementForm(managerDatabaseController);
            managerForm.Closed += (s, args) => this.Close();
            managerForm.Show();
        }
    }
}