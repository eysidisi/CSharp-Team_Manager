using System.Configuration;
using TeamManager.Service.Management.DatabaseManagers;
using TeamManager.UI.Management;

namespace TeamManager.UI
{
    internal static class Program
    {
        //static string connectionString = ConfigurationManager.ConnectionStrings["SQLiteTestSmallDB"].ConnectionString;
        //static string connectionString = ConfigurationManager.ConnectionStrings["SQLiteTestMediumDB"].ConnectionString;
        static readonly string connectionString = ConfigurationManager.ConnectionStrings["MySQLTestLargeDB"].ConnectionString;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            DatabaseManager databaseManager = new MySQLDatabaseManager(connectionString);
            Application.Run(new ManagementForm(databaseManager));
        }
    }
}