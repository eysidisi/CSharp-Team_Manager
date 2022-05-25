using System.Data.Common;

namespace TeamManager.Service.UnitTest.HelperMethods.Database
{
    public abstract class DatabaseTestHelperBase
    {
        public static readonly string ValidManagerUserName = "validUserName";
        public static readonly string ValidManagerPassword = "validPassword";

        public string ConnectionString { get; private set; }
        protected string dbName { get; private set; }
        protected abstract string UserIDToTeamIDTableSQL { get; }

        protected abstract string TeamsTableSQL { get; }

        protected abstract string PurposesTableSQL { get; }

        protected abstract string ManagersTableSQL { get; }

        protected abstract string UserTableSQL { get; }

        static int NextDBNumber = 0;
        static readonly object syncObj = new object();

        private static int GetNextDBNumber()
        {
            int currentDbNum;

            lock (syncObj)
            {
                currentDbNum = NextDBNumber++;
            }

            return currentDbNum;
        }

        /// <summary>
        /// Creates a test DB and returns the connection string
        /// </summary>
        /// <returns></returns>
        public string CreateEmptyTestDBWithTables_ReturnConnectionString(string dbName = null)
        {
            if (dbName != null)
            {
                this.dbName = dbName;
            }

            else
            {
                int currentDBNum = GetNextDBNumber();
                this.dbName = $"db_{currentDBNum}";
            }

            ConnectionString = GetConnectionString();

            DeleteDBIfExists();
            CreateDB();
            AddUserTable();
            AddManagerTable();
            AddPurposeTable();
            AddTeamsTable();
            AddUserIDToTeamIDTable();

            return ConnectionString;
        }


        private void AddUserIDToTeamIDTable()
        {
            RunSQL(UserIDToTeamIDTableSQL);
        }

        private void AddTeamsTable()
        {
            RunSQL(TeamsTableSQL);
        }

        private void AddPurposeTable()
        {
            RunSQL(PurposesTableSQL);
        }

        private void AddManagerTable()
        {
            RunSQL(ManagersTableSQL);
        }

        private void AddUserTable()
        {
            RunSQL(UserTableSQL);
        }

        public void DeleteCreatedDB()
        {
            DeleteDBIfExists();
        }

        protected abstract DbConnection CreateConnection();

        protected abstract string GetConnectionString();

        protected abstract void DeleteDBIfExists();

        protected abstract void RunSQL(string sqlCommand);

        protected abstract void CreateDB();

    }
}
