using TeamManager.Service.ManagerSection.Database;
using TeamManager.UI.ManagerSection.UserControls;

namespace TeamManager.UI.ManagerSection
{
    public partial class ManagerForm : Form
    {
        string connectionString = $@"Data Source = {Directory.GetCurrentDirectory()}\TestDBFiles\Small.db; Version = 3";

        public ManagerForm()
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
