using TeamManager.Service.Management.DatabaseManagers;
using TeamManager.Service.Management.Models;
using TeamManager.Service.Management.TeamServices;
using TeamManager.Service.Management.UserServices;
using TeamManager.Service.UnitTest.HelperMethods.Database;
using Xunit;

namespace TeamManager.Service.SystemTest
{
    public class SystemTests
    {
        DatabaseManager databaseManager;
        UserPageService userPageService;
        TeamPageService teamPageService;


        private void CreateEmptySQLConnection()
        {
            SQLiteDatabaseTestHelper sQLiteHelperMethods = new SQLiteDatabaseTestHelper();
            var connectionString = sQLiteHelperMethods.CreateEmptyTestDBWithTables_ReturnConnectionString();
            databaseManager = new SQLiteDatabaseManager(connectionString);
        }

        // Empty DB
        // Creates a User
        // Checks if user is added
        // Creates a Team
        // Checks if team is added
        // Adds user a Team
        // Checks if user is added to the team
        // Checks user details to see the team
        // Tries to delete the team
        // Deletes the user
        // Deletes the team
        [Fact]
        public void OperationsOnEmptyDB()
        {
            CreateEmptySQLConnection();

            teamPageService = new TeamPageService(databaseManager);
            userPageService = new UserPageService(databaseManager);

            AssertNoUserExists();
            AssertNoTeamExists();

            User user = new User() { ID = 1, Name = "UserToAdd" };
            AddUser(user);
            AssertUserAdded(user);

            Team team = new Team() { ID = 1, Name = "TeamToAdd" };
            AddTeam(team);
            AssertTeamAdded(team);

            AssertTeamHasNoUsers(team);
            AssertUserHasNoTeams(user);

            AddUserToTeam(user, team);
            AssertTeamHasUser(team, user);

            AssertCantDeleteTeam(team);

            DeleteUser(user);
            AssertUserIsDeleted(user);
            AssertTeamHasNoUsers(team);
            AssertCantDeleteUser(user);

            DeleteTeam(team);
            AssertNoTeamExists();
            AssertCantDeleteTeam(team);
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
            TeamDetailsPageService teamDetailsPage = new TeamDetailsPageService(databaseManager, team);
            var users = teamDetailsPage.GetUsersInTeam();
            Assert.Contains(addedUser, users);
        }

        private void AssertUserHasNoTeams(User user)
        {
            UserDetailsPageService userDetailsPageService = new UserDetailsPageService(databaseManager, user);
            var teams = userDetailsPageService.GetTeamsThatUserIn();
            Assert.Empty(teams);
        }

        private void AssertTeamHasNoUsers(Team team)
        {
            TeamDetailsPageService teamDetailsPageService = new TeamDetailsPageService(databaseManager, team);
            var users = teamDetailsPageService.GetUsersInTeam();
            Assert.Empty(users);
        }

        private void AddUserToTeam(User userToAdd, Team team)
        {
            EditTeamPageService editTeamPageService = new EditTeamPageService(databaseManager, team);
            editTeamPageService.AddUserToTheTeam(userToAdd);
        }

        private void AssertTeamAdded(Team teamToAdd)
        {
            var actualTeams = teamPageService.GetAllTeams();
            Assert.Equal(new List<Team>() { teamToAdd }, actualTeams);
        }

        private void AddTeam(Team teamToAdd)
        {
            NewTeamPageService newTeamPageService = new NewTeamPageService(databaseManager);
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
            Assert.Equal(new List<User>() { userToAdd }, actualUsers);
        }

        private void AddUser(User userToAdd)
        {
            NewUserPageService newUserPageService = new NewUserPageService(databaseManager);
            newUserPageService.SaveNewUser(userToAdd);
        }

        private void AssertNoUserExists()
        {
            List<User> users = userPageService.GetUsers();
            Assert.Empty(users);
        }
    }
}