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
        //string connectionString = ConfigurationManager.ConnectionStrings["TestSmallDB"].ConnectionString;
        //string connectionString = ConfigurationManager.ConnectionStrings["TestMediumDB"].ConnectionString;
        //readonly string connectionString = ConfigurationManager.ConnectionStrings["SQLiteTestLargeDB"].ConnectionString;
        readonly string connectionString = ConfigurationManager.ConnectionStrings["MySQLTestLargeDB"].ConnectionString;
        readonly WizardDatabaseController databaseController;
        LoginPage loginPageUserControl;
        PurposePage purposePageUserControl;

        public WizardForm()
        {
            InitializeComponent();
            CenterToScreen();
            databaseController = new WizardMySQLDatabaseController(connectionString);
            AdjustLoginPage();
        }

        private void AdjustLoginPage()
        {
            loginPageUserControl = new LoginPage(databaseController);
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
            purposePageUserControl = new PurposePage(databaseController, manager);
            purposePageUserControl.OnSuccessfulPurposeEnter += OnSuccessfulPurposeEnter;
            panelCenter.Controls.Add(purposePageUserControl);
        }

        private void OnSuccessfulPurposeEnter()
        {
            panelCenter.Controls.Remove(purposePageUserControl);
            this.Hide();
            ManagerDatabaseController databaseController = new ManagerMySQLDatabaseController(connectionString);
            var managerForm = new ManagementForm(databaseController);
            managerForm.Closed += (s, args) => this.Close();
            managerForm.Show();
        }
    }
}