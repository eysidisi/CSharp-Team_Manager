﻿using TeamManager.Service.Models;
using TeamManager.Service.WizardSection;
using TeamManager.Service.WizardSection.Database;

namespace TeamManager.UI.WizardSection.UserControls
{
    public partial class LoginPage : UserControl
    {
        public Action<Manager> OnSuccessfulLogin;

        LoginPageService loginPageService;
        public LoginPage(IWizardDatabaseConnection databaseConnection)
        {
            InitializeComponent();
            loginPageService = new LoginPageService(databaseConnection);
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = textBoxUserName.Text;
                string password = textBoxPassword.Text;
                Manager enteredManager = new Manager()
                {
                    UserName = userName,
                    Password = password
                };

                var managerFromDB = loginPageService.GetManager(enteredManager);

                OnSuccessfulLogin?.Invoke(managerFromDB);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
