using TeamManager.Service.Management.DatabaseManagers;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.TeamServices;

namespace TeamManager.UI.Management.UserControls
{
    public partial class TeamDetailsPage : UserControl
    {
        public Action<TeamDetailsPage> OnBackButtonClicked;
        DataViewPage<User> dataViewPage;
        TeamDetailsPageService teamDetailPageService;

        public TeamDetailsPage(DatabaseManager databaseManager, Team team)
        {
            InitializeComponent();
            InitializeVariables(databaseManager, team);
            CreateDataViewPage();
        }

        private void InitializeVariables(DatabaseManager databaseManager, Team team)
        {
            teamDetailPageService = new TeamDetailsPageService(databaseManager, team);
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
