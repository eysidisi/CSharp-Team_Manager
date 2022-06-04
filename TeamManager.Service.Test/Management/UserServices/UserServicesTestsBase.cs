using Moq;
using TeamManager.Service.Management.DatabaseConnection;
using TeamManager.Service.Management.DatabaseController;

namespace TeamManager.Service.UnitTest.Management.UserServices
{
    public class UserServicesTestsBase
    {
        protected readonly ManagerDatabaseController databaseController;
        protected readonly Mock<IManagerDatabaseConnection> connection;
        public UserServicesTestsBase()
        {
            connection = new Mock<IManagerDatabaseConnection>();
            databaseController = new ManagerDatabaseController(connection.Object);
        }
    }
}
