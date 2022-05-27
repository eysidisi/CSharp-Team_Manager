using System.Configuration;
using TeamManager.Service.Management.DatabaseManagers;
using TeamManager.Service.Wizard.Database;
using TeamManager.Service.Wizard.DatabaseManagers;
using TeamManager.Service.Wizard.Models;
using TeamManager.UI.Management;
using TeamManager.UI.Wizard.UserControls;

namespace TeamManager.UI.Wizard
{
    public partial class WizardForm : Form
    {
        //string connectionString = ConfigurationManager.ConnectionStrings["TestSmallDB"].ConnectionString;
        //string connectionString = ConfigurationManager.ConnectionStrings["TestMediumDB"].ConnectionString;
        //readonly string connectionString = ConfigurationManager.ConnectionStrings["SQLiteTestLargeDB"].ConnectionString;
        readonly string connectionString = ConfigurationManager.ConnectionStrings["MySQLTestLargeDB"].ConnectionString;
        readonly WizardDatabaseController databaseManager;
        LoginPage loginPageUserControl;
        PurposePage purposePageUserControl;

        public WizardForm()
        {
            InitializeComponent();
            CenterToScreen();
            databaseManager = new WizardMySQLDatabaseController(connectionString);
            AdjustLoginPage();
        }

        private void AdjustLoginPage()
        {
            loginPageUserControl = new LoginPage(databaseManager);
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
            purposePageUserControl = new PurposePage(databaseManager, manager);
            purposePageUserControl.OnSuccessfulPurposeEnter += OnSuccessfulPurposeEnter;
            panelCenter.Controls.Add(purposePageUserControl);
        }

        private void OnSuccessfulPurposeEnter()
        {
            panelCenter.Controls.Remove(purposePageUserControl);
            this.Hide();
            ManagerDatabaseController databaseManager = new ManagerMySQLDatabaseController(connectionString);
            var managerForm = new ManagementForm(databaseManager);
            managerForm.Closed += (s, args) => this.Close();
            managerForm.Show();
        }
    }
}