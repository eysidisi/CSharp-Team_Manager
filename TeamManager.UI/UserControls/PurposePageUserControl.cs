using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeamManager.Service.Wizard;
using TeamManager.Service.Wizard.PurposePage;

namespace TeamManager.UI.UserControls
{
    public partial class PurposePageUserControl : UserControl
    {
        public Action OnSuccessfulPurposeEnter;

        User user;
        PurposePageService purposePageService;

        public PurposePageUserControl(IDatabaseConnection connection, User user)
        {
            InitializeComponent();
            purposePageService = new PurposePageService(connection);
            this.user = user;
        }

        private void buttonSavePurpose_Click(object sender, EventArgs e)
        {
            string purpose = textBoxPurpose.Text;

            try
            {
                if (purposePageService.CheckIfPurposeIsValid(purpose))
                {
                    purposePageService.SavePurposeOfVisit(purpose, user);
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
