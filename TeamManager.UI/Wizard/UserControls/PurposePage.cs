using TeamManager.Service.Management.Models;
using TeamManager.Service.Wizard;
using TeamManager.Service.Wizard.DatabaseController;
using TeamManager.Service.Wizard.Models;

namespace TeamManager.UI.Wizard.UserControls
{
    public partial class PurposePage : UserControl
    {
        public Action OnSuccessfulPurposeEnter;
        readonly Manager manager;
        readonly PurposePageService purposePageService;

        public PurposePage(WizardDatabaseController databaseController, Manager manager)
        {
            InitializeComponent();
            purposePageService = new PurposePageService(databaseController);
            this.manager = manager;
        }

        private void buttonSavePurpose_Click(object sender, EventArgs e)
        {
            string purposeText = textBoxPurpose.Text;

            try
            {
                Purpose purpose = new Purpose(manager.UserName, purposeText);
                purposePageService.SavePurposeOfVisit(purpose);
                MessageBox.Show("Purpose is saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                OnSuccessfulPurposeEnter?.Invoke();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
