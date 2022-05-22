using TeamManager.Service.Management.DatabaseManagers;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.UserServices;

namespace TeamManager.UI.Management.UserControls.UserPages
{
    public partial class UserDetailsPage : UserControl
    {
        public Action<UserControl> OnBackButtonClicked;
        readonly UserDetailsPageService userDetailsPageService;
        new DataViewPage<Team> dataViewPage;

        public UserDetailsPage(DatabaseManager databaseManager, User user)
        {
            InitializeComponent();
            userDetailsPageService = new UserDetailsPageService(databaseManager, user);
            SetHeaderText(user.Name);
            CreateDataViewPage();
        }


        private void SetHeaderText(string text)
        {
            labelUserName.Text = text;
        }

        private void CreateDataViewPage()
        {
            dataViewPage = new DataViewPage<Team>(panelDataViewPage);
            SetUpDataViewPage();
        }

        private void SetUpDataViewPage()
        {
            dataViewPage.SetUpPage(userDetailsPageService.GetTeamsThatUserIn());
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            OnBackButtonClicked?.Invoke(this);
        }
    }
}
