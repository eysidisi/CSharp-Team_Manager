using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeamManager.Service.Database;
using TeamManager.Service.ManagerSection;
using TeamManager.Service.Models;

namespace TeamManager.UI.UserControls
{
    public partial class NewUserPageUserControl : UserControl
    {
        public Action OnCancelClick;
        NewUserPageService newUserPageService;

        public NewUserPageUserControl(IDatabaseConnection connection)
        {
            newUserPageService = new NewUserPageService(connection);
            InitializeComponent();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                User user = new User();
                user.Name = textBoxName.Text;
                newUserPageService.SaveNewUser(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            OnCancelClick?.Invoke();
        }
    }
}
