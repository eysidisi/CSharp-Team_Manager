using TeamManager.Service.Management.DatabaseController;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.TeamServices;

namespace TeamManager.UI.Management.UserControls
{
    public partial class TeamDetailsPage : UserControl
    {
        public Action<TeamDetailsPage> OnBackButtonClicked;
        DataViewPage<User> dataViewPage;
        TeamDetailsPageService teamDetailPageService;

        public TeamDetailsPage(ManagerDatabaseController databaseController, Team team)
        {
            InitializeComponent();
            InitializeVariables(databaseController, team);
            CreateDataViewPage();
        }

        private void InitializeVariables(ManagerDatabaseController databaseController, Team team)
        {
            teamDetailPageService = new TeamDetailsPageService(databaseController, team);
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
