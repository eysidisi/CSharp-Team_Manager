using System.Configuration;
using TeamManager.UI.Management;

namespace TeamManager.UI
{
    internal static class Program
    {
        //static string connectionString = ConfigurationManager.ConnectionStrings["TestSmallDB"].ConnectionString;
        //static string connectionString = ConfigurationManager.ConnectionStrings["TestMediumDB"].ConnectionString;
        static readonly string connectionString = ConfigurationManager.ConnectionStrings["TestLargeDB"].ConnectionString;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new ManagementForm(connectionString));
        }
    }
}