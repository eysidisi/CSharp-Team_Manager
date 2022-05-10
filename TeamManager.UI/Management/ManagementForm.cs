using TeamManager.Service.Management.Database;
using TeamManager.UI.Management.UserControls;

namespace TeamManager.UI.Management
{
    public partial class ManagementForm : Form
    {
        public ManagementForm(string connectionString)
        {
            InitializeComponent();
            this.CenterToScreen();
            var connection = new ManagementSQLiteConnetion(connectionString);
            CreateUserPage(connection);
            CreateTeamPage(connection);
        }

        private void CreateTeamPage(IManagementDatabaseConnection connection)
        {
            TeamPage teamPage = new TeamPage(connection);
            panelTeamPage.Controls.Add(teamPage);
        }

        private void CreateUserPage(IManagementDatabaseConnection connection)
        {
            UserPage userPageUser = new UserPage(connection);
            panelUserDetails.Controls.Add(userPageUser);
        }
    }
}
