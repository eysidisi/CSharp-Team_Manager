using Moq;
using TeamManager.Service.Management.DatabaseConnection;
using TeamManager.Service.Management.DatabaseController;

namespace TeamManager.Service.UnitTest.Management.TeamServices
{
    public class TeamServiceTestsBase
    {
        protected readonly Mock<IManagerDatabaseConnection> connection;
        protected readonly ManagerDatabaseController databaseController;

        public TeamServiceTestsBase()
        {
            connection = new Mock<IManagerDatabaseConnection>();
            databaseController = new ManagerDatabaseController(connection.Object);
        }
    }
}
