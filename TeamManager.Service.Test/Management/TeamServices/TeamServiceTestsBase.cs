using Moq;
using TeamManager.Service.Management.DatabaseManagers;

namespace TeamManager.Service.UnitTest.Management.TeamServices
{
    public class TeamServiceTestsBase
    {
        protected readonly Mock<DatabaseManager> databaseManager;
        public TeamServiceTestsBase()
        {
            databaseManager = new Mock<DatabaseManager>("connectionString");
        }
    }
}
