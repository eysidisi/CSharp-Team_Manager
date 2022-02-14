using TeamManager.Service.ManagerSection.Database;
using TeamManager.UI.ManagerSection.UserControls;

namespace TeamManager.UI.ManagerSection
{
    public partial class ManagerForm : Form
    {
        public ManagerForm(string connectionString)
        {
            InitializeComponent();
            this.CenterToScreen();
            var connection = new ManagerSQLiteConnetion(connectionString);
            CreateUserPage(connection);
            CreateTeamPage(connection);
        }

        private void CreateTeamPage(IManagerDatabaseConnection connection)
        {
            TeamPage teamPage = new TeamPage(connection);
            panelTeamPage.Controls.Add(teamPage);
        }

        private void CreateUserPage(IManagerDatabaseConnection connection)
        {
            UserPage userPageUser = new UserPage(connection);
            panelUserDetails.Controls.Add(userPageUser);
        }
    }
}
