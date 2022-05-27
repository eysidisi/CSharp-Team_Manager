using Moq;
using TeamManager.Service.Management.DatabaseControllers;

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
