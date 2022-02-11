using TeamManager.Service.Models;
using TeamManager.Service.Wizard;
using TeamManager.Service.Wizard.Database;

namespace TeamManager.UI.UserControls
{
    public partial class PurposePageUserControl : UserControl
    {
        public Action OnSuccessfulPurposeEnter;

        string managerUserName;
        PurposePageService purposePageService;

        public PurposePageUserControl(IWizardDatabaseConnection connection, string managerUserName)
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
                if (purposePageService.CheckIfPurposeIsValid(purposeText))
                {
                    Purpose purpose = new Purpose(managerUserName, purposeText);
                    purposePageService.SavePurposeOfVisit(purpose);
                    MessageBox.Show("Purpose is saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    OnSuccessfulPurposeEnter?.Invoke();
                }
                else
                {
                    MessageBox.Show("Purpose is not valid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
