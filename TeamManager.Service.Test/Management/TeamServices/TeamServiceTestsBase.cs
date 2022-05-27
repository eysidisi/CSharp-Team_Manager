using Moq;
using TeamManager.Service.Management.DatabaseControllers;

namespace TeamManager.Service.UnitTest.Management.TeamServices
{
    public class TeamServiceTestsBase
    {
        protected readonly Mock<ManagerDatabaseController> databaseManager;
        public TeamServiceTestsBase()
        {
            databaseManager = new Mock<ManagerDatabaseController>("connectionString");
        }
    }
}
