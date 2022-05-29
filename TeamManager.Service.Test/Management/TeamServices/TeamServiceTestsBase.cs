using Moq;
using TeamManager.Service.Management.DatabaseControllers;

namespace TeamManager.Service.UnitTest.Management.TeamServices
{
    public class TeamServiceTestsBase
    {
        protected readonly Mock<ManagerDatabaseController> databaseController;
        public TeamServiceTestsBase()
        {
            databaseController = new Mock<ManagerDatabaseController>("connectionString");
        }
    }
}
