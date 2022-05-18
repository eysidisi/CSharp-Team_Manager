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
using TeamManager.Service.Models;

namespace TeamManager.UI.Management.UserControls
{
    public partial class TeamDetailsPage : UserControl
    {
        public Action<TeamDetailsPage> OnBackButtonClicked;
        DataViewPage<User> dataViewPage;
        TeamDetailsPageService teamDetailPageService;

        public TeamDetailsPage(IManagementDatabaseConnection connection, Team team)
        {
            InitializeComponent();
            InitializeVariables(connection, team);
            CreateDataViewPage();
        }

        private void InitializeVariables(IManagementDatabaseConnection connection, Team team)
        {
            teamDetailPageService = new TeamDetailsPageService(connection, team);
            labelTeamName.Text = team.Name;
        }
        private void CreateDataViewPage()
        {
            dataViewPage = new DataViewPage<User>(panelUserssDataGridView);
            dataViewPage.SetUpPage(teamDetailPageService.GetUsersInTeam());
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            OnBackButtonClicked?.Invoke(this);
        }
    }
}
