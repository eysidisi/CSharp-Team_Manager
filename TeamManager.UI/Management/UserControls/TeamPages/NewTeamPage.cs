using TeamManager.Service.Management.DatabaseControllers;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.TeamServices;

namespace TeamManager.UI.Management.UserControls
{
    public partial class NewTeamPage : UserControl
    {
        readonly NewTeamPageService newTeamPageService;
        public Action<NewTeamPage> OnBackButtonClicked;

        public NewTeamPage(ManagerDatabaseController databaseManager)
        {
            InitializeComponent();
            newTeamPageService = new NewTeamPageService(databaseManager);
        }

        private void buttonSaveTeam_Click(object sender, EventArgs e)
        {
            try
            {
                TryToSaveTeam();
                ReturnPreviousPage();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TryToSaveTeam()
        {
            Team team = new Team()
            {
                Name = textBoxTeamName.Text,
            };

            newTeamPageService.SaveTeam(team);
            MessageBox.Show($"Team {team.Name} saved succesfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ReturnPreviousPage()
        {
            OnBackButtonClicked?.Invoke(this);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            ReturnPreviousPage();
        }
    }
}
