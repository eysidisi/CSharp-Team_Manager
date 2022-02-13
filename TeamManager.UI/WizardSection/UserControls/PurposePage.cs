using TeamManager.Service.Models;
using TeamManager.Service.WizardSection;
using TeamManager.Service.WizardSection.Database;

namespace TeamManager.UI.WizardSection.UserControls
{
    public partial class PurposePage : UserControl
    {
        public Action OnSuccessfulPurposeEnter;

        string managerUserName;
        PurposePageService purposePageService;

        public PurposePage(IWizardDatabaseConnection connection, string managerUserName)
        {
            InitializeComponent();
            purposePageService = new PurposePageService(connection);
            this.managerUserName = managerUserName;
        }

        private void buttonSavePurpose_Click(object sender, EventArgs e)
        {
            string purposeText = textBoxPurpose.Text;

            try
            {
                Purpose purpose = new Purpose(managerUserName, purposeText);
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
