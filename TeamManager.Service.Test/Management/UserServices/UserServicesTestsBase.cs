using Moq;
using TeamManager.Service.Management.DatabaseManagers;

namespace TeamManager.Service.UnitTest.Management.UserServices
{
    public class UserServicesTestsBase
    {
        protected readonly Mock<DatabaseManager> databaseManager;
        public UserServicesTestsBase()
        {
            databaseManager = new Mock<DatabaseManager>();
        }
    }
}
