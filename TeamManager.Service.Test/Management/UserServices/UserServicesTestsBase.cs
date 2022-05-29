using Moq;
using TeamManager.Service.Management.DatabaseControllers;

namespace TeamManager.Service.UnitTest.Management.UserServices
{
    public class UserServicesTestsBase
    {
        protected readonly Mock<ManagerDatabaseController> databaseController;
        public UserServicesTestsBase()
        {
            databaseController = new Mock<ManagerDatabaseController>("connectionString");
        }
    }
}
