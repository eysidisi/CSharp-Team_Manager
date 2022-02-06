﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeamManager.Service.Wizard;
using TeamManager.Service.Wizard.Database;
using TeamManager.Service.Wizard.LoginPage;

namespace TeamManager.UI.UserControls
{
    public partial class LoginPageUserControl : UserControl
    {
        public Action<User> OnSuccessfulLogin;

        LoginPageService loginPageService;
        public LoginPageUserControl(IDatabaseConnection databaseConnection)
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

                if (loginPageService.CheckIfUserExists(userName, password))
                {
                    User user = loginPageService.GetUser(userName);

                    OnSuccessfulLogin?.Invoke(user);
                }

                else
                {
                    MessageBox.Show("Can't find the user!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
