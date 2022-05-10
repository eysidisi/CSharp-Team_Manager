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
        TeamDetailsPageService pageService;
        Team team;
        public Action<TeamDetailsPage> OnBackButtonClicked;

        public TeamDetailsPage(IManagementDatabaseConnection connection,Team team)
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
            ResizeColumns(dataGridViewUsers);
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
