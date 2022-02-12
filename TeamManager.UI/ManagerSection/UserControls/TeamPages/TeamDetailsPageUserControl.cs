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
    public partial class TeamDetailsPageUserControl : UserControl
    {
        TeamDetailsPageService pageService;
        Team team;
        public Action OnBackButtonClicked;

        public TeamDetailsPageUserControl(IManagerDatabaseConnection connection,Team team)
        {
            InitializeComponent();
            pageService = new TeamDetailsPageService(connection);
            this.team = team;
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
            OnBackButtonClicked?.Invoke();
        }
    }
}
