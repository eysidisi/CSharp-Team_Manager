using TeamManager.Service.ManagerSection.Database;
using TeamManager.UI.UserControls;

namespace TeamManager.UI
{
    public partial class ManagerForm : Form
    {
        string connectionString = $@"Data Source = {Directory.GetCurrentDirectory()}\TestDBFiles\TestDB.db; Version = 3";

        public ManagerForm()
        {
            InitializeComponent();
            var connection = new ManagerSQLiteConnetion(connectionString);
            CreateUserPage(connection);
            CreateTeamPage(connection);
        }

        private void CreateTeamPage(IManagerDatabaseConnection connection)
        {
            TeamPageUserControl teamPage = new TeamPageUserControl(connection);
            panelTeamPage.Controls.Add(teamPage);
        }

        private void CreateUserPage(IManagerDatabaseConnection connection)
        {
            UserPageUserControl userPageUser = new UserPageUserControl(connection);
            panelUserDetails.Controls.Add(userPageUser);
        }
    }
}
