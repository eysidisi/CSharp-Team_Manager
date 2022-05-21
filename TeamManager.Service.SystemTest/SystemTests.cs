using TeamManager.Service.Management;
using TeamManager.Service.Management.Database;
using TeamManager.Service.Management.TeamServices;
using TeamManager.Service.Management.UserServices;
using TeamManager.Service.Models;
using TeamManager.Service.Test.HelperMethods.SQLiteDB;
using Xunit;

namespace TeamManager.Service.SystemTest
{
    public class SystemTests
    {
        IManagementDatabaseConnection connection;
        UserPageService userPageService;
        TeamPageService teamPageService;


        private void CreateEmptySQLConnection()
        {
            SQLiteHelperMethods sQLiteHelperMethods = new SQLiteHelperMethods();
            var dbFilePath = sQLiteHelperMethods.CreateEmptyTestDB_ReturnFilePath();
            var connectionString = $@"Data Source = {dbFilePath}; Version = 3";
            connection = new ManagementSQLiteConnetion(connectionString);
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

            teamPageService = new TeamPageService(connection);
            userPageService = new UserPageService(connection);

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
            TeamDetailsPageService teamDetailsPage = new TeamDetailsPageService(connection, team);
            var users = teamDetailsPage.GetUsersInTeam();
            Assert.Contains(addedUser, users);
        }

        private void AssertUserHasNoTeams(User user)
        {
            UserDetailsPageService userDetailsPageService = new UserDetailsPageService(connection, user);
            var teams = userDetailsPageService.GetTeamsThatUserIn();
            Assert.Empty(teams);
        }

        private void AssertTeamHasNoUsers(Team team)
        {
            TeamDetailsPageService teamDetailsPageService = new TeamDetailsPageService(connection, team);
            var users = teamDetailsPageService.GetUsersInTeam();
            Assert.Empty(users);
        }

        private void AddUserToTeam(User userToAdd, Team team)
        {
            EditTeamPageService editTeamPageService = new EditTeamPageService(connection, team);
            editTeamPageService.AddUserToTheTeam(userToAdd);
        }

        private void AssertTeamAdded(Team teamToAdd)
        {
            var actualTeams = teamPageService.GetAllTeams();
            Assert.Equal(new List<Team>() { teamToAdd }, actualTeams);
        }

        private void AddTeam(Team teamToAdd)
        {
            NewTeamPageService newTeamPageService = new NewTeamPageService(connection);
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
            NewUserPageService newUserPageService = new NewUserPageService(connection);
            newUserPageService.SaveNewUser(userToAdd);
        }

        private void AssertNoUserExists()
        {
            List<User> users = userPageService.GetUsers();
            Assert.Empty(users);
        }
    }
}