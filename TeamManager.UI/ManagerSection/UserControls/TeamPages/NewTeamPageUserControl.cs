using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeamManager.Service.ManagerSection;
using TeamManager.Service.ManagerSection.Database;
using TeamManager.Service.Models;

namespace TeamManager.UI.ManagerSection.UserControls
{
    public partial class NewTeamPageUserControl : UserControl
    {
        NewTeamPageService newTeamPageService;
        public Action<NewTeamPageUserControl> OnBackButtonClicked;

        public NewTeamPageUserControl(IManagerDatabaseConnection connection)
        {
            InitializeComponent();
            newTeamPageService = new NewTeamPageService(connection);
        }

        private void buttonSaveTeam_Click(object sender, EventArgs e)
        {
            try
            {
                Team team = new Team()
                {
                    Name = textBoxTeamName.Text,
                };

                newTeamPageService.SaveTeam(team);
                MessageBox.Show($"Team {team.Name} saved succesfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            OnBackButtonClicked?.Invoke(this);
        }
    }
}
