using TeamManager.UI.Wizard;

namespace TeamManager.UI
{
    internal static class Program
    {
        //static string connectionString = ConfigurationManager.ConnectionStrings["SQLiteTestSmallDB"].ConnectionString;
        //static string connectionString = ConfigurationManager.ConnectionStrings["SQLiteTestMediumDB"].ConnectionString;
        //static string connectionString = ConfigurationManager.ConnectionStrings["SQLiteTestLargeDB"].ConnectionString;
        //static readonly string connectionString = ConfigurationManager.ConnectionStrings["MySQLTestSmallDB"].ConnectionString;
        //static readonly string connectionString = ConfigurationManager.ConnectionStrings["MySQLTestLargeDB"].ConnectionString;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new WizardForm());
        }
    }
}