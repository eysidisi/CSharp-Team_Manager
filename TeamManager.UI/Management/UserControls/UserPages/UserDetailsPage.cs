using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeamManager.Service.Management;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Management.UserServices;
using TeamManager.Service.Models;

namespace TeamManager.UI.Management.UserControls.UserPages
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
            labelUserName.Text = user.Name;
            FillUserDatatable();
        }

        private void FillUserDatatable()
        {
            var allTeams = userDetailsPageService.GetTeamsThatUserIn(user);
            var usersDataTable = HelperFunctions.ConvertToDatatable(allTeams);
            dataGridViewTeams.DataSource = usersDataTable;
            ResizeColumns(dataGridViewTeams);
        }
        private void ResizeColumns(DataGridView dataGrid)
        {
            int width = dataGrid.Width;
            int minColWidth = (int)Math.Ceiling(width / (double)dataGrid.Columns.Count);
            for (int i = 0; i < dataGrid.Columns.Count; i++)
            {
                dataGrid.Columns[i].MinimumWidth = minColWidth;
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            OnBackButtonClicked?.Invoke(this);
        }
    }
}
