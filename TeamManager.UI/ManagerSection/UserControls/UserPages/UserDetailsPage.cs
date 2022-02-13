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
using TeamManager.Service.ManagerSection.UserServices;
using TeamManager.Service.Models;

namespace TeamManager.UI.ManagerSection.UserControls.UserPages
{
    public partial class UserDetailsPage : UserControl
    {
        UserDetailsPageService userDetailsPageService;
        User user;
        public Action<UserControl> OnBackButtonClicked;
        public UserDetailsPage(IManagerDatabaseConnection connection, User user)
        {
            InitializeComponent();
            userDetailsPageService = new UserDetailsPageService(connection);
            this.user = user;
            FillUserDatatable();
        }

        private void FillUserDatatable()
        {
            var allTeams = userDetailsPageService.GetTeamsThatUserIn(user);
            var usersDataTable = HelperFunctions.ConvertToDatatable(allTeams);
            dataGridViewTeams.DataSource = usersDataTable;
            dataGridViewTeams.AutoResizeColumns();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            OnBackButtonClicked?.Invoke(this);
        }
    }
}
