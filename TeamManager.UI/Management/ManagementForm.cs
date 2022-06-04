using TeamManager.Service.Management.DatabaseController;
using TeamManager.UI.Management.UserControls;

namespace TeamManager.UI.Management
{
    public partial class ManagementForm : Form
    {
        readonly ManagerDatabaseController databaseController;
        public ManagementForm(ManagerDatabaseController databaseController)
        {
            InitializeComponent();
            CenterToScreen();
            this.databaseController = databaseController;
            CreateUserPage();
            CreateTeamPage();
        }

        private void CreateTeamPage()
        {
            TeamPage teamPage = new TeamPage(databaseController);
            panelTeamPage.Controls.Add(teamPage);
        }

        private void CreateUserPage()
        {
            UserPage userPageUser = new UserPage(databaseController);
            panelUserDetails.Controls.Add(userPageUser);
        }
    }
}
