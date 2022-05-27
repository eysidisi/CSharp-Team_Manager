using TeamManager.Service.Management.DatabaseControllers;
using TeamManager.UI.Management.UserControls;

namespace TeamManager.UI.Management
{
    public partial class ManagementForm : Form
    {
        readonly ManagerDatabaseController databaseManager;
        public ManagementForm(ManagerDatabaseController databaseManager)
        {
            InitializeComponent();
            CenterToScreen();
            this.databaseManager = databaseManager;
            CreateUserPage();
            CreateTeamPage();
        }

        private void CreateTeamPage()
        {
            TeamPage teamPage = new TeamPage(databaseManager);
            panelTeamPage.Controls.Add(teamPage);
        }

        private void CreateUserPage()
        {
            UserPage userPageUser = new UserPage(databaseManager);
            panelUserDetails.Controls.Add(userPageUser);
        }
    }
}
