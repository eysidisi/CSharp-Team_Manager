using System.Configuration;
using TeamManager.Service.Models;
using TeamManager.Service.Wizard.Database;
using TeamManager.UI.Management;
using TeamManager.UI.Wizard.UserControls;

namespace TeamManager.UI.Wizard
{
    public partial class WizardForm : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["TestSmallDB"].ConnectionString;
        //string connectionString = ConfigurationManager.ConnectionStrings["TestMediumDB"].ConnectionString;
        //string connectionString = ConfigurationManager.ConnectionStrings["TestLargeDB"].ConnectionString;

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
            var managerForm = new ManagementForm(connectionString);
            managerForm.Closed += (s, args) => this.Close();
            managerForm.Show();
        }
    }
}