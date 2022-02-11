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
using TeamManager.Service.Database;
using TeamManager.Service.Wizard.PurposePage;
using TeamManager.Service.Models;

namespace TeamManager.UI.UserControls
{
    public partial class PurposePageUserControl : UserControl
    {
        public Action OnSuccessfulPurposeEnter;

        Manager manager;
        PurposePageService purposePageService;

        public PurposePageUserControl(IDatabaseConnection connection, Manager manager)
        {
            InitializeComponent();
            purposePageService = new PurposePageService(connection);
            this.manager = manager;
        }

        private void buttonSavePurpose_Click(object sender, EventArgs e)
        {
            string purposeText = textBoxPurpose.Text;

            try
            {
                if (purposePageService.CheckIfPurposeIsValid(purposeText))
                {
                    Purpose purpose = new Purpose(manager.UserName, purposeText);
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
