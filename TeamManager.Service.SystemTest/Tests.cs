using Dapper.Contrib.Extensions;
using System.Data;
using TeamManager.Service.Management.DatabaseController;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.TeamServices;
using TeamManager.Service.Management.UserServices;
using TeamManager.Service.UnitTest.HelperMethods.Database;
using TeamManager.Service.Wizard;
using TeamManager.Service.Wizard.DatabaseController;
using TeamManager.Service.Wizard.Models;
using Xunit;

namespace TeamManager.Service.SystemTests
{
    public abstract class Tests
    {
        ManagerDatabaseController managerDatabaseController;
        WizardDatabaseController wizardDatabaseController;
        PurposePageService purposePageService;
        LoginPageService loginPageService;
        UserPageService userPageService;
        TeamPageService teamPageService;
        DatabaseTestHelper databaseTestHelper;

        protected string connectionString;

        public void CreateEmptySQLConnection()
        {
            databaseTestHelper = CreateDatabaseTestHelper();
            connectionString = databaseTestHelper.CreateEmptyTestDBWithTables_ReturnConnectionString();
            managerDatabaseController = CreateManagerDatabaseController();
            wizardDatabaseController = CreateWizardDatabaseController();
        }

        protected abstract DatabaseTestHelper CreateDatabaseTestHelper();
        protected abstract ManagerDatabaseController CreateManagerDatabaseController();
        protected abstract WizardDatabaseController CreateWizardDatabaseController();
        protected abstract IDbConnection CreateConnection();


        // A scenerio for an empty DB
        [Fact]
        public void OperationsOnEmptyDB()
        {
            CreateEmptySQLConnection();

            loginPageService = new LoginPageService(wizardDatabaseController);
            databaseTestHelper.AddValidManager();

            Manager validManager = new Manager(DatabaseTestHelper.ValidManagerUserName, DatabaseTestHelper.ValidManagerPassword);
            AssertManagerExists(validManager);

            Manager invalidManager = new Manager("invalidManager", "validManager");
            AssertManagerDoesNotExists(invalidManager);

            purposePageService = new PurposePageService(wizardDatabaseController);

            Purpose validPurpose = new Purpose("userName", "purposeText");
            AssertCanAddPurpose(validPurpose);

            Purpose invalidPurpose = new Purpose();
            AssertCantAddPurpose(invalidPurpose);

            teamPageService = new TeamPageService(managerDatabaseController);
            userPageService = new UserPageService(managerDatabaseController);

            AssertNoUserExists();
            AssertNoTeamExists();

            User validUser1 = new User() { ID = 1, Name = "UserToAdd" };
            AddUser(validUser1);
            AssertUserAdded(validUser1);

            User invalidUser = new User() { };
            AssertCantAddUser(invalidUser);

            User validUser2 = new User() { ID = 2, Name = "UserToAdd2" };
            Team team1 = new Team() { ID = 1, Name = "TeamToAdd" };
            AssertCantAddUserToTeam(validUser1, team1);
            AssertCantAddUserToTeam(validUser2, team1);
            AssertCantRemoveUserFromTeam(validUser1, team1);

            AddTeam(team1);
            AssertTeamAdded(team1);
            AssertCantAddTeam(team1);
            AssertCantRemoveUserFromTeam(validUser2, team1);

            AssertTeamHasNoUsers(team1);
            AssertUserHasNoTeams(validUser1);

            Team invalidTeam = new Team() { };
            AssertCantAddTeam(invalidTeam);

            AddUserToTeam(validUser1, team1);
            AssertTeamHasUser(team1, validUser1);
            AssertCantAddUserToTeam(validUser1, team1);
            AssertUserHasTeam(validUser1, team1);

            AssertCantDeleteTeam(team1);

            DeleteUser(validUser1);
            AssertUserIsDeleted(validUser1);
            AssertTeamHasNoUsers(team1);
            AssertCantDeleteUser(validUser1);

            DeleteTeam(team1);
            AssertNoTeamExists();
            AssertCantDeleteTeam(team1);

            AddTeam(team1);
            AssertTeamAdded(team1);

            AddUser(validUser1);
            AssertUserAdded(validUser1);

            AddUser(validUser2);
            AssertUserAdded(validUser2);

            AddUserToTeam(validUser1, team1);
            AssertTeamHasUser(team1, validUser1);

            AddUserToTeam(validUser2, team1);
            AssertTeamHasUser(team1, validUser2);

            RemoveUserFromTeam(validUser1, team1);
            AssertTeamDoesntContainUser(team1, validUser1);

            AssertCantRemoveUserFromTeam(validUser1, team1);
        }

        private void AssertCantAddPurpose(Purpose purpose)
        {
            Assert.Throws<ArgumentException>(() => purposePageService.SavePurposeOfVisit(purpose));
        }

        private void AssertCanAddPurpose(Purpose purpose)
        {
            purposePageService.SavePurposeOfVisit(purpose);
            List<Purpose> actualPurposes;
            using (IDbConnection conn = CreateConnection())
            {
                actualPurposes = conn.GetAll<Purpose>().ToList();
            }
            Assert.Contains(purpose, actualPurposes);
        }



        private void AssertManagerDoesNotExists(Manager manager)
        {
            Assert.Throws<ArgumentException>(() => loginPageService.GetManager(manager));
        }

        private void AssertManagerExists(Manager validManager)
        {
            var actualManager = loginPageService.GetManager(validManager);
            Assert.NotNull(actualManager);
        }

        private void AssertCantAddUser(User invalidUser)
        {
            NewUserPageService newUserPageService = new NewUserPageService(managerDatabaseController);
            Assert.Throws<ArgumentException>(() => newUserPageService.SaveNewUser(invalidUser));
        }

        private void AssertCantAddTeam(Team team)
        {
            NewTeamPageService teamPageService = new NewTeamPageService(managerDatabaseController);
            Assert.Throws<ArgumentException>(() => teamPageService.SaveTeam(team));
        }

        private void AssertUserHasTeam(User user, Team team)
        {
            UserDetailsPageService userDetailsPage = new UserDetailsPageService(managerDatabaseController, user);
            var actualTeams = userDetailsPage.GetTeamsThatUserIn();

            Assert.Contains(team, actualTeams);
        }

        private void AssertCantAddUserToTeam(User user, Team team)
        {
            EditTeamPageService editTeamPageService = new EditTeamPageService(managerDatabaseController, team);
            Assert.Throws<ArgumentException>(() => editTeamPageService.AddUserToTheTeam(user));
        }

        private void AssertCantRemoveUserFromTeam(User user, Team team)
        {
            EditTeamPageService editTeamPageService = new EditTeamPageService(managerDatabaseController, team);
            Assert.Throws<ArgumentException>(() => editTeamPageService.RemoveUserFromTheTeam(user));
        }

        private void AssertTeamDoesntContainUser(Team team, User user)
        {
            TeamDetailsPageService teamDetailsPage = new TeamDetailsPageService(managerDatabaseController, team);
            var actualUsers = teamDetailsPage.GetUsersInTeam();
            Assert.DoesNotContain(user, actualUsers);
        }

        private void RemoveUserFromTeam(User user1, Team team)
        {
            EditTeamPageService editTeamPageService = new EditTeamPageService(managerDatabaseController, team);
            editTeamPageService.RemoveUserFromTheTeam(user1);
        }

        private void AssertCantDeleteUser(User user)
        {
            Assert.Throws<ArgumentException>(() => userPageService.DeleteUser(user));
        }

        private void DeleteTeam(Team team)
        {
            teamPageService.DeleteTeam(team);
        }

        private void AssertUserIsDeleted(User user)
        {
            var users = userPageService.GetUsers();
            Assert.DoesNotContain(user, users);
        }

        private void DeleteUser(User user)
        {
            userPageService.DeleteUser(user);
        }

        private void AssertCantDeleteTeam(Team team)
        {
            Assert.Throws<ArgumentException>(() => teamPageService.DeleteTeam(team));
        }

        private void AssertTeamHasUser(Team team, User addedUser)
        {
            TeamDetailsPageService teamDetailsPage = new TeamDetailsPageService(managerDatabaseController, team);
            var users = teamDetailsPage.GetUsersInTeam();
            Assert.Contains(addedUser, users);
        }

        private void AssertUserHasNoTeams(User user)
        {
            UserDetailsPageService userDetailsPageService = new UserDetailsPageService(managerDatabaseController, user);
            var teams = userDetailsPageService.GetTeamsThatUserIn();
            Assert.Empty(teams);
        }

        private void AssertTeamHasNoUsers(Team team)
        {
            TeamDetailsPageService teamDetailsPageService = new TeamDetailsPageService(managerDatabaseController, team);
            var users = teamDetailsPageService.GetUsersInTeam();
            Assert.Empty(users);
        }

        private void AddUserToTeam(User userToAdd, Team team)
        {
            EditTeamPageService editTeamPageService = new EditTeamPageService(managerDatabaseController, team);
            editTeamPageService.AddUserToTheTeam(userToAdd);
        }

        private void AssertTeamAdded(Team teamToAdd)
        {
            var actualTeams = teamPageService.GetAllTeams();
            Assert.Contains(teamToAdd, actualTeams);
        }

        private void AddTeam(Team teamToAdd)
        {
            NewTeamPageService newTeamPageService = new NewTeamPageService(managerDatabaseController);
            newTeamPageService.SaveTeam(teamToAdd);
        }

        private void AssertNoTeamExists()
        {
            var allTeams = teamPageService.GetAllTeams();
            Assert.Empty(allTeams);
        }

        private void AssertUserAdded(User userToAdd)
        {
            var actualUsers = userPageService.GetUsers();
            Assert.Contains(userToAdd, actualUsers);
        }

        private void AddUser(User userToAdd)
        {
            NewUserPageService newUserPageService = new NewUserPageService(managerDatabaseController);
            newUserPageService.SaveNewUser(userToAdd);
        }

        private void AssertNoUserExists()
        {
            List<User> users = userPageService.GetUsers();
            Assert.Empty(users);
        }
    }
}