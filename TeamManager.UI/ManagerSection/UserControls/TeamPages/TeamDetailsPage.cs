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
    public partial class TeamDetailsPage : UserControl
    {
        TeamDetailsPageService pageService;
        Team team;
        public Action<TeamDetailsPage> OnBackButtonClicked;

        public TeamDetailsPage(IManagerDatabaseConnection connection,Team team)
        {
            InitializeComponent();
            pageService = new TeamDetailsPageService(connection);
            this.team = team;
            labelTeamName.Text= team.Name;
            FillDataGrid();
        }

        private void FillDataGrid()
        {
            var users = pageService.GetUsersInTeam(team);
            var usersDataTable = HelperFunctions.ConvertToDatatable(users);
            dataGridViewUsers.DataSource = usersDataTable;
            dataGridViewUsers.AutoResizeColumns();
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            OnBackButtonClicked?.Invoke(this);
        }
    }
}
