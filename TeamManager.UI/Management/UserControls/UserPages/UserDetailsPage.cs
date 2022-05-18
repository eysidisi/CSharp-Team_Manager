using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TeamManager.Service.Management;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Management.UserServices;
using TeamManager.Service.Models;

namespace TeamManager.UI.Management.UserControls.UserPages
{
    public partial class UserDetailsPage : UserControl
    {
        public Action<UserControl> OnBackButtonClicked;
        UserDetailsPageService userDetailsPageService;
        new DataViewPage<Team> dataViewPage;

        public UserDetailsPage(IManagementDatabaseConnection connection, User user)
        {
            InitializeComponent();
            userDetailsPageService = new UserDetailsPageService(connection, user);
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
            dataViewPage.SetUpPage( userDetailsPageService.GetTeamsThatUserIn());
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            OnBackButtonClicked?.Invoke(this);
        }
    }
}
