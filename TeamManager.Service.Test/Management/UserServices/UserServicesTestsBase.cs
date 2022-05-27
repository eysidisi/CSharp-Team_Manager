using Moq;
using TeamManager.Service.Management.DatabaseManagers;

namespace TeamManager.Service.UnitTest.Management.UserServices
{
    public class UserServicesTestsBase
    {
        protected readonly Mock<ManagerDatabaseController> databaseManager;
        public UserServicesTestsBase()
        {
            databaseManager = new Mock<ManagerDatabaseController>("connectionString");
        }
    }
}
